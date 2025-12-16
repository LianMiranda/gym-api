using System.Data;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Gym.Domain.Enums.Exercise_Enums;
using Gym.Domain.Interfaces.Seeds;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Gym.Infrastructure.Database.Seeds;

// Record usado com um "wrapper" para mapear a estrutura raiz do JSON que contém a lista de exercícios.
public record ExerciseRoot
{
    [JsonPropertyName("exercises")] public List<ExerciseJson>? Exercises { get; init; } = [];
}

// Record que representa cada exercício no JSON de entrada
public record ExerciseJson
{
    [JsonPropertyName("name")] public string Name { get; init; } = null!;
    [JsonPropertyName("level")] public string Level { get; init; } = null!;
    [JsonPropertyName("instructions")] public List<string> Instructions { get; init; } = [];
    [JsonPropertyName("equipment")] public string Equipment { get; init; } = null!;
    [JsonPropertyName("primaryMuscles")] public List<string> PrimaryMuscles { get; init; } = null!;
    [JsonPropertyName("category")] public string Category { get; init; } = null!;
}

// DTO para inserção em lote
public record ExerciseInsertDto(
    string Name,
    string Description,
    string Category,
    string MuscleGroup,
    string Equipment,
    string DifficultyLevel
);

public class ExerciseSeeder : IExerciseSeeder
{
    private readonly string _connectionString;
    private readonly ILogger<ExerciseSeeder> _logger;

    private const string TableName = "exercises";
    private const string ColumnId = "id";
    private const string ColumnName = "name";
    private const string ColumnDescription = "description";
    private const string ColumnCategory = "category";
    private const string ColumnMuscleGroup = "muscle_group";
    private const string ColumnEquipment = "equipment";
    private const string ColumnDifficultyLevel = "difficulty_level";
    private const string ColumnCreatedAt = "created_at";

    public ExerciseSeeder(
        IConfiguration configuration,
        ILogger<ExerciseSeeder> logger)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(logger);

