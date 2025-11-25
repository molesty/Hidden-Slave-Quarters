using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Debug = UnityEngine.Debug;

public class SenzalaMissionController : MonoBehaviour
{
    public Button botaoFerro;
    public Button botaoPuxarChave;
    public Button botaoPorta;
    public GameObject painelMensagem;
    public TMP_Text textoMensagemTMP;
    public float tempoMensagem = 2.5f;
    public string cenaDestino = "Fazenda";
    public GameObject backgroundClosed;
    public GameObject backgroundOpen;
    public AudioClip doorSlamClip;
    public float delayAfterSound = 0.4f;

    AudioSource _audioSource;
    bool temFerro = false;
    bool temChave = false;

    void Awake()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        ConectarBotoes();
        AtualizarUIInicial();
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null) _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.spatialBlend = 0f;
        if (backgroundOpen != null) backgroundOpen.SetActive(false);
        if (backgroundClosed != null) backgroundClosed.SetActive(true);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        RemoverListeners();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Senzala")
        {
            ConectarBotoes();
            AtualizarUIInicial();
            if (backgroundOpen != null) backgroundOpen.SetActive(false);
            if (backgroundClosed != null) backgroundClosed.SetActive(true);
        }
        else
        {
            RemoverListeners();
        }
    }

    void ConectarBotoes()
    {
        if (botaoFerro == null) botaoFerro = FindButtonByName("BotaoFerro");
        if (botaoPuxarChave == null) botaoPuxarChave = FindButtonByName("BotaoChave");
        if (botaoPorta == null) botaoPorta = FindButtonByName("BotaoPorta");
        if (botaoFerro != null) botaoFerro.onClick.AddListener(OnClickFerro);
        if (botaoPuxarChave != null) botaoPuxarChave.onClick.AddListener(OnClickPuxarChave);
        if (botaoPorta != null) botaoPorta.onClick.AddListener(OnClickPorta);
    }

    void RemoverListeners()
    {
        if (botaoFerro != null) botaoFerro.onClick.RemoveListener(OnClickFerro);
        if (botaoPuxarChave != null) botaoPuxarChave.onClick.RemoveListener(OnClickPuxarChave);
        if (botaoPorta != null) botaoPorta.onClick.RemoveListener(OnClickPorta);
    }

    Button FindButtonByName(string nome)
    {
        var go = GameObject.Find(nome);
        if (go != null) return go.GetComponent<Button>();
        return null;
    }

    void AtualizarUIInicial()
    {
        if (botaoFerro != null) botaoFerro.interactable = !temFerro;
        if (botaoPuxarChave != null) botaoPuxarChave.interactable = !temChave;
    }

    void OnClickFerro()
    {
        if (temFerro)
        {
            Debug.Log("Já tem ferro.");
            Mostrar("Você já pegou o ferro.");
            return;
        }
        temFerro = true;
        Debug.Log("Pegou o ferro (Senzala).");
        Mostrar("Você encontrou um ferro enferrujado.");
        if (botaoFerro != null) botaoFerro.interactable = false;
    }

    void OnClickPuxarChave()
    {
        if (!temFerro)
        {
            Debug.Log("Tentou puxar a chave sem ferro.");
            Mostrar("Você precisa de algo para puxar a chave.");
            return;
        }
        if (temChave)
        {
            Debug.Log("Já puxou a chave.");
            Mostrar("Você já puxou a chave.");
            return;
        }
        temChave = true;
        Debug.Log("Puxou a chave (Senzala).");
        Mostrar("Você puxou uma chave com o ferro.");
        if (botaoPuxarChave != null) botaoPuxarChave.interactable = false;
    }

    void OnClickPorta()
    {
        if (!temChave)
        {
            Debug.Log("Porta trancada.");
            Mostrar("A porta está trancada.");
            return;
        }
        Debug.Log("Abrindo porta e mostrando background aberto...");
        if (backgroundClosed != null) backgroundClosed.SetActive(false);
        if (backgroundOpen != null) backgroundOpen.SetActive(true);
        float waitTime = 0f;
        if (doorSlamClip != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(doorSlamClip);
            waitTime = doorSlamClip.length;
        }
        waitTime += delayAfterSound;
        StartCoroutine(OpenDoorThenChangeScene(waitTime));
    }

    System.Collections.IEnumerator OpenDoorThenChangeScene(float waitSec)
    {
        if (waitSec > 0f) yield return new WaitForSeconds(waitSec);
        else yield return null;
        Mostrar("Você segue para a fazenda...");
        if (GameManager.instancia != null) GameManager.instancia.MudarCena(cenaDestino);
        else SceneManager.LoadScene(cenaDestino);
    }

    void Mostrar(string msg)
    {
        if (painelMensagem != null && textoMensagemTMP != null)
        {
            painelMensagem.SetActive(true);
            textoMensagemTMP.text = msg;
            CancelInvoke(nameof(EsconderMensagem));
            Invoke(nameof(EsconderMensagem), tempoMensagem);
        }
        else
        {
            Debug.Log(msg);
        }
    }

    void EsconderMensagem()
    {
        if (painelMensagem != null) painelMensagem.SetActive(false);
    }

    public bool TemFerro() => temFerro;
    public bool TemChave() => temChave;

    public void ResetarMissao()
    {
        temFerro = false;
        temChave = false;
        if (botaoFerro != null) botaoFerro.interactable = true;
        if (botaoPuxarChave != null) botaoPuxarChave.interactable = true;
        if (backgroundOpen != null) backgroundOpen.SetActive(false);
        if (backgroundClosed != null) backgroundClosed.SetActive(true);
        Debug.Log("Missão da senzala resetada.");
    }
}
