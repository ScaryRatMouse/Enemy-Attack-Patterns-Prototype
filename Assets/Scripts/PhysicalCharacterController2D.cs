using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicalCharacterController2D : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float drag = 6f;
    public float moveMultiplier = 10f;

    private Vector2 _moveDirection = Vector2.zero;
    private Rigidbody2D _rb;

    private float _v = 0f;
    private float _h = 0f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Ensure rotation stays constant
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");
        _moveDirection = transform.up * _v + transform.right * _h;

        _rb.drag = drag;

        var flatVelocity = new Vector2(_rb.velocity.x, _rb.velocity.y);
        if (flatVelocity.magnitude <= moveSpeed)
            return;
        var limitVelocity = flatVelocity.normalized * moveSpeed;
        _rb.velocity = new Vector2(limitVelocity.x, limitVelocity.y);
    }

    void FixedUpdate()
    {
        _rb.AddForce(_moveDirection.normalized * (moveSpeed * moveMultiplier), ForceMode2D.Force);
    }
}
