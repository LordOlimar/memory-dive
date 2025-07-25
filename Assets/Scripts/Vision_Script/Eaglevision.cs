using System.Collections.Generic;
using UnityEngine;

public class EagleVision : MonoBehaviour
{
    public KeyCode activationKey = KeyCode.E;  // Press 'E' to activate
    public float highlightDuration = 3f;
    public float detectionRadius = 10f;
    public LayerMask interactableLayer;
    public Material highlightMaterial;

    private Dictionary<GameObject, Material> originalMaterials = new Dictionary<GameObject, Material>();
    private bool isHighlighting = false;

    void Update()
    {
        if (Input.GetKeyDown(activationKey) && !isHighlighting)
        {
            StartCoroutine(HighlightInteractables());
        }
    }

    private System.Collections.IEnumerator HighlightInteractables()
    {
        isHighlighting = true;

        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, interactableLayer);

        foreach (Collider hit in hits)
        {
            Renderer renderer = hit.GetComponent<Renderer>();
            if (renderer != null)
            {
                originalMaterials[hit.gameObject] = renderer.material;
                renderer.material = highlightMaterial;
            }
        }

        yield return new WaitForSeconds(highlightDuration);

        foreach (var entry in originalMaterials)
        {
            if (entry.Key != null)
            {
                Renderer renderer = entry.Key.GetComponent<Renderer>();
                if (renderer != null)
                    renderer.material = entry.Value;
            }
        }

        originalMaterials.Clear();
        isHighlighting = false;
    }
}
