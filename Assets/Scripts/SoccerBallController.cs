using UnityEngine;

public class SoccerBallController : MonoBehaviour
{
    public GameObject area;
    [HideInInspector]
    public SoccerEnvController envController;
    public string purpleGoal; //will be used to check if collided with purple goal
    public string blueGoal; //will be used to check if collided with blue goal

    void Start()
    {
        envController = area.GetComponent<SoccerEnvController>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (!string.IsNullOrEmpty(purpleGoal) && col.gameObject.CompareTag(purpleGoal)) //ball touched purple goal
        {
            envController.GoalTouched(Team.Blue);
            FindAnyObjectByType<ScoreManager>().blueTeamScore++;
        }
        if (!string.IsNullOrEmpty(blueGoal) && col.gameObject.CompareTag(blueGoal)) //ball touched blue goal
        {
            envController.GoalTouched(Team.Purple);
            FindAnyObjectByType<ScoreManager>().purpleTeamScore++;
            
        }
    }
}