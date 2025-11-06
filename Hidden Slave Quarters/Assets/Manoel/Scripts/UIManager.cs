using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instancia;
    public GameObject painelMensagem;
    public TextMeshProUGUI textoMensagem;
    public float tempoMensagem = 2.5f;

    void Awake()
    {
        if (instancia == null) instancia = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void Mostrar(string msg)
    {
        if (painelMensagem == null || textoMensagem == null) { Debug.Log(msg); return; }
        textoMensagem.text = msg;
        painelMensagem.SetActive(true);
        CancelInvoke("Esconder");
        Invoke("Esconder", tempoMensagem);
    }

    void Esconder()
    {
        painelMensagem.SetActive(false);
    }
}
