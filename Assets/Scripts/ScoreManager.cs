using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int blueTeamScore = 0;
    public int purpleTeamScore = 0;

    public int GetSelfGoalScore(Team team)
    {
        if (team == Team.Blue)
        {
            return blueTeamScore;
        }
        else
        {
            return purpleTeamScore;
        }
    }
    public int GetFullScore(Team team)
    {
        if (team == Team.Blue)
        {
            return blueTeamScore;
        }
        else
        {
            return purpleTeamScore;
        }
    }

    public int GetScoreWithSubtractedSelfGoal(Team team)
    {
        if (team == Team.Blue)
        {
            return blueTeamScore - GetSelfGoalScore(Team.Blue);
        }
        else
        {
            return purpleTeamScore - GetSelfGoalScore(Team.Purple);
        }
    }
}
