using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class NPCPlayer : MonoBehaviour
{
    public float detectionRadius = 5f; 
    public LayerMask enemyLayer;
    private AIDestinationSetter destinationSetter;

    public LayerMask obstacleL;
    public Transform patrolPoint;
    public Vector3 wkPoint;
    private bool wkSet;
    public float lkpoints;


    private void Awake()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();     
    }

    private void Update()
    {

        if (!CheckForEnemy()) Moving();
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

    private void Moving()
    {
        if (!wkSet) LookForNodes();
        if (wkSet)
        {
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
            Debug.Log("rgergr");
            wkSet = true;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(wkPoint, transform.right * 0.5f);
    }

}
