using UnityEngine;
using TMPro;
using System.Diagnostics;
using Debug = UnityEngine.Debug;


public class UIManager : MonoBehaviour
{
    public static UIManager instancia;
    public GameObject painelMensagem;
    public TextMeshProUGUI textoMensagem;
    public float tempoMensagem = 2.5f;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

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