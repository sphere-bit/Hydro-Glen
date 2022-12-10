using UnityEngine;
using UnityEngine.Events;

public class SavePoint : MonoBehaviour, IInteractable
{
    public static UnityAction OnPersistentMenuRequested;
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }

    public void EndInteraction()
    {
        Debug.Log("Unimplemented: EndInteraction()");
    }

    public void Interact(Interactor interactor, out bool hasInteracted)
    {
        OnPersistentMenuRequested?.Invoke();
        hasInteracted = true;
    }
}
