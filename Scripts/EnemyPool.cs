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
            GameObject obj1 = (GameObject) Instantiate(enemyPrefab);
            GameObject obj2 = (GameObject) particlePrefab;
            obj1.SetActive(false);
            obj2.SetActive(false);

            enemyPool.Enqueue(obj1);
            particlePool.Enqueue(obj2);
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
