using Fusion;
using UnityEngine;

public class PCNetworkMovement : NetworkBehaviour
{
    [Tooltip("Speed in units per second")]
    public float speed = 5f;

    // Called every network tick
    public override void FixedUpdateNetwork()
    {
        // only run movement on the avatar you own
        if (!Object.HasInputAuthority) return;

        // fetch the input struct you set in OnInput(...)
        if (GetInput(out NetworkInputData data))
        {
            Vector3 dir = new Vector3(data.movement.x, 0, data.movement.y);
            transform.position += dir * speed * Runner.DeltaTime;
        }
    }
}
