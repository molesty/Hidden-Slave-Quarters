using UnityEngine;
using TMPro;

public class SistemaMensagens : MonoBehaviour
{
    public static SistemaMensagens instancia;

    [Header("UI References")]
    public GameObject painelMensagem;
    public TextMeshProUGUI textoMensagem;
    public float tempoMensagem = 3f;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MostrarMensagem(string mensagem)
    {
        if (textoMensagem != null && painelMensagem != null)
        {
            textoMensagem.text = mensagem;
            painelMensagem.SetActive(true);
            CancelInvoke("EsconderMensagem");
            Invoke("EsconderMensagem", tempoMensagem);
        }
    }

    void EsconderMensagem()
    {
        if (painelMensagem != null)
            painelMensagem.SetActive(false);
    }
}
