using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    private Renderer objRenderer;
    private Material originalMaterial;

    [Header("Highlight")]
    public Material outlineMaterial; // assign in Inspector

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
        if (objRenderer != null)
        {
            // Store the original material so we can revert later
            originalMaterial = objRenderer.material;
        }
    }

    public void OnFocus()
    {
        if (objRenderer != null && outlineMaterial != null)
        {
            objRenderer.material = outlineMaterial;
        }
    }

    public void OnLoseFocus()
    {
        if (objRenderer != null && originalMaterial != null)
        {
            objRenderer.material = originalMaterial;
        }
    }

    public void Interact()
    {
        Debug.Log("Interacted with: " + gameObject.name);
        // Add interaction logic here if needed
    }
}