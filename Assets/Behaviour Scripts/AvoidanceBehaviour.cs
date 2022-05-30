using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class AvoidanceBehaviour : FlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        Vector2 avoidanceMove = Vector2.zero;
        int avoidCount = 0;
        foreach (var item in context)
        {
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                avoidCount++;
                avoidanceMove += (Vector2) (agent.transform.position - item.position);
            }
        }

        if (avoidCount > 0)
        {
            avoidanceMove /= avoidCount;
        }

        return avoidanceMove;
    }
}
