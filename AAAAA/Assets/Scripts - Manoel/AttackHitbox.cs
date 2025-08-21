using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [HideInInspector] public bool canHit = false; // garante que PlayerCombat possa acessar

    void OnTriggerEnter2D(Collider2D other)
    {
        if (canHit && other.CompareTag("Item"))
        {
            Destroy(other.gameObject);
            Debug.Log("Item destruído pelo ataque!");
        }
    }
}
