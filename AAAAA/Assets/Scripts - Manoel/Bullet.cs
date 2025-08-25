using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 direction;
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            //other.GetComponent<PlayerHealth>().TakeDamage(1);

            Destroy(gameObject);
        }
    }
}
