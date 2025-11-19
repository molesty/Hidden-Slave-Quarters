using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissaoFazenda1 : MonoBehaviour
{
    public Button botaoColher;
    public GameObject fundoNormal;
    public GameObject fundoColhido;
    public TMP_Text textoContagem;

    public GameObject painelMensagem;
    public TMP_Text textoMensagem;

    public Image vignette;
    public float vignetteMax = 0.55f;

    public int cliquesNecessarios = 10;

    int cliques;
    bool colheu;
    float shake;
    float zoom;

    void Start()
    {
        fundoNormal.SetActive(true);
        fundoColhido.SetActive(false);
        painelMensagem.SetActive(false);
        textoContagem.text = "0 / " + cliquesNecessarios;
        if (vignette != null) vignette.color = new Color(0, 0, 0, 0);
        botaoColher.onClick.AddListener(Colher);
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

        if (vignette != null)
        {
            float alvo = (float)cliques / cliquesNecessarios * vignetteMax;
            vignette.color = new Color(0, 0, 0, Mathf.Lerp(vignette.color.a, alvo, 5 * Time.deltaTime));
        }
    }

    void Colher()
    {
        if (colheu) return;

        cliques++;
        textoContagem.text = cliques + " / " + cliquesNecessarios;

        shake = 0.1f + cliques * 0.02f;
        zoom = 1f;

        if (cliques >= cliquesNecessarios)
        {
            colheu = true;
            fundoNormal.SetActive(false);
            fundoColhido.SetActive(true);
            botaoColher.gameObject.SetActive(false);
            painelMensagem.SetActive(true);
            textoMensagem.text = "Plantação colhida!";
        }
    }
}
