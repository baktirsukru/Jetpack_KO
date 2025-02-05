using UnityEngine;

public class Platforms : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab; // Platform Prefab
    [SerializeField] float[] lanes = { -2.5f, 0f, 2.5f }; // Platformların olabileceği dikey pozisyonlar

    void Start()
    {
        SpawnObstacles();
    }

    void SpawnObstacles()
    {
        int randomLane = Random.Range(0, lanes.Length); // Rastgele bir dikey pozisyon seç
        Vector2 spawnPosition = new Vector2(transform.position.x, lanes[randomLane]); // Dikey pozisyonu belirle
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity, this.transform); // Platformu oluştur

    }
}
