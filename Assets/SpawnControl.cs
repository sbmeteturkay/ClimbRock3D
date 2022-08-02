using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
    [SerializeField] private GameObject[] handlePrefabs;
    [SerializeField] private Transform startPoint;
    [SerializeField] private float handleYpositionGap = 0.5f;
    [SerializeField] private float xLimitLeft,xLimitRight = 0.5f;
    int currentHandleIndex=0;
    Vector3 newSpawnPositon;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform;
        SpawnHandles(100);
    }

    // Update is called once per frame
    private void SpawnHandles(int index)
    {
        for (int i = 0; i < index; i++)
        {
            newSpawnPositon.y = startPoint.position.y + currentHandleIndex * handleYpositionGap;
            newSpawnPositon.x = Random.Range(xLimitLeft, xLimitRight);
            Instantiate(handlePrefabs[Random.Range(0, handlePrefabs.Length)],gameObject.transform).transform.position=newSpawnPositon;
            currentHandleIndex++;
        }
    } 
}
