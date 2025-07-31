using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientV2 : MonoBehaviour
{
    public GameObject referenceObject;
    public float wantedAngle;
    public float opacity;
    private Material material;
    private Color currentColor;

    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            material = meshRenderer.material;
            currentColor = material.color;
            currentColor.a = opacity;
            material.color = currentColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 eulerAngles = referenceObject.transform.eulerAngles;

        transform.localEulerAngles = eulerAngles;
        float xRotation = eulerAngles.z - wantedAngle;
        xRotation %= 360;
        if (xRotation > 180)
        {
            xRotation = -(360-xRotation);
        }
        float t = Mathf.Abs(xRotation) / 180;
        currentColor.a = t;
        material.color = currentColor;

    }
}
