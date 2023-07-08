using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobTarget : MonoBehaviour
{
    private AIDestinationSetter destinationSetter;
    private void Awake()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        destinationSetter.target = GameObject.FindWithTag("Player").transform;

    }
}
