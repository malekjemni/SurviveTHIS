using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawer : MonoBehaviour
{
    public GameObject[] mobList;
    public Vector3 pos;
    public LayerMask mask;

    private GameObject pendingObject;  
    private RaycastHit hit;


    private void Update()
    {
        if (pendingObject != null)
        {
            pendingObject.transform.position = pos;

            if (Input.GetMouseButtonDown(0))
            {
                PlaceMob();
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);      
        pos = spawnPosition;
        pos.z = 0; 
    }

    public void PlaceMob()
    {
        pendingObject = null;
    }
    public void SelectedMobBar(int index)
    {
        pendingObject = Instantiate(mobList[index], pos, Quaternion.identity);
    }

}
