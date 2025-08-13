using UnityEngine;

public class Cage : MonoBehaviour
{
    [SerializeField] GameObject bars;          // objeto visual das grades
    [SerializeField] GameObject captive;       // pessoa dentro da jaula
    [SerializeField] Animator captiveAnimator; // opcional: anima "Free"
    [SerializeField] AudioSource sfx;          // opcional
    [SerializeField] ParticleSystem fx;        // opcional

    public bool IsRescued { get; private set; }

    public void Rescue()
    {
        if (IsRescued) return;
        IsRescued = true;

        if (bars) bars.SetActive(false);

        if (captiveAnimator)
            captiveAnimator.SetTrigger("Free");

        if (fx) fx.Play();
        if (sfx) sfx.Play();

        // Ex.: fazer a pessoa sair andando e desativar colis�o dela
        if (captive)
        {
            var rb = captive.GetComponent<Rigidbody2D>();
            if (rb) rb.linearVelocity = new Vector2(1.5f, 0f);
            var col = captive.GetComponent<Collider2D>();
            if (col) col.enabled = false;
        }

        // (Opcional) contabilizar: GameManager.RescuedCount++;
    }
}
