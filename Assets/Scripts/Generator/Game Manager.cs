using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    // Oyun bittiğinde tetiklenecek statik event
    public static event Action GameOverEvent;

    private static bool gameOver = false;
    public static bool IsGameOver => gameOver;

    // Bu metod oyun bittiğinde çağrılacak
    public static void GameOver()
    {
        if (gameOver) return; // Oyun zaten bitmişse çık

        gameOver = true;
        Debug.Log("GameManager: Oyun Bitti!");
        GameOverEvent?.Invoke(); // Olayı duyur
    }
}
