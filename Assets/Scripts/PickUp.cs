using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private float pickUpRange = 5f;
    private Transform holdPos;
    private int LayerNumber; //layer index
    private GameObject heldObj; //held object
    private Rigidbody heldObjRB; //held object rigidbody
    private bool canDrop = true;

    private void Start()
    {
        holdPos = this.transform.Find("holdPos").transform;
        LayerNumber = LayerMask.NameToLayer("PickUp");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObj == null) //if currently not holding anything
            {
                //check if player is looking at a pickupable object within pickuprange
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    if (hit.transform.gameObject.tag == "canPickUp")
                    {
                        PickUpObject(hit.transform.gameObject); //pass hit object to PickUpObject function

                    }
                       
                }
            }
            else
            {
                if (canDrop == true)
                {
                    StopClipping(); //prevents object from clipping through walls
                    DropObject();
                }
            }
        }
        if (heldObj != null) //if player is holding object
        {
            heldObj.transform.position = holdPos.transform.position; //keep object position at holdPos
        }
    }

    private void PickUpObject(GameObject pickUpObject)
    {
        if (pickUpObject.GetComponent<Rigidbody>()) //check if object has a rigidbody
        {
            heldObj = pickUpObject;
            heldObjRB = pickUpObject.GetComponent<Rigidbody>(); //assign rigidbody
            heldObjRB.isKinematic = true;
            heldObjRB.transform.parent = holdPos.transform; //parent object to holdpos object
            heldObj.layer = LayerNumber; //change the object layer to the holdLayer
            heldObj.GetComponent<Interactable>().PickUpObject();
        }
    }

    private void DropObject()
    {
        heldObj.layer = 0; //object assigned back to default layer
        heldObjRB.isKinematic = false;
        heldObj.transform.parent = null; //unparent object
        heldObj.GetComponent<Interactable>().DropObject();
        heldObj = null;
    }

    private void StopClipping()
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position); //distance from holdPos to the camera
        //have to use RaycastAll as object blocks raycast in center screen
        //RaycastAll returns array of all colliders hit within the cliprange
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        //if the array length is greater than 1, meaning it has hit more than just the object we are carrying
        if (hits.Length > 1)
        {
            //change object position to camera position 
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);
        }
    }
}
