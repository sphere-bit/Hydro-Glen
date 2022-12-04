using UnityEngine;
using UnityEngine.Events;

public class ChestInventory : InventoryHolder, IInteractable
{
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }

    public void Interact(Interactor interactor, out bool hasInteracted)
    {
        OnDynamicInventoryDisplayRequested?.Invoke(inventory);
        hasInteracted = true;
    }

    public void EndInteraction()
    {
        // Do something if player moves away from interactable.
        Debug.Log("Unimplemented: EndInteraction()");
    }
}
