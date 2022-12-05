using System;
using UnityEngine;
using UnityEngine.Events;

public class ChestInventory : InventoryHolder, IInteractable
{
    protected override void Awake()
    {
        base.Awake();
        Persistence.OnLoadGame += LoadInventory;
    }

    private void Start()
    {
        var chestSaveData = new ChestSaveData(primaryInventory, transform.position, transform.rotation);

        SaveGameManager.gameData.chestDict.Add(GetComponent<Uid>().Id, chestSaveData);
    }

    private void LoadInventory(SaveData gameData)
    {
        // Check the inventory of the chest with a uid 
        if (gameData.chestDict.TryGetValue(GetComponent<Uid>().Id, out ChestSaveData chestData))
        {
            // TODO: move chest location 
            // Save position of chest
            this.primaryInventory = chestData.inventory;
            this.transform.position = chestData.position;
            this.transform.rotation = chestData.rotation;
        }
    }

    public UnityAction<IInteractable> OnInteractionComplete { get; set; }

    public void Interact(Interactor interactor, out bool hasInteracted)
    {
        OnDynamicInventoryDisplayRequested?.Invoke(primaryInventory);
        hasInteracted = true;
    }

    public void EndInteraction()
    {
        // Do something if player moves away from interactable.
        Debug.Log("Unimplemented: EndInteraction()");
    }
}

[Serializable]
public struct ChestSaveData
{
    public Inventory inventory;
    public Vector3 position;
    public Quaternion rotation;
    public ChestSaveData(Inventory _inventory, Vector3 _position, Quaternion _rotation)
    {
        inventory = _inventory;
        position = _position;
        rotation = _rotation;
    }
}