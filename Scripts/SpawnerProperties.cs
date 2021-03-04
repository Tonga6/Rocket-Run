using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using Random = UnityEngine.Random;

public class SpawnerProperties : MonoBehaviour
{
    [SerializeField]
    GameObject player, rocket, enemyPool;
    [SerializeField]
    float waveTimer, spawnTimer, waveDelay, spawnDelay;
    [SerializeField]
    int waveN, numToSpawn, spawnLoc, maxRange, screenX, screenY, nextEnemyNum;

    Vector3 spawnPoint;

    void Start()
    {
        enemyPool = GameObject.FindGameObjectWithTag("Manager");
        waveTimer = waveDelay;
        spawnTimer = spawnDelay;
    }

    void Update()
    {
            Spawn();             
    }

    private void Spawn()
    {
        if (waveTimer < 0) {
            spawnTimer -= Time.deltaTime;
            if(spawnTimer < 0) {
                if (numToSpawn > 0) {

                    //Spawn enemy at random pos at one of the four borders
                    spawnLoc = Random.Range(0, 4);
                    switch (spawnLoc) {
                        case 0: spawnPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, Random.Range(0, Screen.height), 0));
                            break;
                        case 1:
                            spawnPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Random.Range(0, Screen.height), 0));
                            break;
                        case 2:
                            spawnPoint = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), 0, 0));
                            break;
                        case 3:
                            spawnPoint = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Screen.height, 0));
                            break;
                        default:
                            break;
                    }
                    
                    rocket = enemyPool.GetComponent<EnemyPool>().getRocket();
                    if (rocket != null) {
                        rocket.transform.position = new Vector3 (spawnPoint.x, spawnPoint.y, 0);

                        var dir = player.transform.position - rocket.transform.position;
                        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
                        rocket.transform.rotation = (Quaternion.Euler(0, 0, angle));

                        rocket.SetActive(true);
                    }
                    numToSpawn--;
                }
                else {
                    waveTimer = waveDelay;
                    waveN++;
                    if (waveN < 3)
                        nextEnemyNum = 1;
                    else if (waveN < 5)
                        nextEnemyNum = 2;
                    else if (waveN < 10)
                        nextEnemyNum = 4;
                    else if (waveN < 15)
                        nextEnemyNum = 5;
                    else if (waveN < 20)
                        nextEnemyNum = 7;
                    else if (waveN < 30)
                        nextEnemyNum = 12;
                    else
                        nextEnemyNum = 20;

                    numToSpawn = nextEnemyNum;
                }
                spawnTimer = spawnDelay;
            }            
        }
        else
            waveTimer -= Time.deltaTime;        
    }
}
