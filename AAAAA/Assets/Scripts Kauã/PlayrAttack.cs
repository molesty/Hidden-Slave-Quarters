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
        // Ataque com botão esquerdo do mouse
        if (Input.GetMouseButtonDown(0)) // 0 = botão esquerdo
        {
            anim.SetTrigger("Atacar");
        }
    }
}