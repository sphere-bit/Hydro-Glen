#pragma warning disable 108

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class ItemCollectible : MonoBehaviour
{
    public float pickUpRadius = 1f;
    public ItemData itemData;

    public ItemData ItemData => itemData;
    public LayerMask obstacleLayerMask;
    private CircleCollider2D collider;
    private Rigidbody2D rb;

    void OnTriggerEnter2D(Collider2D other)
    {
        AdjustItemPosition();
    }
    void OnTriggerStay2D(Collider2D other)
    {
        AdjustItemPosition();
    }

    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
        collider.isTrigger = true;
        collider.radius = pickUpRadius;
    }
    private void AdjustItemPosition()
    {
        // Ensure the item is not placed on an obstacle
        // GameObject obstacles = GameObject.FindGameObjectWithTag("Obstacle");

        if (collider.IsTouchingLayers(obstacleLayerMask))
        {
            Player player = FindObjectOfType<Player>();
            Debug.Log($"{this.name} collision with {obstacleLayerMask}");
            Vector2 freePosition = player.transform.position;
            this.transform.position = freePosition;
            this.transform.parent = null;
            return;
        }
    }
}
