using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CustomPlayerSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    [Header("Assign in Inspector")]
    public GameObject pcPlayerPrefab;
    public GameObject vrPlayerPrefab;

    int _serverSpawnCount = 0;

    public async void StartGame(GameMode mode)
    {
        var runner = gameObject.AddComponent<NetworkRunner>();
        runner.ProvideInput = true;
        await runner.StartGame(new StartGameArgs {
            GameMode     = mode,
            SessionName  = "MurderMystery",
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
        runner.AddCallbacks(this);
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        // only the server decides what to spawn
        if (!runner.IsServer) return;

        // decide which prefab to use for *this* join:
        //  • if this is the *first ever* join (count == 0), pick based on device
        //  • if it’s the *second* join (count == 1), pick the *other* prefab
        bool localIsVR = XRSettings.isDeviceActive;   
        GameObject toSpawn;

        if (_serverSpawnCount == 0)
        {
            // first player
            toSpawn = localIsVR ? vrPlayerPrefab : pcPlayerPrefab;
        }
        else if (_serverSpawnCount == 1)
        {
            // second player
            toSpawn = localIsVR ? pcPlayerPrefab : vrPlayerPrefab;
        }
        else
        {
            // fallback for any extras
            toSpawn = pcPlayerPrefab;
        }

        Debug.Log($"[Spawner] Join #{_serverSpawnCount + 1} → spawning {toSpawn.name}");
        runner.Spawn(toSpawn, Vector3.zero, Quaternion.identity, player);
        _serverSpawnCount++;
    }
    // ================= REQUIRED CALLBACKS =================

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }

    public void OnInput(NetworkRunner runner, NetworkInput input)
{
    // Gather PC or VR joystick/keys into our NetworkInputData
    var data = new NetworkInputData();

    // PC: WASD / Arrow keys
    float h = Input.GetAxis("Horizontal");
    float v = Input.GetAxis("Vertical");
    data.movement = new Vector2(h, v);

    // VR joystick (if you want VR locomotion too)
    if (UnityEngine.XR.XRSettings.isDeviceActive)
    {
        // e.g. left thumbstick
        var leftHand = UnityEngine.XR.InputDevices.GetDeviceAtXRNode(UnityEngine.XR.XRNode.LeftHand);
        if (leftHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 stick))
        {
            data.movement = stick;
        }
    }

    input.Set(data);
}

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }

    public void OnConnectedToServer(NetworkRunner runner) { }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) => request.Accept();

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }

    public void OnSceneLoadDone(NetworkRunner runner) { }

    public void OnSceneLoadStart(NetworkRunner runner) { }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
}