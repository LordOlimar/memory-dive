using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 6;
    [SerializeField] private float zoomSmoothness = 5;
    [SerializeField] private float minZoom = 2;
    [SerializeField] private float maxZoom = 10;
    [SerializeField] private float rotationSpeed = 5;
    public float currentZoom;
    private Camera cam1;
    private Camera cam2;

    private void Start()
    {
        cam1 = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        cam2 = GameObject.FindGameObjectWithTag("PickUpCam").GetComponent<Camera>();
    }


    // Update is called once per frame
    private void Update()
    {
        currentZoom = Mathf.Clamp(currentZoom - Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime, minZoom, maxZoom);
        cam1.orthographicSize = Mathf.Lerp(cam1.orthographicSize, currentZoom, zoomSmoothness * Time.deltaTime);
        cam2.orthographicSize = Mathf.Lerp(cam2.orthographicSize, currentZoom, zoomSmoothness * Time.deltaTime);

        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            float mouseDeltaX = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up, mouseDeltaX * rotationSpeed * Time.deltaTime);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
