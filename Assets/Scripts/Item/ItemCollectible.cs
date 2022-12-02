using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ItemCollectible : MonoBehaviour
{
    public float pickUpRadius = 1f;
    public ItemData itemData;
    public ItemData ItemData => itemData;
    private new CircleCollider2D collider;
    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
        collider.isTrigger = true;
        collider.radius = pickUpRadius;
    }


}
