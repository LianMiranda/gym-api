namespace Gym.Domain.Interfaces.Seeds;

public interface IExerciseSeeder
{
    Task SeedExercisesAsync(CancellationToken cancellationToken = default);
}