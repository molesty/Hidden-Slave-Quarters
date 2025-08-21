using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 direction;

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player atingido!");
            Destroy(gameObject);
        }
        else if (other.CompareTag("Obstacle")) 
        {
            Destroy(gameObject);
        }
    }
}
