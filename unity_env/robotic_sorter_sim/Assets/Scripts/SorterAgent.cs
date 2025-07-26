using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class SorterAgent : Agent
{
    private Rigidbody rBody;
    [SerializeField] private Transform target;

    private float previousDistance;
    private int stepCount;

    public override void Initialize()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        rBody.velocity = Vector3.zero;
        rBody.angularVelocity = Vector3.zero;

        // Reset agent and target positions VERY CLOSE at first to help early success
        transform.localPosition = new Vector3(Random.Range(-1f, 1f), 0.5f, Random.Range(-1f, 1f));
        target.localPosition = transform.localPosition + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));

        previousDistance = Vector3.Distance(transform.localPosition, target.localPosition);
        stepCount = 0;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3 relativePos = target.localPosition - transform.localPosition;

        sensor.AddObservation(relativePos); // 3
        sensor.AddObservation(rBody.velocity.x); // 1
        sensor.AddObservation(rBody.velocity.z); // 1
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        Vector3 controlSignal = new Vector3(moveX, 0, moveZ);
        rBody.AddForce(controlSignal * 10f);

        // Clamp speed
        float maxSpeed = 5f;
        if (rBody.velocity.magnitude > maxSpeed)
        {
            rBody.velocity = Vector3.ClampMagnitude(rBody.velocity, maxSpeed);
        }

        float distanceToTarget = Vector3.Distance(transform.localPosition, target.localPosition);

        // === Reward Shaping ===
        float progress = previousDistance - distanceToTarget;
        AddReward(progress * 0.05f); // Reward getting closer to target
        previousDistance = distanceToTarget;

        // Reached goal
        if (distanceToTarget < 1.2f)
        {
            SetReward(1.0f);
            EndEpisode();
        }

        // Fell off
        if (transform.localPosition.y < 0)
        {
            SetReward(-1.0f);
            EndEpisode();
        }

        // Step penalty
        AddReward(-0.001f);

        // Episode time limit
        stepCount++;
        if (stepCount > 500)
        {
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Horizontal");
        continuousActions[1] = Input.GetAxis("Vertical");
    }

    private void OnDrawGizmos()
    {
        if (target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}
