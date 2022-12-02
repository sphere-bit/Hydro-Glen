using UnityEngine;

[System.Serializable]
public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Vector2 movement;
    [SerializeField] private ItemHolder itemHolder;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        itemHolder = GetComponent<ItemHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        adjustitemHolderDirection();

        // transform.Translate(speed * Time.deltaTime * new Vector3(movement.x, movement.y, 0));
        // transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 3.84f), 0);
    }

    private void adjustitemHolderDirection()
    {
        // Based on which direction the player is facing while idle, set the addition
        // direction vector of the picked up collectible.
        string spriteName = GetComponent<SpriteRenderer>().sprite.name;
        if (spriteName == "PlayerIdleDown")
            itemHolder.Direction = new Vector3(0, -1);
        else if (spriteName == "PlayerIdleUp")
            itemHolder.Direction = new Vector3(0, 1);
        else if (spriteName == "PlayerIdleRight")
            itemHolder.Direction = new Vector3(1, 0);
        else if (spriteName == "PlayerIdleLeft")
            itemHolder.Direction = new Vector3(-1, 0);
    }

    void FixedUpdate()
    {
        Vector2 change = new Vector2(movement.x, movement.y);

        if (change.sqrMagnitude > 0.1f)
        {
            // don't store 0 as a direction
            itemHolder.Direction = change.normalized;
        }

        transform.Translate(speed * Time.fixedDeltaTime * change.normalized);
    }
}