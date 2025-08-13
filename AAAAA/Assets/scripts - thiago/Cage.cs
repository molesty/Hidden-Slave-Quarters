using UnityEngine;

public class Cage : MonoBehaviour
{
    [Header("Objetos da Jaula")]
    [SerializeField] private GameObject bars;    // grades visíveis
    [SerializeField] private GameObject captive; // pessoa dentro da jaula

    [Header("Efeitos (Opcional)")]
    [SerializeField] private AudioSource sfx;       // som de libertação
    [SerializeField] private ParticleSystem fx;     // partículas ao libertar

    public bool IsRescued { get; private set; }    // flag de jaula já aberta

    public void Rescue()
    {
        if (IsRescued) return;
        IsRescued = true;

        // Desativa as grades
        if (bars) bars.SetActive(false);

        // Faz a pessoa desaparecer
        if (captive) captive.SetActive(false);

        // Toca partículas e som
        if (fx) fx.Play();
        if (sfx) sfx.Play();
    }
}
