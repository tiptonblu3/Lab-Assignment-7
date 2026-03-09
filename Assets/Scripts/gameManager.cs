using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameManager : MonoBehaviour
{
    public GameObject Enemy;
    public Transform Player;
    
    [Header("Enemy Variables")]
    public float spawnDistance = 20f;
    public float spawnRate = 3f;
    // public int maxEnemyCount = 15;

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(CreateEnemyGroup());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameOver()
    {
        Player.gameObject.SetActive(false); // deactivates player object when health is 0 
    }

    public void createEnemy()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPosition = Player.position + new Vector3(randomDirection.x, 0, randomDirection.y) * spawnDistance;

        Instantiate(Enemy, spawnPosition, Quaternion.identity); 

        

    }


    IEnumerator CreateEnemyGroup() 
    {
        // while loop will be implemented here so that, while the game is still active (or player is still alive)
        // max enemy count increases 
        
        while (true)
        {
            createEnemy();

            spawnRate *= 0.99f;

            yield return new WaitForSeconds(spawnRate);
        }
        
        
    }
}
