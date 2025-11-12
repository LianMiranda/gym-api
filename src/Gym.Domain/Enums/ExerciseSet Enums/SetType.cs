namespace Gym.Domain.Enums.ExerciseSet_Enums;

public enum SetType
{
    Warmup = 1,          // Aquecimento
    Working = 2,         // Série válida/normal
    DropSet = 3,         // Drop set (redução progressiva de carga)
    SuperSet = 4,        // Super set (dois exercícios seguidos)
    RestPause = 5,       // Rest-pause (pausa curta no meio da série)
    Pyramid = 6,         // Pirâmide (aumenta ou diminui carga)
    Cluster = 7,         // Cluster set (mini-pausas entre repetições)
    Giant = 8,           // Giant set (3+ exercícios seguidos)
    AMRAP = 9,           // As Many Reps As Possible
    EMOM = 10,           // Every Minute On the Minute
    Burnout = 11,        // Série até a falha com peso leve
    Isometric = 12,      // Isométrica (manter posição)
    Eccentric = 13,      // Fase excêntrica enfatizada
    Tempo = 14,          // Com tempo controlado
    MyoRep = 15,         // Myo-reps (mini séries após série principal)
    Failure = 16,        // Até a falha muscular
    PartialRep = 17,     // Repetições parciais
    Negative = 18        // Negativas (apenas fase excêntrica)
}