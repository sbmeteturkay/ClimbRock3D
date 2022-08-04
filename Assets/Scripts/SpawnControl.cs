using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
    [SerializeField] private GameObject[] handlePrefabs;
    [SerializeField] private GameObject[] obstaclesPrefabs;
    [SerializeField] private Transform startPoint;
    [SerializeField] private float handleYpositionGap = 0.5f;
    [SerializeField] private float xLimitLeft,xLimitRight = 0.5f;
    List<GameObject> handles,obstacles;
    int currentHandleIndex=0;
    Vector3 newSpawnPositon;
    GameObject lastSpawnedHandle,lastSpawnedObstacle;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform;
        handles= new List<GameObject>();
        obstacles= new List<GameObject>();
        SpawnHandles(100);
    }

    // REPLACE SPAWN MECHANIC WITH OBJECT POOLING
    private void SpawnHandles(int index)
    {
        for (int i = 0; i < index; i++)
        {
            newSpawnPositon.y = startPoint.position.y + currentHandleIndex * handleYpositionGap;
            newSpawnPositon.x = Random.Range(xLimitLeft, xLimitRight);
            lastSpawnedHandle = Instantiate(handlePrefabs[Random.Range(0, handlePrefabs.Length)], gameObject.transform);
            lastSpawnedHandle.transform.position=newSpawnPositon;
            handles.Add(lastSpawnedHandle);
            SpawnObstacles();
            currentHandleIndex++;
        }
    }
    private void SpawnObstacles()
    {
            newSpawnPositon.y = handleYpositionGap/2+ startPoint.position.y + currentHandleIndex * handleYpositionGap;
            newSpawnPositon.x = Random.Range(xLimitLeft, xLimitRight);
            lastSpawnedObstacle = Instantiate(obstaclesPrefabs[Random.Range(0, obstaclesPrefabs.Length)], gameObject.transform);
            lastSpawnedObstacle.transform.position = newSpawnPositon;
            obstacles.Add(lastSpawnedObstacle);
    }
}
