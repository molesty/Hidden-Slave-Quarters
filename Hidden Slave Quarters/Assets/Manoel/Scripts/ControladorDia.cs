using UnityEngine;
using UnityEngine.UI;

public class ControladorDia : MonoBehaviour
{
    public Text textoDia;

    void Start()
    {
        if (GameManager.instancia == null)
        {
            Debug.LogWarning("GameManager não encontrado no ControladorDia");
            return;
        }

        AtualizarTextoDia();
    }

    public void ProximoDia()
    {
        if (GameManager.instancia == null) return;

        GameManager.instancia.AvancarDia();
        AtualizarTextoDia();
    }

    public void DefinirDia(int novoDia)
    {
        if (GameManager.instancia == null) return;

        GameManager.instancia.MudarDia(novoDia);
        AtualizarTextoDia();
    }

    void AtualizarTextoDia()
    {
        if (textoDia != null)
        {
            textoDia.text = "Dia " + GameManager.instancia.diaAtual;
        }
    }
}
