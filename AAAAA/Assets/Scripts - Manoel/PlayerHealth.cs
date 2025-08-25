using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 10;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Player recebeu dano! Vida atual: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player morreu!");
    
    }
}
