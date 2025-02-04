using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject platformPrefab;
    [SerializeField] int startingNumberOfPlatforms = 3;
    [SerializeField] Transform platformParent;
    [SerializeField] float platformSize = 20f;
    [SerializeField] float platformSpeed = 5f;

    GameObject[] platforms = new GameObject[3];
    void Start()
    {
        GeneratePlatforms();
    }

    void Update()
    {
        MovePlatforms();
    }

    void GeneratePlatforms()
    {
        for (int i = 0; i < startingNumberOfPlatforms; i++)
        {
            float startPositionX = SpawnPositionCalculation(i);

            Vector2 spawnPosition = new Vector2(startPositionX, transform.position.y);
            GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity, platformParent);

            platforms[i] = newPlatform;
        }
    }

    private float SpawnPositionCalculation(int i)
    {
        float startPositionX;

        if (i == 0)
        {
            startPositionX = transform.position.x;
        }
        else
        {
            startPositionX = transform.position.x + i * platformSize;
        }

        return startPositionX;
    }

    void MovePlatforms()
    {
        for (int i = 0; i < platforms.Length; i++)
        {
            GameObject currentPlatform = platforms[i];

            platforms[i].transform.position = new Vector2(platforms[i].transform.position.x - platformSpeed * Time.deltaTime, platforms[i].transform.position.y);

            if (platforms[i].transform.position.x < -platformSize)
            {
                /* platforms.(currentPlatform);
                Destroy(currentPlatform);
                GeneratePlatforms(); */
            }
        }
    }
}
