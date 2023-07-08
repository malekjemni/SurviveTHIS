using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPlayerBehaviour : MonoBehaviour
{
   private Animator animator;


    void Awake()
    {
        animator = GetComponent<Animator>();
    }



    public void MoveAnimaton(bool _state)
    {
        animator.SetBool("isMoving",_state);
    }
    public void AttackAnimaton()
    {
        animator.SetTrigger("attack");
    }
}
