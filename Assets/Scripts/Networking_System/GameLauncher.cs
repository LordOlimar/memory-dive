using UnityEngine;
using Fusion;

public class GameLauncher : MonoBehaviour
{
    public NetworkRunner runnerPrefab;

    async void Start()
    {

        FindObjectOfType<CustomPlayerSpawner>().StartGame(GameMode.AutoHostOrClient);
        if (runnerPrefab == null)
        {
            Debug.LogError("Runner prefab is not assigned!");
            return;
        }
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
