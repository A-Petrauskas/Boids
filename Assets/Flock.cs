using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    public FlockBehaviour behaviour;
    List<FlockAgent> agents = new List<FlockAgent>();


    [Range(10, 500)]
    public int agentCount = 250;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;

    [Range(1f, 100f)]
    public float maxSpeed = 5f;

    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;

    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;


    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;

    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }


    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < agentCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * agentCount * AgentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                );

            newAgent.name = "Agent " + i;

            newAgent.Initialize(this);

            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            // Interpolates between white and red in relation to the number of neighbours
            // agent.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);

            Vector2 move = behaviour.CalculateMove(agent, context, this);

            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                // Multiply 1 (normalized) by the max speed
                move = move.normalized * maxSpeed;
            }

            agent.Move(move);
        }
    }


    List<Transform> GetNearbyObjects (FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();

        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);

        foreach (var collider in contextColliders)
        {
            if (collider != agent.AgentCollider)
            {
                context.Add(collider.transform);
            }
        }

        return context;
    }
}
