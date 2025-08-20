using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            anim.SetTrigger("Attack");
        }
    }
}
