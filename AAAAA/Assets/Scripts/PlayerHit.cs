using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    [SerializeField] private GameObject powerWordPrefab; // Prefab de um projectile (raio de luz em pixel art)
    [SerializeField] private Transform attackPoint; // Ponto de origem do ataque (ex.: m�o do Abdias)
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
        // Adicione f�sica: powerWord.GetComponent<Rigidbody2D>().velocity = transform.right * 10f; // Direita

        // Efeito educativo: Mostre uma cita��o aleat�ria na UI (use um TextMeshPro)
        Debug.Log("Cita��o de Abdias: 'O racismo � uma estrutura que deve ser desmontada'");
    }
}