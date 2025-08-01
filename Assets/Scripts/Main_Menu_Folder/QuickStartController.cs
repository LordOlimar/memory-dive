using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickStartController : MonoBehaviour
{
    public GameObject instructionsPanel;

    // Called by StartCoOp button
    public void StartCoOp()
    {
        // Load your main game scene, which handles network initialization
        SceneManager.LoadScene("Cutscene");
    }

    public void ShowInstructions()
    {
        instructionsPanel.SetActive(true);
    }

    public void CloseInstructions()
    {
        instructionsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}