using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class BootLoader : MonoBehaviour
{
    void Start()
    {
        // true on a real headset build (not in Editor)
        bool isVR = XRSettings.isDeviceActive && !Application.isEditor;

        if (isVR)
        {
            // VR skips straight to the cutscene
            SceneManager.LoadScene("Cutscene");
        }
        else
        {
            // PC sees the main menu
            SceneManager.LoadScene("MainMenu");
        }
    }
}
