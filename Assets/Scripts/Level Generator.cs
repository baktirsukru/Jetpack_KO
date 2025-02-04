using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject platformPrefab;
    [SerializeField] int startingNumberOfPlatforms = 3;
    [SerializeField] Transform platformParent;
    [SerializeField] float platformSize = 2f;

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for (int i = 0; i < startingNumberOfPlatforms; i++)
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

            Vector2 spawnPosition = new Vector2(startPositionX, transform.position.y);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity, platformParent);
            
        }
        
    }
}
