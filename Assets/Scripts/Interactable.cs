using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private bool returnToStart;
    [SerializeField] private bool disablePlayer;
    [SerializeField] private bool canRotate;
    [HideInInspector] public bool active;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private MoveCam isoCam;


    protected void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        isoCam = GameObject.FindGameObjectWithTag("IsoCam").GetComponent<MoveCam>();
    }

    private void Update()
    {
        if (isoCam == null) { isoCam = GameObject.FindGameObjectWithTag("IsoCam").GetComponent<MoveCam>(); }

        if (active)
        {
            if (canRotate && Input.GetKey(KeyCode.R)) //hold R key to rotate
            {
                float XaxisRotation = Input.GetAxis("Mouse X") * 5;
                float YaxisRotation = Input.GetAxis("Mouse Y") * 5;
                //rotate the object depending on mouse X-Y Axis
                this.transform.Rotate(Vector3.down, XaxisRotation);
                this.transform.Rotate(Vector3.right, YaxisRotation);
            }
        }
    }
    public void PickUpObject()
    {
        if (disablePlayer)
        {
            isoCam.enabled = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        active = true;
    }

    public void DropObject()
    {
        if (disablePlayer)
        {
            isoCam.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (returnToStart)
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
        }
        active = false;
    }
}
