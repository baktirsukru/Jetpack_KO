using UnityEngine;

public class PowerUp : Pickup
{
    //[SerializeField] private float powerUpDuration = 5f;
    [SerializeField] private float powerUpSpeedAmount = 10f;
    LevelGenerator levelGenerator;

    public void Init(LevelGenerator levelGenerator)
    {
        this.levelGenerator = levelGenerator;
    }

    protected override void OnPickup()
    {
        Debug.Log("PowerUp Collected");
        levelGenerator.SpeedUpPlatforms(powerUpSpeedAmount);//, powerUpDuration);
        //
    }
}

