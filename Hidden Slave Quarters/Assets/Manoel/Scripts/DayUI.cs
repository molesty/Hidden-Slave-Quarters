using UnityEngine;
using TMPro;

public class DayUI : MonoBehaviour
{
    public TMP_Text textoDia;

    void Update()
    {
        if (textoDia == null || DayManager.instancia == null) return;
        textoDia.text = "Dia " + DayManager.instancia.diaAtual + (DayManager.instancia.ehDia ? " (Dia)" : " (Noite)");
    }
}
