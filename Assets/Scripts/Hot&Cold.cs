using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.RendererUtils;

public class HotCold : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject referenceObject;
    public Renderer newRenderer;
    public float colorChangeDistance = 5f;
    public float wantedAngle;
    public Gradient gradient;


    // Start is called before the first frame update
    void Start()
    {
        /*
        gradient = new Gradient();

        var colors = new GradientColorKey[2];
        colors[0] = new GradientColorKey(Color.white, 0.0f);
        colors[1] = new GradientColorKey(Color.green, 1.0f);

        var alphas = new GradientAlphaKey[2];
        alphas[0] = new GradientAlphaKey(1.0f, 0.0f);
        alphas[1] = new GradientAlphaKey(0.0f, 1.0f);

        gradient.SetKeys(colors, alphas);
        */

        newRenderer = targetObject.GetComponent<Renderer>();


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 eulerAngles = transform.eulerAngles;
        float xRotation = eulerAngles.x - wantedAngle;

        float t = Mathf.Clamp01(xRotation / colorChangeDistance);
        Color newColor = gradient.Evaluate(t);

        newRenderer.material.SetColor("_Color", newColor);

    }
}
