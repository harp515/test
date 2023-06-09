using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;
    
    public float maxSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;
    public Text scoreText;

    private void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay > maxSpawnDelay) {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(0.5f, 3f);
            curSpawnDelay = 0;
        }

        Player1 playerLogic = player.GetComponent<Player1>();
        scoreText.text =string.Format("Score: " + "{0:n0}", playerLogic.score);
    }
    void SpawnEnemy()
    {
        int ranEnemy = Random.Range(0, 3);
        int ranPoint = Random.Range(0, 9);
        GameObject enemy = Instantiate(enemyObjs[ranEnemy], spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;

        if(ranPoint == 5 || ranPoint == 6) { //Right Spawn
            enemy.transform.Rotate(Vector3.back*75);
            rigid.velocity = new Vector2(enemyLogic.speed*(-1), -1);
        }
        else if(ranPoint == 7 || ranPoint == 8) { //Left Spawn
            enemy.transform.Rotate(Vector3.forward*75);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else { //FrontSpawn
            rigid.velocity = new Vector2(0, enemyLogic.speed*(-1));
        }
    }
    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f);
    }
    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 3.25f;
        player.SetActive(true);
    }
}