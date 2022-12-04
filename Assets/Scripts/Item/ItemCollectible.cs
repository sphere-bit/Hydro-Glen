#pragma warning disable 108

using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ItemCollectible : MonoBehaviour
{
    public float pickUpRadius = 1f;
    public ItemData itemData;
    public ItemData ItemData => itemData;
    private CircleCollider2D collider;
    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
        collider.isTrigger = true;
        collider.radius = pickUpRadius;
    }


}
