using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    int score = 0;

    // Ayarlanabilir parametreler: artış aralığı ve bu aralıkta eklenen skor miktarı
    [SerializeField] public float interval = 1f;       // Skor artış aralığı (saniye cinsinden)
    [SerializeField] public int scorePerInterval = 10; // Her artışta eklenen skor miktarı

    // Skor çarpanı için ayarlar
    [SerializeField] public int scoreMultiplier = 1;     // Varsayılan çarpan (aktif değilse 1)
    private float multiplierEndTime = 0f;   // Çarpanın biteceği zamanı tutar
    [SerializeField] public float multiplierDuration = 10f;  // Power-up alındığında eklenen süre

    /* public bool gameActive = true; // Oyun aktif mi? */
    public void Start()
    {
        StartCoroutine(AutoIncreaseScore());
    } 
    IEnumerator AutoIncreaseScore()
    {
        while (true) // Sonsuz döngü, oyun objesi aktif olduğu sürece çalışır
        {
            IncreaseScore(scorePerInterval * scoreMultiplier); // Skoru artır
            yield return new WaitForSeconds(interval); // Belirtilen saniye kadar bekle
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }

    // Power-up alındığında çağırılacak metot
    public void ActivateMultiplier()
    {
        if (Time.time < multiplierEndTime)
        {
            // Eğer daha önce power-up alınmış ve çarpan süresi dolmamışsa,
            // çarpan değerini artır ve süresini uzat
            scoreMultiplier++;
            multiplierEndTime = Time.time + multiplierDuration;
        }
        else
        {
            // Eğer çarpan süresi dolmuşsa veya ilk kez power-up alınıyorsa,
            // çarpan değerini aktif hale getir (örneğin 2 olarak başlatabilirsiniz)
            scoreMultiplier = 2;
            multiplierEndTime = Time.time + multiplierDuration;
            // Çarpanın süresini takip edecek coroutine’i başlat
            StartCoroutine(MultiplierCountdown());
        }

        Debug.Log("Multiplier Activated: " + scoreMultiplier);
    }

    // Çarpan süresini kontrol eden coroutine
    IEnumerator MultiplierCountdown()
    {
        // Süre dolana kadar bekler
        while (Time.time < multiplierEndTime)
        {
            yield return null;
        }

        // Süre dolduğunda çarpanı sıfırlıyoruz (aktif etki kaldırılıyor)
        scoreMultiplier = 1;
        Debug.Log("Multiplier expired. Multiplier reset to 1.");
    }

   /*  // Oyun bittiğinde otomatik skor artışını durdurmak için çağırılacak metot
    public void EndGame()
    {
        gameActive = false;
        Debug.Log("Game over. Automatic score increase stopped.");
    } */
}

