using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : FilteredFlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // No neighbours => maintain current alignment
        if (context.Count == 0)
        {
            return agent.transform.up;
        }

        Vector2 alignmentMove = Vector2.zero;

        // If filter selected => use it
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        foreach (var item in filteredContext)
        {
            alignmentMove += (Vector2) item.transform.up;
        }

        alignmentMove /= context.Count;

        return alignmentMove;
    }
}
