using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class NPCPlayer : MonoBehaviour
{
    public float detectionRadius = 5f;
    private AIDestinationSetter destinationSetter;
    private NPCPlayerBehaviour nPCPlayerBehaviour;

    public LayerMask obstacleL, enemyLayer;
    public Transform patrolPoint;
    public Vector3 wkPoint;
    private bool wkSet;
    public float lkpoints;

    private Vector2 previousPosition;


    public float checkCirleRadius;
    public bool canAttack = false;
    public float attackCD;

    private bool isAttacking = false;

    public Transform closestEnemy = null;
    public Transform destination = null;


    private void Awake()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        nPCPlayerBehaviour = GetComponent<NPCPlayerBehaviour>();
        destination = new GameObject("FollowPoint").transform;

    }

    private void Update()
    {

        if (!CheckForEnemy()) Moving();
        else ReachEnemy();

        MoveDirection();
    }

    private bool CheckForEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);
        if (colliders.Length > 0)
        {
            
            float minDistance = Mathf.Infinity;

            foreach (Collider2D collider in colliders)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = collider.transform;
                }
            }

            if (closestEnemy != null)
            {
                destinationSetter.target = closestEnemy;
                nPCPlayerBehaviour.MoveAnimaton(true);
                return true;
            }
        }

        return false;
    }



    private void ReachEnemy()
    {
        Vector3 dist = transform.position - destinationSetter.target.position;
        if (dist.magnitude < checkCirleRadius)
        {
            if (canAttack)
            {
                StartCoroutine(AttackSequence());
                // perform attck here
                nPCPlayerBehaviour.AttackAnimaton();
                
            }
            else
            {
                EscapeFormTarget();
            }
                

        }

    }
    private void EscapeFormTarget()
    {
        Vector3 enemyPosition = destinationSetter.target.position;
        Vector3 oppositeDestination = transform.position - (enemyPosition - transform.position);
        destination.position = oppositeDestination;
        destinationSetter.target = destination;
        nPCPlayerBehaviour.MoveAnimaton(true);

    }
    private IEnumerator AttackSequence()
    {
        if (!isAttacking)
        {
            isAttacking = true;

        
            nPCPlayerBehaviour.AttackAnimaton();

            // Wait for the attack animation to finish
            yield return new WaitForSeconds(attackCD);
            canAttack = false;

            float lerpTime = Random.Range(0.1f, 0.5f);
            Vector3 dist = transform.position - destinationSetter.target.position;
            Vector3 direction = dist.normalized;
            Vector3 targetPosition = transform.position + direction * 0.1f;
            StartCoroutine(MoveBackSmoothly(targetPosition, lerpTime));

            isAttacking = false;
        }
    }
  
    private IEnumerator MoveBackSmoothly(Vector3 targetPosition, float lerpTime)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < lerpTime)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / lerpTime);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }


    private void MoveDirection()
    {
        Vector2 currentPosition = transform.position;
        Vector2 movementDirection = currentPosition - previousPosition;
        previousPosition = currentPosition;
        transform.localScale = new Vector3(-Mathf.Sign(movementDirection.x), transform.localScale.y, transform.localScale.z);
    }

    private void Moving()
    {
        if (!wkSet) LookForNodes();
        if (wkSet)
        {
            nPCPlayerBehaviour.MoveAnimaton(true);          
            destination.position = wkPoint;
            destinationSetter.target = destination;

        }

        Vector3 dist = transform.position - wkPoint;
        if (dist.magnitude < 1f)
        {
            wkSet = false;
        }

    }

    private void LookForNodes()
    {

        float randomX = Random.Range(-lkpoints, lkpoints);
        float randomY = Random.Range(-lkpoints, lkpoints); ;

        wkPoint = new Vector3(patrolPoint.position.x + randomX, patrolPoint.position.y + randomY, 0f);

        if (!Physics2D.OverlapCircle(transform.position, 0.2f, obstacleL))
        {
            wkSet = true;
        }
    }

   
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(wkPoint, transform.right * 0.5f);
    }

}
