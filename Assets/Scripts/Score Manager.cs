using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int score = 0;

    public void IncreaseScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }
}
