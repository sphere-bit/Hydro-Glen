using UnityEngine;

[System.Serializable]
public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Vector2 movement;
    // [SerializeField] private ItemHolder itemHolder;
    [SerializeField] private Interactor interactor;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // itemHolder = GetComponent<ItemHolder>();
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
        // Based on Sprites > Player, set the interaction trace/point and placed item direction
        string spriteName = GetComponent<SpriteRenderer>().sprite.name;
        if (spriteName.Contains("Down"))
        {
            // itemHolder.Direction = new Vector3(0, -1);
            interactor.InteractionPoint.position = new Vector3(transform.position.x, transform.position.y - .5f);
        }
        else if (spriteName.Contains("Up"))
        {
            // itemHolder.Direction = new Vector3(0, 1);
            interactor.InteractionPoint.position = new Vector3(transform.position.x, transform.position.y + .5f);
        }
        else if (spriteName.Contains("Right"))
        {
            // itemHolder.Direction = new Vector3(1, 0);
            interactor.InteractionPoint.position = new Vector3(transform.position.x + .5f, transform.position.y - 1);
        }
        else if (spriteName.Contains("Left"))
        {
            // itemHolder.Direction = new Vector3(-1, 0);
            interactor.InteractionPoint.position = new Vector3(transform.position.x - .5f, transform.position.y - 1);
        }
    }

    void FixedUpdate()
    {
        Vector2 change = new Vector2(movement.x, movement.y);

        // if (change.sqrMagnitude > 0.1f)
        // {
        //     // don't store 0 as a direction
        //     // itemHolder.Direction = change.normalized;
        // }

        transform.Translate(speed * Time.fixedDeltaTime * change.normalized);
    }
}