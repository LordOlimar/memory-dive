using Fusion;
using UnityEngine;
using UnityEngine.XR;

public class VRNetworkMovement : NetworkBehaviour
{
    [Tooltip("Speed in units per second")]
    public float speed = 2f;

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasInputAuthority) return;

        // default: read left thumbstick on HMD
        Vector2 stick;
        var leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        if (leftHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out stick))
        {
            // move relative to HMD forward/right
            Vector3 fw = Camera.main.transform.forward; fw.y = 0;
            Vector3 rt = Camera.main.transform.right;  rt.y = 0;
            Vector3 move = (fw * stick.y + rt * stick.x) * speed;
            transform.position += move * Runner.DeltaTime;
        }
    }
}
