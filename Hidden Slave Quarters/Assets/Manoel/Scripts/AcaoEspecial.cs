using UnityEngine;

public class AcaoEspecial : MonoBehaviour
{
    [Header("Configuracao")]
    public string mensagem;

    public void ExecutarAcao()
    {
        SistemaMensagens.instancia.MostrarMensagem(mensagem);
    }
}