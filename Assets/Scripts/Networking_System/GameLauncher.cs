using UnityEngine;
using Fusion;

public class GameLauncher : MonoBehaviour
{
    public NetworkRunner runnerPrefab;

    async void Start()
    {
        var runner = Instantiate(runnerPrefab);
        runner.ProvideInput = true;

        await runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.AutoHostOrClient,
            SessionName = "MurderMysteryRoom",
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }
}
