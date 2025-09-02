using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    [SerializeField] private GameObject powerWordPrefab; // Prefab de um projectile (raio de luz em pixel art)
    [SerializeField] private Transform attackPoint; // Ponto de origem do ataque (ex.: mão do Abdias)
    [SerializeField] private float attackCooldown = 1f; // Tempo entre ataques
    private float lastAttackTime;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Time.time > lastAttackTime + attackCooldown) // Tecla F para atacar
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    private void Attack()
    {
        // Instancia o prefab do raio
        GameObject powerWord = Instantiate(powerWordPrefab, attackPoint.position, Quaternion.identity);
        // Adicione física: powerWord.GetComponent<Rigidbody2D>().velocity = transform.right * 10f; // Direita

        // Efeito educativo: Mostre uma citação aleatória na UI (use um TextMeshPro)
        Debug.Log("Citação de Abdias: 'O racismo é uma estrutura que deve ser desmontada'");
    }
}