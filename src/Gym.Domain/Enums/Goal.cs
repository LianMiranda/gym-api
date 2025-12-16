using System.ComponentModel;

namespace Gym.Domain.Enums;

public enum Goal : short
{
    [Description("Ganho de Massa Muscular")]
    MuscleGain = 0,

    [Description("Perda de Gordura")] FatLoss = 1,

    [Description("Força Máxima")] Strength = 2,

    [Description("Resistência Muscular")] Endurance = 3,

    [Description("Manutenção Física")] Maintenance = 4,

    [Description("Recomposição Corporal")] BodyRecomposition = 5,

    [Description("Powerbuilding (Força + Estética)")]
    Powerbuilding = 6,

    [Description("Desempenho Atlético")] Performance = 7,

    [Description("Reabilitação / Recuperação")]
    Rehabilitation = 8,

    [Description("Condicionamento Geral")] GeneralFitness = 9
}