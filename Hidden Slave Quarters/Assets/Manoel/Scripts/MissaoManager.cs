using UnityEngine;

public class MissaoManager : MonoBehaviour
{
    public static MissaoManager instance;

    public bool missao1Concluida;
    public bool missao2Concluida;
    public bool missao3Concluida;

    private void Awake()
    {
        instance = this;
    }

    public bool TodasMissoesConcluidas()
    {
        return missao1Concluida && missao2Concluida && missao3Concluida;
    }
}
