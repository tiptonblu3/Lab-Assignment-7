using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;

public class gameManager : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject SuperEnemy;
    public GameObject SuperDuperEnemy;
    public Transform Player;
    public SceneReloader screl;
    public Settings settings;
    
    [Header("Enemy Variables")]
    public float spawnDistance = 20f;
    public float spawnRate = 3f;
    // public int maxEnemyCount = 15;

    public void Blade()
    {
        Debug.Log("Blade ability Activated.");
    }
    public void Vaccum()
    {
        Debug.Log("Vaccum ability Activated.");
    }
    public void Dash()
    {
        Debug.Log("Dash ability Activated.");
    }

    void Awake()
    {
        screl = GetComponent<SceneReloader>();
        settings.inGame = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(CreateEnemyGroup());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Game Over Sequence
    private void OnEnable()
    {
        player.OnDeath += GameOver;
    }
    private void OnDisable()
    {
        player.OnDeath -= GameOver;
    }
    private void GameOver()
    {
        screl.GameOverScreen.SetActive(true);
            screl.StoreMan.UIButtons.SetActive(true);
            Time.timeScale = 0f; // pause game
    }
    #endregion

    public void createEnemy()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPosition = Player.position + new Vector3(randomDirection.x, 0, randomDirection.y) * spawnDistance;

        float roll = Random.value; // change enemy spawn to be based on rolls
        float minutes = Time.timeSinceLevelLoad / 60f;

        GameObject enemyToSpawn = Enemy;

        if (minutes >= 2f && roll < 0.20f)
        {
            enemyToSpawn = SuperEnemy;
        }

        if (minutes >= 5f && roll < 0.05f)
        {
            enemyToSpawn = SuperDuperEnemy;
        }

        Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
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
