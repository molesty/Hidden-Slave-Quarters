using UnityEngine;

public class ObjetoInterativo : MonoBehaviour
{
    [Header("Config")]
    public string nomeInteracao = "Objeto";
    public bool completaMissao = false;
    public string nomeMissao = "Missao";
    public string mensagemAoInteragir = "Você interagiu.";

    [Header("Opcional")]
    public bool desaparecerAoInteragir = true;

    public void Interagir()
    {   
        UIManager.instancia?.Mostrar(mensagemAoInteragir);

        if (completaMissao)
        {
            GerenciadorMissoes.instancia?.CompletarMissao(nomeMissao);
        }

        if (desaparecerAoInteragir)
            gameObject.SetActive(false);
    }
}
