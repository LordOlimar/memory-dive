using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Phone : MonoBehaviour
{
    private TMP_Text phoneText;
    private GameObject playerCam;
    private Vector3 intialScale;
    private MoveCam isoCam;
    private float scale;
    private void Start()
    {
        phoneText = transform.Find("PhoneScreen/Phone Text").GetComponent<TMP_Text>();
        playerCam = GameObject.FindGameObjectWithTag("MainCamera");
        isoCam = GameObject.FindGameObjectWithTag("IsoCam").GetComponent<MoveCam>();
    }
    private void Update()
    {
        if (phoneText == null) { phoneText = transform.Find("PhoneScreen/Phone Text").GetComponent<TMP_Text>(); }
        if (playerCam == null) { playerCam = GameObject.FindGameObjectWithTag("MainCamera"); }
        if (isoCam == null) { isoCam = GameObject.FindGameObjectWithTag("IsoCam").GetComponent<MoveCam>(); }
        intialScale = new Vector3(0.1f, 0.01f, 0.2f);


        if (this.GetComponent<Interactable>().active)
        {
            transform.localPosition = new Vector3(0, 0, transform.localPosition.z);
            transform.localRotation = Quaternion.Euler(90, 0, 180);

            scale = isoCam.currentZoom;
            transform.localScale = new Vector3(0.5f * scale, intialScale.y, 1 * scale);
        }
        else
        {
            transform.localScale = intialScale;
        }

    }

    public void Dad()
    {
        phoneText.text = "Hello i am dad yayyy";
    }

    public void Sasha()
    {
        phoneText.text = "I am sasha and i do drugs";
    }

    public void Bill()
    {
        phoneText.text = "KILL KILL KILL STAB STAB STAB";
    }


}
