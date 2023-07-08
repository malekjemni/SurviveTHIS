using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class NPCPlayer : MonoBehaviour
{
    public float detectionRadius = 5f; 
    public LayerMask enemyLayer;
    private AIDestinationSetter destinationSetter;
    private NPCPlayerBehaviour nPCPlayerBehaviour;

    public LayerMask obstacleL;
    public Transform patrolPoint;
    public Vector3 wkPoint;
    private bool wkSet;
    public float lkpoints;


    private float stepBackDistance = 2f; 
    private float minLerpTime = 1f; 
    private float maxLerpTime = 1.5f;
    private Vector2 previousPosition;


    private void Awake()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        nPCPlayerBehaviour = GetComponent<NPCPlayerBehaviour>();
    }

    private void Update()
    {

        if (!CheckForEnemy()) Moving();

        MoveDirection();
        ReachEnemy();
    }

    private bool CheckForEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);
        if (colliders.Length > 0)
        {
            foreach (Collider2D collider in colliders)
            {
                destinationSetter.target = collider.transform;
                return true;
            }
        }
        return false;
    }


    private void ReachEnemy()
    {
        if (CheckForEnemy())
        {
            Vector3 dist = transform.position - destinationSetter.target.position;
            if (dist.magnitude < 1f)
            {
                Vector3 direction = dist.normalized;
                Vector3 targetPosition = transform.position + direction * stepBackDistance;

                float lerpTime = Random.Range(minLerpTime, maxLerpTime);
                nPCPlayerBehaviour.AttackAnimaton();
                StartCoroutine(MoveBackSmoothly(targetPosition, lerpTime));
            }
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
            Transform destination = patrolPoint;
            destination.position = wkPoint;
            destinationSetter.target = destination;

        }

        Vector3 dist = transform.position - wkPoint;
        if (dist.magnitude < 1f)
        {
            destinationSetter.target = null;
            wkSet = false;
        }

    }

    private void LookForNodes()
    {
        float randomX = Random.Range(-lkpoints, lkpoints);
        float randomY = Random.Range(-lkpoints, lkpoints);

        Vector3 patrolPointPosition = patrolPoint.transform.position;
        wkPoint = new Vector3(patrolPointPosition.x + randomX, patrolPointPosition.y + randomY, 0f);

        if (!Physics2D.OverlapCircle(patrolPointPosition, 0.2f, obstacleL))
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
