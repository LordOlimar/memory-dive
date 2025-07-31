using Fusion;
using UnityEngine;

public class LocalCameraOnly : NetworkBehaviour
{
    public Camera cam;

    public override void Spawned()
    {
        bool mine = Object.HasInputAuthority;
        cam.enabled = mine;
        cam.gameObject.SetActive(mine);
        Debug.Log($"Camera for {Object.InputAuthority} enabled={mine}");
    }
}