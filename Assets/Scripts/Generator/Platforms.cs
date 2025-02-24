using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Platforms : MonoBehaviour
{
    [Header ("References")] 
    [SerializeField] GameObject obstaclePrefab; // Platform Prefab
    [SerializeField] GameObject coinPrefab; // Coin Prefab
    [SerializeField] GameObject powerUpPrefab; // PowerUp Prefab

    [Header ("Settings")]
    [SerializeField] float powerUpSpawnChance = 0.3f;
    [SerializeField] float coinSpawnChance = 0.5f;
    [SerializeField] float coinSeperationLength = 2f;
    [SerializeField] float[] lanes = { -2.5f, 1f, 0f, 1f, 2.5f }; // Obstacleların olabileceği dikey pozisyonlar
    [SerializeField] float[] XOffset = { -10f, -5f, 0f, 5f, 10f }; // Obstacleların olabileceği yatay pozisyonlar
    
    List<int> availableLanes = new List<int>{0, 1, 2}; // Kullanılabilir dikey pozisyonlar


    LevelGenerator levelGenerator;

    public void Init(LevelGenerator levelGenerator)
    {
        this.levelGenerator = levelGenerator;
    }


    void Start()
    {
        SpawnObstacles();
        SpawnCoins();
        SpawnPowerUp();
    }

    void SpawnObstacles()
    {
        int obstaclesToSpawn = Random.Range(0, lanes.Length); // 0 ile max lane sayısı arasında rastgele bir sayı seç

        for (int i = 0; i < obstaclesToSpawn; i++)
        {
            int selectedLane = SelectLanes();
            float randomXOffset = XOffset[Random.Range(0, XOffset.Length)]; // Yatay Offset belirle
            Vector2 spawnPosition = new Vector2(transform.position.x + randomXOffset, lanes[selectedLane]); // Dikey pozisyonu belirle


            Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity, this.transform); // Obstacle oluştur
        }
    }

    /* void SpawnCoins()
    {
        if(Random.value > coinSpawnChance || availableLanes.Count <= 0) return; // failsafe

        int selectedLane = SelectLanes();
        float randomXOffset = XOffset[Random.Range(0, XOffset.Length)];
        Vector2 spawnPosition = new Vector2(transform.position.x + randomXOffset, lanes[selectedLane]); // Dikey pozisyonu belirle
        Instantiate(coinPrefab, spawnPosition, Quaternion.identity, this.transform); // Coin oluştur
    } */

    void SpawnCoins()
{
    if(Random.value > coinSpawnChance || availableLanes.Count <= 0) return; // failsafe

    int selectedLane = SelectLanes(); // Coin'ların spawnlanacağı dikey pozisyonu seçiyoruz
    int maxCoinsToSpawn = 6;
    int coinsToSpawn = Random.Range(1, maxCoinsToSpawn);

    // Coin'ların başlangıç x pozisyonu, XOffset array'inden rastgele seçilen bir offset ile belirleniyor
    float randomXOffset = XOffset[Random.Range(0, XOffset.Length)];
    float startX = transform.position.x + randomXOffset;
    float laneY = lanes[selectedLane];

    for (int i = 0; i < coinsToSpawn; i++)
    {
        // Her coin, coinSeperationLength kadar artan x pozisyonunda spawnlanıyor
        float coinX = startX + (i * coinSeperationLength);
        Vector2 spawnPosition = new Vector2(coinX, laneY);
        Instantiate(coinPrefab, spawnPosition, Quaternion.identity, this.transform);
        
    }
}



 
    void SpawnPowerUp()
    {
        if(Random.value > powerUpSpawnChance || availableLanes.Count <= 0) return; // failsafe

        int selectedLane = SelectLanes();
        float randomXOffset = XOffset[Random.Range(0, XOffset.Length)];
        Vector2 spawnPosition = new Vector2(transform.position.x + randomXOffset, lanes[selectedLane]); // Dikey pozisyonu belirle
        PowerUp newPowerUp = Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity, this.transform).GetComponent<PowerUp>(); // PowerUp oluştur
        newPowerUp.Init(levelGenerator); //çözüm olma ihtimali var ama hem yukarda GetComponent kullanmam gerekiyor hem de Init fonksiyonunu çağırmam gerekiyor
    }

    int SelectLanes()
    {
        int randomLane = Random.Range(0, availableLanes.Count); // Rastgele bir dikey pozisyon seç
        int selectedLane = availableLanes[randomLane]; // Seçilen dikey pozisyonu al
        availableLanes.RemoveAt(randomLane); // Kullanılan dikey pozisyonu listeden çıkar
        return selectedLane;
    }


}
