using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBehaviour : MonoBehaviour
{
    private Animator animator;
    private Vector2 previousPosition;


    void Awake()
    {
        animator = GetComponent<Animator>();
        MoveAnimaton(true);
    }
    private void Update()
    {
        MoveDirection();
    }

    private void MoveDirection()
    {
        Vector2 currentPosition = transform.position;
        Vector2 movementDirection = currentPosition - previousPosition;
        previousPosition = currentPosition;
        transform.localScale = new Vector3(-Mathf.Sign(movementDirection.x), transform.localScale.y, transform.localScale.z);
    }
    public void MoveAnimaton(bool _state)
    {
        animator.SetBool("isMoving", _state);
    }
    public void AttackAnimaton()
    {
        animator.SetTrigger("attack");
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            AttackAnimaton();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            MoveAnimaton(true);
        }
    }
}
