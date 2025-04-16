using UnityEngine;

public class PowerUp : Pickup
{
    [SerializeField] private float powerUpDuration = 1f;
    [SerializeField] private float powerUpSpeedAmount = 10f;
    // Powerup’ın çarpan olarak ne kadar ekleyeceğini belirleyen değer (örneğin, her alımda +2)
    [SerializeField] private int additionalMultiplierValue = 2;

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

        // Her powerup alımında mevcut çarpanı artırıyoruz.
        ScoreManager.Instance.IncreaseMultiplier(additionalMultiplierValue);
        
        // PowerUp efektini uyguladıktan sonra kendini sahneden kaldırabilirsiniz.
        Destroy(gameObject);
    }
}

