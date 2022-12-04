using UnityEngine;

public class Interactor
{
    public Transform InteractionPoint;
    public LayerMask InteractionLayer;
    public float InteractionPointRadius = 1f;
    public bool IsInteracting { get; private set; }

    public void Update()
    {
        var colliders = Physics.OverlapSphere(InteractionPoint.position, InteractionPointRadius, InteractionLayer);

        if (Input.GetKeyDown(KeyCode.F))
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                var interactable = colliders[i].GetComponent<IInteractable>();

                if (interactable != null)
                {
                    startInteraction(interactable);
                }
            }
        }
    }

    private void startInteraction(IInteractable interactable)
    {
        interactable.Interact(this, out bool hasInteracted);
    }
}