        _connectionString = configuration.GetConnectionString("DbConnection")
                            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        _logger = logger;
    }

    /// <summary>
    /// Método principal que lê o arquivo JSON de exercícios e popula o banco de dados
    /// </summary>
    public async Task SeedExercisesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Seeding exercises...");

            //Verifica se o banco de dados já possui registros -> se não: executa o json
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);

            // Verifica se já existem dados
            if (await HasExistingDataAsync(connection, cancellationToken))
            {
                _logger.LogInformation("The database already contains exercises. Seed canceled.");
                return;
            }

            // Lê e deserializa o JSON
            var exercises = await LoadExercisesFromJsonAsync(cancellationToken);

            if (exercises.Count == 0)
            {
                _logger.LogWarning("No exercises were found in the JSON.");
                return;
            }

            _logger.LogInformation("{Count} exercises found in JSON.", exercises.Count);

            var mappedExercises = exercises
                .Select(MapExerciseToDto)
                .Where(e => e is not null)
                .ToList();

            await BulkInsertExercisesAsync(connection, mappedExercises!, cancellationToken);

            _logger.LogInformation("✅ Seed completed successfully! {Count} exercises inserted.",
                mappedExercises.Count);
        }
        catch (FileNotFoundException ex)
        {
            _logger.LogError(ex, "❌ JSON file not found.");
            throw;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "❌ Error deserializing JSON");
            throw;
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "❌ Error accessing the database.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Unexpected error during exercise seeding.");
            throw;
        }
    }

    /// <summary>
    /// Verifica se já existem exercícios no banco
    /// </summary>
    private static async Task<bool> HasExistingDataAsync(
        SqlConnection connection,
        CancellationToken cancellationToken)
    {
        await using var command = new SqlCommand($"SELECT COUNT(*) FROM {TableName}", connection);
        var count = (int)(await command.ExecuteScalarAsync(cancellationToken))!;
        return count > 0;
    }

    // <summary>
    /// Carrega e deserializa o arquivo JSON
    /// </summary>
    private async Task<List<ExerciseJson>> LoadExercisesFromJsonAsync(CancellationToken cancellationToken)
    {
        var jsonPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Database",
            "Seeds",
            "exercises.json"
        );

        if (!File.Exists(jsonPath))
        {
            throw new FileNotFoundException($"File not found: {jsonPath}");
        }

        _logger.LogDebug("Reading JSON file: {Path}", jsonPath);

        var jsonString = await File.ReadAllTextAsync(jsonPath, Encoding.UTF8, cancellationToken);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true
        };

        var root = JsonSerializer.Deserialize<ExerciseRoot>(jsonString, options);

        return root?.Exercises ?? [];
    }

    /// <summary>
    /// Insere exercícios em lote usando SqlBulkCopy para máxima performance
    /// </summary>
    private async Task BulkInsertExercisesAsync(
        SqlConnection connection,
        List<ExerciseInsertDto> exercises,
        CancellationToken cancellationToken)
    {
        // Cria DataTable para bulk insert
        var dataTable = CreateExerciseDataTable();

        foreach (var exercise in exercises)
        {
            dataTable.Rows.Add(
                Guid.NewGuid(),
                exercise.Name,
                exercise.Description,
                exercise.Category,
                exercise.MuscleGroup,
                exercise.Equipment,
                exercise.DifficultyLevel,
                DateTime.UtcNow
            );
        }

        // Usa transação para garantir consistência
        await using var transaction = (SqlTransaction)await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            using var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction)
            {
                DestinationTableName = TableName,
                BatchSize = 1000,
                BulkCopyTimeout = 300
            };

            // Mapeia colunas
            bulkCopy.ColumnMappings.Add(ColumnId, ColumnId);
            bulkCopy.ColumnMappings.Add(ColumnName, ColumnName);
            bulkCopy.ColumnMappings.Add(ColumnDescription, ColumnDescription);
            bulkCopy.ColumnMappings.Add(ColumnCategory, ColumnCategory);
            bulkCopy.ColumnMappings.Add(ColumnMuscleGroup, ColumnMuscleGroup);
            bulkCopy.ColumnMappings.Add(ColumnEquipment, ColumnEquipment);
            bulkCopy.ColumnMappings.Add(ColumnDifficultyLevel, ColumnDifficultyLevel);
            bulkCopy.ColumnMappings.Add(ColumnCreatedAt, ColumnCreatedAt);

            await bulkCopy.WriteToServerAsync(dataTable, cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("Bulk insert completed successfully.");
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError("Error during bulk insert. Transaction rolled back.");
            throw;
        }
    }

    /// <summary>
    /// Cria DataTable com a estrutura da tabela de exercícios
    /// </summary>
    private static DataTable CreateExerciseDataTable()
    {
        var dataTable = new DataTable();

        dataTable.Columns.Add(ColumnId, typeof(Guid));
        dataTable.Columns.Add(ColumnName, typeof(string));
        dataTable.Columns.Add(ColumnDescription, typeof(string));
        dataTable.Columns.Add(ColumnCategory, typeof(string));
        dataTable.Columns.Add(ColumnMuscleGroup, typeof(string));
        dataTable.Columns.Add(ColumnEquipment, typeof(string));
        dataTable.Columns.Add(ColumnDifficultyLevel, typeof(string));
        dataTable.Columns.Add(ColumnCreatedAt, typeof(DateTime));

        return dataTable;
    }

    /// <summary>
    /// Mapeia um exercício do JSON para DTO de inserção
    /// </summary>
    private ExerciseInsertDto? MapExerciseToDto(ExerciseJson exercise)
    {
        if (string.IsNullOrWhiteSpace(exercise.Name))
        {
            _logger.LogWarning("No name found for this exercise, skip...");
            return null;
        }

        var category = MapCategory(exercise.Category) ?? ExerciseCategory.Strength;
        var difficulty = MapDifficulty(exercise.Level) ?? DifficultyLevel.Beginner;
        var equipment = MapEquipment(exercise.Equipment) ?? Equipment.None;

        // Pega o primeiro músculo primário ou usa um valor padrão
        var muscleGroup = exercise.PrimaryMuscles.Count > 0
            ? MapMuscleGroup(exercise.PrimaryMuscles[0]) ?? MuscleGroup.FullBody
            : MuscleGroup.FullBody;

        // Combina todas as instruções em uma descrição
        var description = exercise.Instructions.Count > 0
            ? string.Join(" ", exercise.Instructions)
            : "No description available.";

        return new ExerciseInsertDto(
            Name: exercise.Name,
            Description: description,
            Category: category.ToString(),
            MuscleGroup: muscleGroup.ToString(),
            Equipment: equipment.ToString(),
            DifficultyLevel: difficulty.ToString()
        );
    }

    #region Métodos de Mapeamento

    private static ExerciseCategory? MapCategory(string value) =>
        value.ToLowerInvariant().Trim() switch
        {
            "cardio" => ExerciseCategory.Cardio,
            "olympic weightlifting" => ExerciseCategory.Olympic,
            "plyometrics" => ExerciseCategory.Plyometrics,
            "powerlifting" => ExerciseCategory.Powerlifting,
            "strength" => ExerciseCategory.Strength,
            "stretching" => ExerciseCategory.Stretching,
            "strongman" => ExerciseCategory.Strength,
            _ => null
        };

    private static DifficultyLevel? MapDifficulty(string value) =>
        value.ToLowerInvariant().Trim() switch
        {
            "beginner" => DifficultyLevel.Beginner,
            "intermediate" => DifficultyLevel.Intermediate,
            "expert" => DifficultyLevel.Expert,
            _ => null
        };

    private static Equipment? MapEquipment(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Equipment.Other;  
        
        return value.ToLowerInvariant().Trim() switch
            {
                "none" or "body only" or "" => Equipment.None,
                "barbell" => Equipment.Barbell,
                "dumbbell" => Equipment.Dumbbell,
                "kettlebells" or "kettlebell" => Equipment.Kettlebell,
                "machine" => Equipment.Machine,
                "cable" => Equipment.Cable,
                "bands" => Equipment.ResistanceBand,
                "medicine ball" => Equipment.MedicineBall,
                "foam roll" => Equipment.FoamRoller,
                "bench" => Equipment.Bench,
                "pull-up bar" or "pull up bar" => Equipment.PullUpBar,
                _ => Equipment.Other
            };
    }


    private static MuscleGroup? MapMuscleGroup(string value) =>
        value.ToLowerInvariant().Trim() switch
        {
            "chest" => MuscleGroup.Chest,
            "lats" or "middle back" or "lower back" or "back" => MuscleGroup.Back,
            "shoulders" => MuscleGroup.Shoulders,
            "biceps" => MuscleGroup.Biceps,
            "triceps" => MuscleGroup.Triceps,
            "forearms" => MuscleGroup.Forearms,
            "abdominals" or "abs" => MuscleGroup.Abs,
            "obliques" => MuscleGroup.Obliques,
            "quadriceps" => MuscleGroup.Quadriceps,
            "hamstrings" => MuscleGroup.Hamstrings,
            "calves" => MuscleGroup.Calves,
            "glutes" => MuscleGroup.Glutes,
            "full body" or "fullbody" => MuscleGroup.FullBody,
            _ => null
        };

    #endregion
}