using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    public GameObject Obstacle1, Obstacle2, Obstacle3;
    [HideInInspector]
    public float obstacleSpawnInterval = 2.5f;

    void Start()
    {
        StartCoroutine("SpawnObstacles");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().isGameOver)
        {
            StopCoroutine("SpawnObstacles");
        }
    }

    public void SpawnObstacle()
    {
        int random = Random.Range(1, 4);

        if (random == 1)
        {
            Instantiate(Obstacle1, new Vector3(transform.position.x, -1.14f, 0), Quaternion.identity);
        }
        else if (random == 2)
        {
            Instantiate(Obstacle2, new Vector3(transform.position.x, -1.14f, 0), Quaternion.identity);
        }
        else if (random == 3)
        {
            Instantiate(Obstacle3, new Vector3(transform.position.x, -1.14f, 0), Quaternion.identity);
        }
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            SpawnObstacle();
            yield return new WaitForSeconds(obstacleSpawnInterval);
        }
    }
}
