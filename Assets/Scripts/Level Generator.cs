using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject platformPrefab;
    [SerializeField] int startingNumberOfPlatforms = 3;
    [SerializeField] Transform platformParent;
    [SerializeField] float platformSize = 20f;
    [SerializeField] float platformSpeed = 5f;

    List<GameObject> platforms = new List<GameObject>();
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
            SinglePlatformGenerate();
        }
    }

    private void SinglePlatformGenerate()
    {
        float startPositionX = SpawnPositionCalculation();

        Vector2 spawnPosition = new Vector2(startPositionX, transform.position.y);
        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity, platformParent);

        platforms.Add(newPlatform);
    }

    private float SpawnPositionCalculation()
    {
        float startPositionX;

        if (platforms.Count == 0)
        {
            startPositionX = transform.position.x;
        }
        else
        {
            startPositionX = platforms[platforms.Count - 1].transform.position.x + platformSize;
        }

        return startPositionX;
    }

    void MovePlatforms()
    {
        for (int i = 0; i < platforms.Count; i++)
        {
            GameObject currentPlatform = platforms[i];

            currentPlatform.transform.Translate(-transform.right * (platformSpeed * Time.deltaTime));

            if (platforms[i].transform.position.x < -platformSize)
            {
                platforms.Remove(currentPlatform);
                Destroy(currentPlatform);
                SinglePlatformGenerate();
            }
        }
    }
}
