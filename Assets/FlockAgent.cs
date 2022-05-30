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
    // velocity - offset position (local)
    public void Move(Vector2 velocity)
    {
        // The sprite is pointing Upwards, this sets in angle to the position it will move in
        transform.up = velocity;

        // Delta time => time (in seconds) between frames
        transform.position += (Vector3) velocity * Time.deltaTime;
    }
}
