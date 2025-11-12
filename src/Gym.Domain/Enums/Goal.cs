using System.ComponentModel;

namespace Gym.Domain.Enums;
public enum Goal
{
    [Description("Ganho de Massa Muscular")]
    MuscleGain,

    [Description("Perda de Gordura")]
    FatLoss,

    [Description("Força Máxima")]
    Strength,

    [Description("Resistência Muscular")]
    Endurance,

    [Description("Manutenção Física")]
    Maintenance,

    [Description("Recomposição Corporal")]
    BodyRecomposition,

    [Description("Powerbuilding (Força + Estética)")]
    Powerbuilding,

    [Description("Desempenho Atlético")]
    Performance,

    [Description("Reabilitação / Recuperação")]
    Rehabilitation,

    [Description("Condicionamento Geral")]
    GeneralFitness
}
