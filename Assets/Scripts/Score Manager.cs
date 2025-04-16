using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    int score = 0;

    // Ayarlanabilir parametreler: artış aralığı ve bu aralıkta eklenen skor miktarı
    [SerializeField] public float interval = 1f;       // Skor artış aralığı (saniye cinsinden)
    [SerializeField] public int scorePerInterval = 10; // Her artışta eklenen skor miktarı
    [SerializeField] public int scoreMultiplier = 1;   // Skor çarpanı
    [SerializeField] public int totalScore = 0;      // Toplam skor

    private Coroutine scoreCoroutine; // Coroutine referansı
    private bool gameIsOver = false;  // Oyun bittiğinde true olacak bayrak


    private void Awake()
    {
        // Singleton kontrolü
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            // İsterseniz oyunda sahne değişse bile ScoreManager kalması için:
            // DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        // GameManager'dan oyunun bitme olayını dinle
        GameManager.GameOverEvent += OnGameOver;
    }

    private void OnDisable()
    {
        GameManager.GameOverEvent -= OnGameOver;
    }

    public void Start()
    {
        scoreCoroutine = StartCoroutine(AutoIncreaseScore());
    } 

    IEnumerator AutoIncreaseScore()
    {
        while (!gameIsOver) // Sonsuz döngü, oyun objesi aktif olduğu sürece çalışır
        {
            IncreaseScore(scorePerInterval); // Skoru artır
            yield return new WaitForSeconds(interval); // Belirtilen saniye kadar bekle
        }
    }

    // Skoru, verilen miktarın skor çarpanı ile çarpılmış hali kadar artırır.
    public void IncreaseScore(int amount)
    {
        int addedScore = amount * scoreMultiplier;
        score += addedScore;
        totalScore = score; // toplam skoru güncelle, eğer ayrı bir mantık varsa bu satır değiştirilebilir.
        Debug.Log("Score: " + score + " (+" + addedScore + " puan, çarpan: " + scoreMultiplier + ")");
    }

    // PowerUp alındığında mevcut çarpanın üzerine ekler.
    public void IncreaseMultiplier(int additionalValue)
    {
        scoreMultiplier += additionalValue;
        Debug.Log("Skor çarpanı arttı: " + scoreMultiplier);
    }

    // Oyuncunun örneğin engele çarpması gibi durumlarda çarpanı sıfırlamak için kullanılabilir.
    public void ResetMultiplier()
    {
        scoreMultiplier = 1;
        Debug.Log("Skor çarpanı sıfırlandı.");
    }

    // Oyun bittiğinde tetiklenecek metod.
    private void OnGameOver()
    {
        gameIsOver = true;
        StopScoreIncrease();
        Debug.Log("Oyun bitti gameoveramk");
    }

    void StopScoreIncrease()
    {
        if(scoreCoroutine != null)
        {
            StopCoroutine(scoreCoroutine);
            Debug.Log("ScoreManager: Skor artışı durduruldu.");
        }
    }
    
}

