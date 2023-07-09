using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUi : MonoBehaviour
{

    public GameObject[] prefabs;
    public float posX = 10f; 
    public float spawnInterval = 1f;
    public Transform spawnPoit;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }
    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            float randomInter = Random.Range(0.2f, spawnInterval);
            float randomX = Random.Range(spawnPoit.position.x - posX, spawnPoit.position.x + posX);
                int random = Random.Range(0, prefabs.Length);
                GameObject spwn = Instantiate(prefabs[random], new Vector3(randomX, spawnPoit.position.y, 0f), Quaternion.identity);
                Destroy(spwn, 2f);
            
        

            yield return new WaitForSeconds(randomInter);
        }
    }


}
