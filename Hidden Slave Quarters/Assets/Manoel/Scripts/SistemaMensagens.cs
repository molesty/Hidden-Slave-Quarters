using System.Diagnostics;
using UnityEngine;
using Text = UnityEngine.UI.Text;
using Debug = UnityEngine.Debug;


public class SistemaMensagens : MonoBehaviour
{
    public static SistemaMensagens instancia;
    public Text textoMensagem;
    public float tempoExibicao = 3f;

    void Awake()
    {
        if (instancia == null) instancia = this;
        else Destroy(gameObject);
    }

    public void MostrarMensagem(string msg)
    {
        Debug.Log(msg);

        if (textoMensagem != null)
        {
            textoMensagem.text = msg;
            CancelInvoke(nameof(LimparMensagem));
            Invoke(nameof(LimparMensagem), tempoExibicao);
        }
    }

    void LimparMensagem()
    {
        if (textoMensagem != null)
            textoMensagem.text = "";
    }
}
