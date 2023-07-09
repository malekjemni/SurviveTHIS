using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTutorial : MonoBehaviour
{

    public GameObject player;
    public GameObject[] popups;
    private int index;
    void Start()
    {
        Time.timeScale = System.Convert.ToInt32(false);
        popups[0].SetActive(true);
    }

    public void GetPopUpIndex(int pos)
    {
        index = pos;
        popups[index].SetActive(false);

        if (index < popups.Length - 1)
            popups[index + 1].SetActive(true);
        else
            Time.timeScale = System.Convert.ToInt32(true);
    }
}
