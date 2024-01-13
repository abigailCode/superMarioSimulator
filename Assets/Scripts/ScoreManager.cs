using UnityEngine;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI text; 
    private int score;
    public int objectiveScore;

    private void Start()
    {
        score = 0;
        UpdateScoreText();
    }

    public void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;
        AudioManager.instance.PlaySFX("CollectCoin");

        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (text != null) 
        {
            text.text = score + "/" +objectiveScore;
        }

        if (score >= objectiveScore)
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (currentSceneName == "Game")
        {
            SceneController.instance.LoadScene("Victory");
        }
    }
}
