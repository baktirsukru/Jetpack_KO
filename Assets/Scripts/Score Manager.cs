using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    int score = 0;

    // Ayarlanabilir parametreler: artış aralığı ve bu aralıkta eklenen skor miktarı
    [SerializeField] public float interval = 1f;       // Skor artış aralığı (saniye cinsinden)
    [SerializeField] public int scorePerInterval = 10; // Her artışta eklenen skor miktarı






    public void Start()
    {
        StartCoroutine(AutoIncreaseScore());
    } 
    IEnumerator AutoIncreaseScore()
    {
        while (true) // Sonsuz döngü, oyun objesi aktif olduğu sürece çalışır
        {
            IncreaseScore(scorePerInterval); // Skoru artır
            yield return new WaitForSeconds(interval); // Belirtilen saniye kadar bekle
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }



    
}

