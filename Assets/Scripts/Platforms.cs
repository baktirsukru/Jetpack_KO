using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    [Header ("References")] 
    [SerializeField] GameObject obstaclePrefab; // Platform Prefab
    [SerializeField] GameObject coinPrefab; // Coin Prefab
    [SerializeField] GameObject powerUpPrefab; // PowerUp Prefab

    [Header ("Settings")]
    [SerializeField] float powerUpSpawnChance = 0.3f;
    [SerializeField] float coinSpawnChance = 0.5f;
    [SerializeField] float[] lanes = { -2.5f, 1f, 0f, 1f, 2.5f }; // Obstacleların olabileceği dikey pozisyonlar
    //[SerializeField] float[] columns = { -7.5f, 0, 7.5f }; 


    List<int> availableLanes = new List<int>{0, 1, 2}; // Kullanılabilir dikey pozisyonlar
    //List<int> availableColumns = new List<int>{0, 1, 2}; // Kullanılabilir yatay pozisyonlar


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
            Vector2 spawnPosition = new Vector2(transform.position.x, lanes[selectedLane]); // Dikey pozisyonu belirle
            Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity, this.transform); // Obstacle oluştur
        }
    }

    void SpawnCoins()
    {
        if(Random.value > coinSpawnChance || availableLanes.Count <= 0) return; // failsafe

        int selectedLane = SelectLanes();
        //int selectedColumn = SelectColumns();
        Vector2 spawnPosition = new Vector2(transform.position.x, lanes[selectedLane]); // Dikey pozisyonu belirle
        Instantiate(coinPrefab, spawnPosition, Quaternion.identity, this.transform); // Coin oluştur
    }

    void SpawnPowerUp()
    {
        if(Random.value > powerUpSpawnChance || availableLanes.Count <= 0) return; // failsafe

        int selectedLane = SelectLanes();
        Vector2 spawnPosition = new Vector2(transform.position.x, lanes[selectedLane]); // Dikey pozisyonu belirle
        Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity, this.transform); // PowerUp oluştur
    }

    int SelectLanes()
    {
        int randomLane = Random.Range(0, availableLanes.Count); // Rastgele bir dikey pozisyon seç
        int selectedLane = availableLanes[randomLane]; // Seçilen dikey pozisyonu al
        availableLanes.RemoveAt(randomLane); // Kullanılan dikey pozisyonu listeden çıkar
        return selectedLane;
    }

/*     int SelectColumns()
    {
        int randomColumn = Random.Range(0, availableColumns.Count); // Rastgele bir yatay pozisyon seç
        int selectedColumn = availableColumns[randomColumn]; // Seçilen yatay pozisyonu al
        availableColumns.RemoveAt(randomColumn); // Kullanılan yatay pozisyonu listeden çıkar
        return selectedColumn;
    } */

}
