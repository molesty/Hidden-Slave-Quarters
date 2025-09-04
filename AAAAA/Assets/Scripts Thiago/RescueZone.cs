using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RescueZone : MonoBehaviour
{
    [SerializeField] Cage cage;

    void Reset()
    {
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
        if (cage == null) cage = GetComponentInParent<Cage>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            UIRescue.Instance.ShowFor(cage);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            UIRescue.Instance.Hide(cage);
    }
}
