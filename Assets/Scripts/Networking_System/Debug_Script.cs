using Fusion;
using UnityEngine;

public class CameraDebug : NetworkBehaviour
{
    public Camera playerCam;

    public override void Spawned()
    {
        Debug.Log("Spawned: " + Object.InputAuthority);

        if (!Object.HasInputAuthority)
        {
            Debug.Log("Disabling remote camera");
            playerCam.enabled = false;
            playerCam.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Enabling local camera");
            playerCam.enabled = true;
            playerCam.gameObject.SetActive(true);
        }
    }
}
