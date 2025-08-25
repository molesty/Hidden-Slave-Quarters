using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shootInterval = 1f;
    private float timer = 0f;
    public Transform player;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= shootInterval)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("BulletPrefab não está atribuído no EnemyShoot!");
            return;
        }

        if (player != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            Vector3 direction = (player.position - transform.position).normalized;
            bullet.GetComponent<Bullet>().direction = direction;
        }
    }
}

