using System.ComponentModel;

namespace Gym.Domain.Enums.ExerciseSet_Enums;

public enum SetType : short
{
    [Description("Negativas (apenas fase excêntrica)")]
    Negative = 0,

    [Description("Aquecimento")] Warmup = 1,

    [Description("Série válida/normal")] Working = 2,

    [Description("Drop set (redução progressiva de carga)")]
    DropSet = 3,

    [Description("Super set (dois exercícios seguidos)")]
    SuperSet = 4,

    [Description("Rest-pause (pausa curta no meio da série)")]
    RestPause = 5,

    [Description("Pirâmide (aumenta ou diminui carga)")]
    Pyramid = 6,

    [Description("Cluster set (mini-pausas entre repetições)")]
    Cluster = 7,

    [Description("Giant set (3+ exercícios seguidos)")]
    Giant = 8,

    [Description("As Many Reps As Possible")]
    AMRAP = 9,

    [Description("Every Minute On the Minute")]
    EMOM = 10,

    [Description("Série até a falha com peso leve")]
    Burnout = 11,

    [Description("Isométrica (manter posição)")]
    Isometric = 12,

    [Description("Fase excêntrica enfatizada")]
    Eccentric = 13,

    [Description("Com tempo controlado")] Tempo = 14,

    [Description("Myo-reps (mini séries após série principal)")]
    MyoRep = 15,

    [Description("Até a falha muscular")] Failure = 16,

    [Description("Repetições parciais")] PartialRep = 17
}