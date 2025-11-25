using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class MissaoFazenda1 : MonoBehaviour
{
    public Button botaoColher;
    public GameObject fundoNormal;
    public GameObject fundoColhido;
    public TMP_Text textoContagem;
    public GameObject painelMensagem;
    public TMP_Text textoMensagem;
    public int cliquesNecessarios = 10;
    public Button botaoMudarDia;

    private int cliques;
    private bool colheu;
    private float shake;
    private float zoom;
    private AudioSource _audio;

    void Start()
    {
        if (fundoNormal != null) fundoNormal.SetActive(true);
        if (fundoColhido != null) fundoColhido.SetActive(false);
        if (painelMensagem != null) painelMensagem.SetActive(false);
        if (textoContagem != null) textoContagem.text = "0 / " + cliquesNecessarios;

        if (botaoColher != null) botaoColher.onClick.AddListener(Colher);

        if (botaoMudarDia != null)
        {
            botaoMudarDia.gameObject.SetActive(false);
            botaoMudarDia.onClick.AddListener(MudarDia);
        }

        _audio = GetComponent<AudioSource>();
        if (_audio == null) _audio = gameObject.AddComponent<AudioSource>();
        _audio.playOnAwake = false;
    }

    void Update()
    {
        if (shake > 0)
        {
            shake -= Time.deltaTime * 3f;
            float s = shake;
            float x = Random.Range(-s, s);
            float y = Random.Range(-s, s);
            Camera.main.transform.localPosition = new Vector3(x, y, -10);
        }
        else Camera.main.transform.localPosition = new Vector3(0, 0, -10);

        if (zoom > 0)
        {
            zoom -= Time.deltaTime * 1.5f;
            float alvo = 5 - (cliques * 0.03f);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, alvo, 10 * Time.deltaTime);
        }
        else Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 5, 3 * Time.deltaTime);
    }

    void Colher()
    {
        if (colheu) return;

        cliques++;
        if (textoContagem != null) textoContagem.text = cliques + " / " + cliquesNecessarios;
        shake = 0.1f + cliques * 0.02f;
        zoom = 1f;

        if (cliques >= cliquesNecessarios)
        {
            colheu = true;

            if (fundoNormal != null) fundoNormal.SetActive(false);
            if (fundoColhido != null) fundoColhido.SetActive(true);

            if (botaoColher != null) botaoColher.gameObject.SetActive(false);

            if (painelMensagem != null && textoMensagem != null)
            {
                painelMensagem.SetActive(true);
                textoMensagem.text = "Missão concluída.";
            }

            if (botaoMudarDia != null)
                botaoMudarDia.gameObject.SetActive(true);
        }
    }

    void MudarDia()
    {
        SceneManager.LoadScene("Dia2");
    }
}
