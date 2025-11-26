using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class SistemaMensagens : MonoBehaviour
{
    public static SistemaMensagens instancia;

    [Header("UI de Mensagem")]
    public GameObject painelMensagem; 
    public TMP_Text textoMensagem;   

    void Awake()
    {

        instancia = this;


        if (painelMensagem == null)
            painelMensagem = GameObject.Find("PainelMensagem");

        if (textoMensagem == null && painelMensagem != null)
            textoMensagem = painelMensagem.GetComponentInChildren<TMP_Text>();

 
        if (painelMensagem == null || textoMensagem == null)
            Debug.LogWarning("SistemaMensagens: Painel ou texto de mensagem não encontrados na cena.");
        else
            painelMensagem.SetActive(false);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene cena, LoadSceneMode modo)
    {

        if (painelMensagem == null)
            painelMensagem = GameObject.Find("PainelMensagem");

        if (textoMensagem == null && painelMensagem != null)
            textoMensagem = painelMensagem.GetComponentInChildren<TMP_Text>();


        if (painelMensagem != null)
            painelMensagem.SetActive(false);


        StartCoroutine(EsperarParaMostrarMissao());
    }

    IEnumerator EsperarParaMostrarMissao()
    {
        yield return null;

        if (GameManager.instancia != null && GameManager.instancia.missaoScript != null)
        {
            MostrarMensagem(GameManager.instancia.missaoScript.descricao, 4f);
        }
    }


    public void MostrarMensagem(string mensagem, float duracao = 2.5f)
    {
        if (painelMensagem == null || textoMensagem == null)
        {
            Debug.LogWarning("SistemaMensagens: Painel ou texto de mensagem não configurados.");
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
