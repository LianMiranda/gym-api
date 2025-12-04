using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Gym.Domain.Enums.Exercise_Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Gym.Infrastructure.Database.Seeds;

// Record usado com um "wrapper" para mapear a estrutura raiz do JSON que contém a lista de exercícios.
public record ExerciseRoot
{
    [JsonPropertyName("exercises")] public List<ExerciseJson>? Exercises { get; set; }
}

// Record que representa cada exercício no JSON de entrada
public record ExerciseJson
{
    [JsonPropertyName("name")] public string Name { get; set; } = null!;
    [JsonPropertyName("level")] public string Level { get; set; } = null!;
    [JsonPropertyName("instructions")] public List<string> Instructions { get; set; } = null!;
    [JsonPropertyName("equipment")] public string Equipment { get; set; } = null!;
    [JsonPropertyName("primaryMuscles")] public List<string> PrimaryMuscles { get; set; } = null!;
    [JsonPropertyName("category")] public string Category { get; set; } = null!;
}

public class ExerciseSeeder
{
    private readonly string _connectionString;

    public ExerciseSeeder(string connectionString)
    {
        _connectionString = connectionString;
    }

    /// <summary>
    /// Método principal que lê o arquivo JSON de exercícios e popula o banco de dados
    /// </summary>
    public async Task SeedExercises()
    {
        try
        {
            //Verifica se o banco de dados já possui registros -> se não: executa o json
            using var checkConnection = new SqlConnection(_connectionString);
            await checkConnection.OpenAsync();
        
            var countCommand = new SqlCommand("SELECT COUNT(*) FROM exercises", checkConnection);
            var existingCount = (int)(await countCommand.ExecuteScalarAsync())!;
        
            if (existingCount > 0)
            {
                return;
            }
            
            // Localiza o arquivo JSON no diretório da aplicação
            var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "Seeds", "exercises.json");

            if (!File.Exists(jsonPath))
                throw new FileNotFoundException($"File not found in: {jsonPath}");

            // Lê o conteúdo do arquivo JSON
            var jsonString = await File.ReadAllTextAsync(jsonPath, Encoding.UTF8);

            // Desserializa o JSON em objetos C#
            var root = JsonSerializer.Deserialize<ExerciseRoot>(jsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (root?.Exercises == null || !root.Exercises.Any())
            {
                Console.WriteLine("No exercises found in the JSON.");
                return;
            }

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            int imported = 0;
            int skipped = 0;

            foreach (var exercise in root.Exercises)
            {
                // Verifica se o exercício já existe no banco para evitar duplicação
                var checkCommand = new SqlCommand(
                    "SELECT COUNT(*) FROM exercises WHERE name = @name",
                    connection);
                checkCommand.Parameters.AddWithValue("@name", exercise.Name);

                var exists = (int)(await checkCommand.ExecuteScalarAsync())! > 0;
                if (exists)
                {
                    skipped++;
                    continue;
                }

                // Converte os valores do JSON para os enums do sistema
                var (mappedCategory, mappedDifficulty, mappedEquipment, mappedMuscleGroup)
                    = MapExercise(exercise.Category, exercise.Level, exercise.Equipment,
                        exercise.PrimaryMuscles?.FirstOrDefault());
                
                // Insere o novo exercício no banco de dados
                var command = new SqlCommand(@"
                    INSERT INTO exercises (id, name, description, category, muscle_group, equipment, difficulty_level, created_at)
                    VALUES (NEWID(), @name, @description, @category, @muscle_group, @equipment, @difficulty_level, GETDATE())",
                    connection);

                command.Parameters.AddWithValue("@name", exercise.Name ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@description", exercise.Instructions?.FirstOrDefault() ?? "");
                command.Parameters.AddWithValue("@category", mappedCategory ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@equipment",
                    string.IsNullOrWhiteSpace(exercise.Equipment) ? "none" : mappedEquipment);
                command.Parameters.AddWithValue("@difficulty_level", mappedDifficulty ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@muscle_group", mappedMuscleGroup ?? "");

                await command.ExecuteNonQueryAsync();
                imported++;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error importing exercises.: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Coordena o mapeamento de todos os campos do exercício para os enums correspondentes
    /// </summary>
    private static (string? Category, string? Difficulty, string? Equipment, string? MuscleGroup) MapExercise(
        string? categories,
        string? difficulties,
        string? equipments,
        string? muscleGroups)
    {
        var category = MapCategory(categories ?? "") ?? ExerciseCategory.Strength;
        var difficulty = MapDifficulty(difficulties ?? "") ?? DifficultyLevel.Beginner;
        var equipment = MapEquipment(equipments ?? "") ?? Equipment.None;
        var muscle = MapMuscleGroup(muscleGroups ?? "");

        return (
            category.ToString(),
            difficulty.ToString(),
            equipment.ToString(),
            muscle?.ToString()
        );
    }

    /// <summary>
    /// Converte strings de categoria do JSON para o enum ExerciseCategory
    /// </summary>
    private static ExerciseCategory? MapCategory(string value)
    {
        return value.ToLower().Trim() switch
        {
            "cardio" => ExerciseCategory.Cardio,
            "olympic weightlifting" => ExerciseCategory.Olympic,
            "plyometrics" => ExerciseCategory.Plyometrics,
            "powerlifting" => ExerciseCategory.Powerlifting,
            "strength" => ExerciseCategory.Strength,
            "stretching" => ExerciseCategory.Stretching,
            "strongman" => ExerciseCategory.Strength, // Strongman é mapeado como Strength
            _ => null, // Retorna null para valores não reconhecidos
        };
    }

    /// <summary>
    /// Converte strings de nível de dificuldade para o enum DifficultyLevel
    /// </summary>
    private static DifficultyLevel? MapDifficulty(string value)
    {
        return value.ToLowerInvariant().Trim() switch
        {
            "beginner" => DifficultyLevel.Beginner,
            "intermediate" => DifficultyLevel.Intermediate,
            "expert" => DifficultyLevel.Expert,
            _ => null
        };
    }

    /// <summary>
    /// Converte strings de equipamento para o enum Equipment
    /// Trata variações de nomenclatura (ex: "kettlebells" e "kettlebell")
    /// </summary>
    private static Equipment? MapEquipment(string value)
    {
        return value.ToLowerInvariant().Trim() switch
        {
            "none" or "body only" => Equipment.None,
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
            _ => Equipment.Other // Equipamentos não reconhecidos são marcados como "Other"
        };
    }

    /// <summary>
    /// Converte strings de grupo muscular para o enum MuscleGroup
    /// Consolida variações de costas (lats, middle back, lower back) em um único grupo
    /// </summary>
    private static MuscleGroup? MapMuscleGroup(string value)
    {
        return value.ToLowerInvariant().Trim() switch
        {
            "chest" => MuscleGroup.Chest,
            "lats" or "middle back" or "lower back" or "back" => MuscleGroup.Back, // Todas variações de costas
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
    }
}