using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject essentialObjectsPrefab;

    private void Awake()
    {
        var existingObjects = FindObjectsOfType<EssentialObjects>();
        if (existingObjects.Length == 0)
        {
            // if theres a grid spawn at center
            var spawnPos = new Vector3(0, 0, 0);

            var grid = FindObjectOfType<Grid>();
            if(grid != null)
            {
                Debug.Log($"Spawning at {grid.transform.position} cuz no grid was found");
                spawnPos = grid.transform.position;
            }

            Instantiate(essentialObjectsPrefab, spawnPos, Quaternion.identity);
        }
    }
}
