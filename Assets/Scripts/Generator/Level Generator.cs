using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{
    [Header("Platform Settings")]
    [SerializeField] GameObject platformPrefab;
    [SerializeField] int startingNumberOfPlatforms = 3;
    [SerializeField] Transform platformParent;
    [SerializeField] float platformSize = 20f;
    [SerializeField] float platformSpeed = 5f;
    [SerializeField] float minPlatformSpeed = 5f;
    [SerializeField] float maxPlatformSpeed = 100f;

    List<GameObject> platforms = new List<GameObject>();
    void Start()
    {
        GeneratePlatforms();
    }

    void Update()
    {
        MovePlatforms();
    }

    public void SpeedUpPlatforms(float speedAmount, float duration) // PowerUp.cs
    {
        StartCoroutine(SpeedBoostCoroutine(speedAmount, duration));


        //Debug.Log("Speed Boosted");
    }

    public IEnumerator SpeedBoostCoroutine(float speedAmount, float duration) // PowerUp.cs
    {
        Debug.Log("Speed Boost Coroutine Started");
        float newPlatformSpeed = platformSpeed + speedAmount;
        newPlatformSpeed = Mathf.Clamp(newPlatformSpeed, minPlatformSpeed, maxPlatformSpeed);

        platformSpeed = newPlatformSpeed;

        yield return new WaitForSeconds(duration);

        platformSpeed -= speedAmount;
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

        Platforms platform = newPlatform.GetComponent<Platforms>(); //Burda da GetComponent<Platforms>() kullanmam gerekiyo.

        platform.Init(this);
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
