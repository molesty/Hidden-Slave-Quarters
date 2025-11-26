using UnityEngine;

public class Missao : MonoBehaviour
{
    public string nomeMissao;

    public void CompletarObjetivo()
    {
        Debug.Log("Objetivo da missão concluído!");
    }
    [TextArea]
    public string descricao; 

    public void MostrarMissao()
    {
        SistemaMensagens.instancia.MostrarMensagem(descricao, 5f);
    }
}

