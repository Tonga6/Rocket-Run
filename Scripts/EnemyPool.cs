using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField]
    static Queue<GameObject> enemyPool, particlePool;
    [SerializeField]
    GameObject enemyPrefab, particlePrefab;
    [SerializeField]
    int poolSize;
    void Start()
    {
        enemyPool = new Queue<GameObject>();
        particlePool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++) {
            GameObject enemy = (GameObject) Instantiate(enemyPrefab);
            GameObject particle = (GameObject) particlePrefab;
            enemy.SetActive(false);
            particle.SetActive(false);

            enemyPool.Enqueue(enemy);
            particlePool.Enqueue(particle);
        }
    }

    public GameObject getRocket()
    {
        if (enemyPool.Count > 0) 
            return enemyPool.Dequeue();
        
        return null;
    }

    public void InstantiateParticle(Transform enemyTransform)
    {
        if(particlePool.Count > 0) { 
            GameObject particle = particlePool.Dequeue();
            particle.transform.position = enemyTransform.position;
            particle.SetActive(true);
        }
    }
    public void addToEnemyQueue(GameObject enemy)
    {
        enemyPool.Enqueue(enemy);
        enemy.SetActive(false);
    }
    public void addToParticleQueue(GameObject particle)
    {
        enemyPool.Enqueue(particle);
        particle.SetActive(false);
    }
}
