using UnityEngine;

public class PistaAmbiente : MonoBehaviour
{
    [Header("Configurações da Pista")]
    public string descricaoPista;
    public int valorCultural = 10;
    public bool revelada = false;

    [Header("Efeitos Visuais")]
    public ParticleSystem particulasDescoberta;
    public GameObject luzDestaque;

    void Start()
    {
        if (luzDestaque != null)
            luzDestaque.SetActive(false);
    }

    public void RevelarPista()
    {
        if (revelada) return;

        revelada = true;

        if (particulasDescoberta != null)
            particulasDescoberta.Play();

        if (luzDestaque != null)
            luzDestaque.SetActive(true);

        UIManager.instancia.MostrarMensagem($"Pista encontrada: {descricaoPista}");

        Debug.Log($"Pista revelada: {descricaoPista}");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !revelada)
        {
            RevelarPista();
        }
    }
}