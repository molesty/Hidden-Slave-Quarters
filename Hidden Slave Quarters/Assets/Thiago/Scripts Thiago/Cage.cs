using UnityEngine;

public class Cage : MonoBehaviour
{
    [Header("Objetos da Jaula")]
    [SerializeField] private GameObject bars;    
    [SerializeField] private GameObject captive; 

    [Header("Efeitos (Opcional)")]
    [SerializeField] private AudioSource sfx;      
    [SerializeField] private ParticleSystem fx;    

    public bool IsRescued { get; private set; }    

    public void Rescue()
    {
        if (IsRescued) return;
        IsRescued = true;

        if (bars) bars.SetActive(false);

        if (captive) captive.SetActive(false);

        if (fx) fx.Play();
        if (sfx) sfx.Play();
    }
}
