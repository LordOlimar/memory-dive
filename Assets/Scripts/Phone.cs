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
        playerCam = GameObject.FindGameObjectWithTag("PlayerCam");
        isoCam = GameObject.FindGameObjectWithTag("IsoCam").GetComponent<MoveCam>();
        intialScale = transform.localScale;
    }
    private void Update()
    {
        if(this.GetComponent<Interactable>().active)
        {
            transform.LookAt(playerCam.transform.position, Vector3.up);
            transform.rotation = transform.rotation * Quaternion.Euler(90, 0, 0);

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
