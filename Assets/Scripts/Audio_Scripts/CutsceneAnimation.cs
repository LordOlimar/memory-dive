using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneAnimation : MonoBehaviour
{
    public GameObject cutsceneObject; // Reference to the object you want to show/hide
  //  public float inactiveDuration = 13.0f; // Duration the object will be inactive
    public float activeDuration = 50.0f; // Duration the object will be active

    void Start()
    {
       // cutsceneObject.SetActive(true); // Start inactive
        StartCoroutine(ShowAndHideObject());
    }

    IEnumerator ShowAndHideObject()
    {
        // object inactive
      //  yield return new WaitForSeconds(inactiveDuration);

        // activate the object after duration
        cutsceneObject.SetActive(true);

        yield return new WaitForSeconds(activeDuration); // show on screen for time

        cutsceneObject.SetActive(false); // deactivate
    }
}