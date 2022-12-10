using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform InteractionPoint;
    public LayerMask InteractionLayer;
    public float InteractionPointRadius = 1f;
    public static bool IsInteracting { get; private set; }

    public void Update()
    {
        var colliders = Physics2D.OverlapCircleAll(InteractionPoint.position, InteractionPointRadius, InteractionLayer);
        Collider2D collider = Physics2D.OverlapCircle(InteractionPoint.position, InteractionPointRadius, InteractionLayer);

        if (collider == null)
        {
            EndInteraction();
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            var interactable = colliders[i].GetComponent<IInteractable>();

            if (interactable != null)
            {
                // Debug.Log("Interactable in reach");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartInteraction(interactable);
                    continue;
                }
            }
        }
    }

    void StartInteraction(IInteractable interactable)
    {
        // Debug.Log("Interacting ...");
        interactable.Interact(this, out bool hasInteracted);
        IsInteracting = true;
    }

    void EndInteraction()
    {
        // Debug.Log("No longer interacting ...");
        IsInteracting = false;
    }
}