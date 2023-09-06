using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class AgentScript : Agent
{
    public int Score = 0;

    Rigidbody AgentRB;
    public float maxSpawnRange = 45f;
    float spawnRange = 45f;//1f;
    // Speed of agent rotation.
    public float turnSpeed = 300;
    // Speed of agent movement.
    public float moveSpeed = 2;

    public override void Initialize()
    {
        AgentRB = GetComponent<Rigidbody>();
    }

    public override void CollectObservations(VectorSensor sensor)
    {

        var localVelocity = transform.InverseTransformDirection(AgentRB.velocity);
        sensor.AddObservation(localVelocity.x);
        sensor.AddObservation(localVelocity.z);
        //sensor.AddObservation(AgentRB.transform.localPosition.x);
        //sensor.AddObservation(AgentRB.transform.localPosition.z);
    }

    public void MoveAgent(ActionBuffers actionBuffers)
    {

        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;

        var continuousActions = actionBuffers.ContinuousActions;
        //var discreteActions = actionBuffers.DiscreteActions;

        var forward = Mathf.Clamp(continuousActions[0], -1f, 1f);
        var rotate = Mathf.Clamp(continuousActions[1], -1f, 1f);

        dirToGo = transform.forward * forward;
        rotateDir = transform.up * rotate;

        AgentRB.AddForce(dirToGo * moveSpeed, ForceMode.VelocityChange);
        transform.Rotate(rotateDir, Time.fixedDeltaTime * turnSpeed);

        if (AgentRB.velocity.sqrMagnitude > 25f) // slow it down
        {
            AgentRB.velocity *= 0.95f;
        }
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        MoveAgent(actionBuffers);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;

        continuousActionsOut[1] = Input.GetAxisRaw("Horizontal");
        continuousActionsOut[0] = Input.GetAxisRaw("Vertical");
    }

    public override void OnEpisodeBegin()
    {
        AgentRB.velocity = Vector3.zero;
        transform.position = new Vector3(Random.Range(-spawnRange, spawnRange), 2f, Random.Range(-spawnRange, spawnRange));
        transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));

        //make it easier in the beginning same settings on goal object
        spawnRange += 0.5f;
        spawnRange = Mathf.Clamp(spawnRange, 2f, maxSpawnRange);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            Score++;
            AddReward(1f);
            EndEpisode();
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            AddReward(-1f);
        }
    }

}
