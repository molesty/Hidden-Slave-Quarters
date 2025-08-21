using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator anim;
    public AttackHitbox attackHitbox; // referência direta ao script

    void Start()
    {
        anim = GetComponent<Animator>();
        attackHitbox.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
            StartCoroutine(DoAttack());
        }
    }

    System.Collections.IEnumerator DoAttack()
    {
        attackHitbox.gameObject.SetActive(true);
        attackHitbox.canHit = true;

        yield return new WaitForSeconds(0.3f); // tempo do golpe ativo

        attackHitbox.canHit = false;
        attackHitbox.gameObject.SetActive(false);
    }
}
