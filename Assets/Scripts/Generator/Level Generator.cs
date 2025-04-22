using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{
    [Header("Platform Settings")]
    [SerializeField] GameObject platformPrefab;
    [SerializeField] GameObject startingPlatformPrefab;
    [SerializeField] int startingNumberOfPlatforms = 3;
    [SerializeField] Transform platformParent;
    [SerializeField] public float platformSize = 20f;
    [SerializeField] public float platformSpeed = 5f;
    [SerializeField] float minPlatformSpeed = 10f;
    [SerializeField] float maxPlatformSpeed = 100f;

    private float lastSpeedBoostAmount;
    int platformSpawned = 0;
    List<GameObject> platforms = new List<GameObject>();
    private bool gameStarted = false;
    private float desiredPlatformSpeed;
    private Coroutine speedBoostCoroutine;



    void OnEnable()
    {
        PlayerHealth.OnHealthChanged += OnHealthChanged;
    }
    void OnDisable()
    {
        PlayerHealth.OnHealthChanged -= OnHealthChanged;
    }


    void Start()
    {
        desiredPlatformSpeed = platformSpeed;
        platformSpeed = 0f;
        GeneratePlatforms();
    }

    void Update()
    {
        bool flowControl = CheckSpaceIsPressed();
        if (!flowControl)
        {
            return;
        }

        MovePlatforms();

    }

    private bool CheckSpaceIsPressed()
    {
        if (!gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameStarted = true;
                platformSpeed = desiredPlatformSpeed;  // Oyuna başlandığında asıl hız değerini geri yükle.
                Debug.Log("Oyun Başladı, platform hızı: " + platformSpeed);
            }
            else
            {
                return false; // space tuşuna basılmadıysa, diğer işlemleri yapma.
            }
        }

        return true;
    }

    public void SpeedUpPlatforms(float speedAmount, float duration) // PowerUp.cs
    {
        //StartCoroutine(SpeedBoostCoroutine(speedAmount, duration));

        if (speedBoostCoroutine != null)
        StopCoroutine(speedBoostCoroutine);

        speedBoostCoroutine = StartCoroutine(SpeedBoostCoroutine(speedAmount, duration));

        //Debug.Log("Speed Boosted");
    }

    public IEnumerator SpeedBoostCoroutine(float speedAmount, float duration) // PowerUp.cs
    {
        // speedAmount'ı saklıyoruz
        lastSpeedBoostAmount = speedAmount;

        // hızı arttır
        platformSpeed = Mathf.Clamp(platformSpeed + speedAmount, minPlatformSpeed, maxPlatformSpeed);
        Debug.Log("Speed Boost Applied: " + platformSpeed);

        yield return new WaitForSeconds(duration);

        // süre dolunca aynı miktarda geri çek
        platformSpeed = Mathf.Max(platformSpeed - lastSpeedBoostAmount, minPlatformSpeed);
        Debug.Log("Speed Boost Ended: " + platformSpeed);
    }

        public void StopSpeedBoost()
    {
        if (speedBoostCoroutine != null)
        {
            StopCoroutine(speedBoostCoroutine);
            speedBoostCoroutine = null;
        }
        platformSpeed = minPlatformSpeed;
    }

    void OnHealthChanged(int health)
    {
        // eğer oyun bitti ise veya henüz hiç boost değeri yoksa çık
        if (health <= 0 || lastSpeedBoostAmount <= 0f) return;

        // boost kadar yavaşlat; minPlatformSpeed'in altına inmesin
        platformSpeed = Mathf.Max(platformSpeed - lastSpeedBoostAmount, minPlatformSpeed);
        Debug.Log($"Oyuncu engele çarptı! Platform hızı: {platformSpeed}");
        
    }

   

    GameObject CreatePlatform(GameObject prefab, Vector2 position)
    {
        GameObject newPlatform = Instantiate(prefab, position, Quaternion.identity, platformParent);
        platforms.Add(newPlatform);

        Platforms platform = newPlatform.GetComponent<Platforms>();
        if (platform == null)
        {
            return null;
        }
        platform.Init(this);
        platformSpawned++;
        //Debug.Log("Platform Spawned: " + platformSpawned);

        return newPlatform;
    }


    void GeneratePlatforms()
    {    
        for (int i = 0; i < startingNumberOfPlatforms; i++)
        {
            if(i == 0)
            {
                // İlk platformu transform.position'da oluşturuyoruz.
                CreatePlatform(startingPlatformPrefab, transform.position);
            }
            else
            {
                float startPositionX = SpawnPositionCalculation();
                Vector2 spawnPosition = new Vector2(startPositionX, transform.position.y);
                CreatePlatform(platformPrefab, spawnPosition);
            }
        }
    }

    void StartingPlatformGenerate()
    {
        // İlk platform için doğrudan transform.position kullanabilirsiniz.
        Vector2 spawnPosition = transform.position;
        GameObject newPlatform = Instantiate(startingPlatformPrefab, spawnPosition, Quaternion.identity, platformParent);
        
        platforms.Add(newPlatform);
        Platforms platform = newPlatform.GetComponent<Platforms>();
        if (platform == null)
        {
            Debug.LogError("Starting platform prefab'da Platforms bileşeni eksik!");
            return;
        }
        platform.Init(this);
        platformSpawned++;
        Debug.Log("Starting Platform Spawned: " + platformSpawned);
        
    }


    private void SinglePlatformGenerate()
    {
        float startPositionX = SpawnPositionCalculation();

        Vector2 spawnPosition = new Vector2(startPositionX, transform.position.y);
        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity, platformParent);

        platforms.Add(newPlatform);

        Platforms platform = newPlatform.GetComponent<Platforms>(); //Burda da GetComponent<Platforms>() kullanmam gerekiyo.

        platform.Init(this);

        platformSpawned++;

        Debug.Log("Platform Spawned: " + platformSpawned);

    }
    
    private float SpawnPositionCalculation()
    {
        float startPositionX;

        if (platforms.Count == 0)
        {
            startPositionX = transform.position.x + platformSize;
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


            float platformDestroyOffset = platformSize * 2;

            if (platforms[i].transform.position.x < (-platformDestroyOffset))
            {
                platforms.Remove(currentPlatform);
                Destroy(currentPlatform);
                SinglePlatformGenerate();
            }
        }
    }

}
