using UnityEngine;

public class PullKey_Senzala : MonoBehaviour
{
    [Header("Config")]
    public string itemRequerido = "ferro";
    public string nomeMissao = "Pegar Chave";
    public string mensagemSem = "Você precisa de algo para puxar a chave.";
    public string mensagemCom = "Você puxou uma chave com o ferro.";

    bool jaPuxou = false;

    public void Interagir() => TryPull();
    public void OnInteract() => TryPull();
    public void Coletar() => TryPull();

    void TryPull()
    {
        if (jaPuxou)
        {
            if (SistemaMensagens.instancia != null) SistemaMensagens.instancia.MostrarMensagem("Não há mais nada aqui.");
            return;
        }

        if (!SenzalaProgress.temFerro)
        {
            if (SistemaMensagens.instancia != null) SistemaMensagens.instancia.MostrarMensagem(mensagemSem);
            else UnityEngine.Debug.Log(mensagemSem);
            return;
        }

        SenzalaProgress.temChave = true;
        jaPuxou = true;

        if (GerenciadorMissoes.instancia != null) GerenciadorMissoes.instancia.CompletarMissao(nomeMissao);
        if (SistemaMensagens.instancia != null) SistemaMensagens.instancia.MostrarMensagem(mensagemCom);
        else UnityEngine.Debug.Log(mensagemCom);

        var sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.color = new Color(1f, 1f, 1f, 0.5f);
    }
}
