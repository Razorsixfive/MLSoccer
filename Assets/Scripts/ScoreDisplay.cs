using UnityEngine;
using UnityEngine.UI; // Import this namespace if using Text for UI

public class ScoreDisplay : MonoBehaviour
{
    public Text blueTeamScoreText;
    public Text purpleTeamScoreText;

    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void Update()
    {
        blueTeamScoreText.text = "Blue Team: " + scoreManager.blueTeamScore;
        purpleTeamScoreText.text = "Purple Team: " + scoreManager.purpleTeamScore;
    }
}
