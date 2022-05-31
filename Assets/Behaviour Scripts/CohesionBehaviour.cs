using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class CohesionBehaviour : FilteredFlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // No neighbours => no adjustments
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        Vector2 cohesionMove = Vector2.zero;

        // If filter selected => use it
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        foreach (var item in filteredContext)
        {
                // Global position
                cohesionMove += (Vector2) item.position;
        }

        cohesionMove /= context.Count;

        // Offset from agent position
        cohesionMove -= (Vector2) agent.transform.position;

        return cohesionMove;
    }
}
