using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    // [SerializeField]
    // private Rigidbody2D rb;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private Vector2 _movement;
    [SerializeField]
    private PickUp _pickUp;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _pickUp = GetComponent<PickUp>();
    }

    // Update is called once per frame
    void Update()
    {
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");
        _animator.SetFloat("Horizontal", _movement.x);
        _animator.SetFloat("Vertical", _movement.y);
        _animator.SetFloat("Speed", _movement.sqrMagnitude);
        adjustPickUpDirection();

        // transform.Translate(_speed * Time.deltaTime * new Vector3(_movement.x, _movement.y, 0));
        // transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 3.84f), 0);
    }

    private void adjustPickUpDirection()
    {
        // Based on which direction the player is facing while idle, set the addition
        // direction vector of the picked up collectible.
        string spriteName = GetComponent<SpriteRenderer>().sprite.name;
        if (spriteName == "PlayerIdleDown")
            _pickUp.Direction = new Vector3(0, -1);
        else if (spriteName == "PlayerIdleUp")
            _pickUp.Direction = new Vector3(0, 1);
        else if (spriteName == "PlayerIdleRight")
            _pickUp.Direction = new Vector3(1, 0);
        else if (spriteName == "PlayerIdleLeft")
            _pickUp.Direction = new Vector3(-1, 0);
    }

    void FixedUpdate()
    {
        Vector2 change = new Vector2(_movement.x, _movement.y);

        if (change.sqrMagnitude > 0.1f)
        {
            // don't store 0 as a direction
            _pickUp.Direction = change.normalized;
        }

        transform.Translate(_speed * Time.fixedDeltaTime * change.normalized);
    }
}