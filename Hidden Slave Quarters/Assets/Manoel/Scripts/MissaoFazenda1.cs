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

    public int cliquesNecessarios = 10;

    private int cliques;
    private bool colheu;
    private float intensidade;

    void Start()
    {
        fundoNormal.SetActive(true);
        fundoColhido.SetActive(false);
        textoContagem.text = "0 / " + cliquesNecessarios;

        painelMensagem.SetActive(false);

        botaoColher.onClick.AddListener(Colher);
    }

    void Update()
    {
        if (intensidade > 0)
        {
            intensidade -= Time.deltaTime * 3f;
            float x = Random.Range(-intensidade, intensidade);
            float y = Random.Range(-intensidade, intensidade);
            Camera.main.transform.localPosition = new Vector3(x, y, -10);
        }
        else
        {
            Camera.main.transform.localPosition = new Vector3(0, 0, -10);
        }
    }

    void Colher()
    {
        if (colheu) return;

        cliques++;
        textoContagem.text = cliques + " / " + cliquesNecessarios;

        intensidade = 0.1f + (cliques * 0.02f);

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
