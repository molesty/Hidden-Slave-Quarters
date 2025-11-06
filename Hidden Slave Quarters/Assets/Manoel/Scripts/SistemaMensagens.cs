using UnityEngine;
using TMPro;

public class SistemaMensagens : MonoBehaviour
{
    public static SistemaMensagens instancia;
    public TMP_Text textoMensagem;
    public GameObject painelMensagem;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MostrarMensagem(string mensagem, float duracao = 2.5f)
    {
        if (painelMensagem == null || textoMensagem == null)
        {
            UnityEngine.Debug.LogWarning("Painel ou texto de mensagem não configurados.");
            return;
        }

        textoMensagem.text = mensagem;
        painelMensagem.SetActive(true);
        CancelInvoke(nameof(FecharMensagem));
        Invoke(nameof(FecharMensagem), duracao);
    }

    void FecharMensagem()
    {
        if (painelMensagem != null)
            painelMensagem.SetActive(false);
    }
}
