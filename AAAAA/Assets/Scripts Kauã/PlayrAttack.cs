using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Ataque com bot�o esquerdo do mouse
        if (Input.GetMouseButtonDown(0)) // 0 = bot�o esquerdo
        {
            anim.SetTrigger("Atacar");
        }
    }
}