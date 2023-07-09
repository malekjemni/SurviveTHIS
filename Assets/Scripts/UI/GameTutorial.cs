using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTutorial : MonoBehaviour
{

    public GameObject player;
    public GameObject[] popups;
    private int index;
    private bool tutoEnds =false;
    void Start()
    {
        player.GetComponent<NPCPlayer>().enabled = false;
        popups[0].SetActive(true);
    }

    public void GetPopUpIndex(int pos)
    {
        index = pos;
        popups[index].SetActive(false);

        if (index < popups.Length - 1)
            popups[index + 1].SetActive(true);
        else
            player.GetComponent<NPCPlayer>().enabled = true;
    }
}
