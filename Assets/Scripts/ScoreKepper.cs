using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKepper : MonoBehaviour
{
    public GameObject[] Agents;
    public Text TextScoreText;
    // Start is called before the first frame update
    void Start()
    {
        Agents = GameObject.FindGameObjectsWithTag("Agent");
        ScoreUpdate();
    }

    // Update is called once per frame
    public void ScoreUpdate()
    {
        TextScoreText.text = "";
       foreach (var agent in Agents) {
            TextScoreText.text += string.Format("{0}: {1} \n",agent.name,agent.GetComponent<AgentScript>().Score);
        } 
    }
}
