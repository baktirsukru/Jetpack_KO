using UnityEngine;

public class PowerUp : Pickup
{
    [SerializeField] private float powerUpDuration = 1f;
    [SerializeField] private float powerUpSpeedAmount = 10f;
    LevelGenerator levelGenerator;
    BackgroundScroller backgroundScroller;

    public void Init(LevelGenerator levelGenerator)
    {
        this.levelGenerator = levelGenerator;
    }

    void Awake()
    {
        backgroundScroller = FindFirstObjectByType<BackgroundScroller>();
    }

    protected override void OnPickup()
    {
        Debug.Log("PowerUp Collected");
        levelGenerator.SpeedUpPlatforms(powerUpSpeedAmount, powerUpDuration);
        backgroundScroller.SpeedUpBackground(powerUpSpeedAmount, powerUpDuration);
    }
}

