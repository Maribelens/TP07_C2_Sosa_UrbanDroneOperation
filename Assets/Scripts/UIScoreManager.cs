using TMPro;
using UnityEngine;

public class UIScoreManager : MonoBehaviour
{

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int score = 0;

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
            //scoreText.text = "Score: " + score;
    }
}
