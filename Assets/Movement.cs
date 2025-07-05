using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orien;

    float rotX;
    float rotY;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockDtate = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("MX") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("MY") * Time.deltaTime * sensY;

        rotX += mouseX;
        rotY -+ mouseY;

        rotX = Mathf.Clamp(rotX, -90f, 90f);

        transform.rotation = Quaternion.Euler(rotX, rotY, 0);
        orien.rotation = Quaternion.Euler(0, rotY, 0);


    }
}
