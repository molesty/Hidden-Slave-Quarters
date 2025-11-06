using UnityEngine;

public class SenzalaNightNPC : MonoBehaviour
{
    public int diaParaLibertar = 2;
    bool jaFalou = false;

    void Update()
    {
        if (jaFalou) return;
        if (DayManager.instancia == null) return;
        if (!DayManager.instancia.ehDia && DayManager.instancia.diaAtual >= diaParaLibertar)
        {
            jaFalou = true;
            SistemaMensagens.instancia?.MostrarMensagem("Ele sussurra: Vou te libertar.", 2f);
            GerenciadorMissoes.instancia?.CompletarMissao("Encontro à noite");
            SenzalaProgress.portaDestravada = true;
        }
    }
}
