using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Coin : Pickup
{
    [SerializeField] int scoreAmount = 100;
    private ScoreManager scoreManager;
    

    void Awake()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }


    protected override void OnPickup()
    {
        scoreManager.IncreaseScore(scoreAmount);
    }
}
