using UnityEngine;
using UnityEngine.UI;

public class ItemClicavel : MonoBehaviour
{
    public int numeroMissao;

    public void ClicarItem()
    {
        if (numeroMissao == 1) MissaoManager.instance.missao1Concluida = true;
        if (numeroMissao == 2) MissaoManager.instance.missao2Concluida = true;
        if (numeroMissao == 3) MissaoManager.instance.missao3Concluida = true;

        gameObject.SetActive(false); 
    }
}
