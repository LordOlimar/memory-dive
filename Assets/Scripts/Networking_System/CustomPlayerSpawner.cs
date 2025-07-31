using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CustomPlayerSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    public GameObject pcPlayerPrefab;
    public GameObject vrPlayerPrefab;

    private NetworkRunner runner;

    public async void StartGame(GameMode mode)
    {
        runner = gameObject.AddComponent<NetworkRunner>();
        runner.ProvideInput = true;

        await runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = "MurderMystery",
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });

        runner.AddCallbacks(this); // VERY important!
    }
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        // only spawn for your own player
        if (player != runner.LocalPlayer) 
            return;

        // PlayerRef.RawEncoded is 0 for the first joiner, 1 for the second, etc.
        int joinIndex = (int)player.RawEncoded;

        // First player → PC, second → VR, any further fallback to PC
        GameObject prefabToSpawn = joinIndex == 1
            ? vrPlayerPrefab
            : pcPlayerPrefab;

        Debug.Log($"Player {joinIndex} joined → spawning {prefabToSpawn.name}");

        runner.Spawn(prefabToSpawn, Vector3.zero, Quaternion.identity, player);
    }

    // ================= REQUIRED CALLBACKS =================

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }

    public void OnInput(NetworkRunner runner, NetworkInput input) { }

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