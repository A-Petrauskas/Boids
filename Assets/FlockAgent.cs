using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }


    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    public void Move(Vector2 velocity)
    {
        transform.up += (Vector3) velocity * Time.deltaTime;
    }
}