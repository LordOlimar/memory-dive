using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 3f;
    public Camera playerCam;

    private IInteractable currentTarget;

    void Update()
    {
        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (interactable != currentTarget)
                {
                    if (currentTarget != null)
                        currentTarget.OnLoseFocus();

                    currentTarget = interactable;
                    currentTarget.OnFocus();
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentTarget.Interact();
                }
            }
            else
            {
                ClearTarget();
            }
        }
        else
        {
            ClearTarget();
        }
    }

    void ClearTarget()
    {
        if (currentTarget != null)
        {
            currentTarget.OnLoseFocus();
            currentTarget = null;
        }
    }
}
