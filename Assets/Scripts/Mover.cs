using UnityEngine;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    [Header("이동")]
    public Transform forwardTransform;
    [Range(1f, 100f)]
    public float moveSpeed = 3f;

    [Space(20f), Header("점프")]
    [Range(0.5f, 10f)]
    public float jumpHeight = 2f;
    public int maxJumpCount = 1;
    public LayerMask groundLayer;
    [Range(0.1f, 10f)]
    public float groundRadius = 0.3f;
    public float groundOffset = 0f;

    public bool Grounded
    {
        get => _isGrounded;
        private set
        {
            if (_isGrounded == value)
                return;

            _isGrounded = value;
            if(value)
            {
                _remainJumpCount = maxJumpCount;
            }
        }
    }
    private bool _isGrounded;
    private int _remainJumpCount;

    private Rigidbody _rigidBody;
    private Vector2 _direction;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue value)
    {
        _direction = value.Get<Vector2>();
    }

    public void OnJump()
    {
        if (_remainJumpCount == 0)
            return;

        _rigidBody.AddForce(
            Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y) * Vector3.up, ForceMode.VelocityChange);
    }

    private void FixedUpdate()
    {
        Vector3 forward = forwardTransform.forward;
        Vector3 right = forwardTransform.right;
        forward.y = 0f;
        forward.Normalize();
        right.y = 0f;
        right.Normalize();

        Vector2 direction = moveSpeed * Time.fixedDeltaTime * _direction;
        Vector3 newPos = (direction.y * forward) + (direction.x * right) + _rigidBody.position;
        _rigidBody.MovePosition(newPos);
    }

    private void Update()
    {
        Grounded = GroundCheck();
    }

    //private void Update()
    //{
    //    Vector3 forward = forwardTransform.forward;
    //    Vector3 right = forwardTransform.right;
    //    forward.y = 0f;
    //    forward.Normalize();
    //    right.y = 0f;
    //    right.Normalize();

    //    Vector2 direction = moveSpeed * Time.deltaTime * _direction;
    //    Vector3 newPos = (direction.y * forward) + (direction.x * right);
    //    transform.Translate(newPos, Space.World);
    //}

    private bool GroundCheck()
    {
        Vector3 gPos = transform.position;
        gPos.y += groundOffset;

        return Physics.CheckSphere(gPos, groundRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (_isGrounded)
            Gizmos.color = transparentGreen;
        else
            Gizmos.color = transparentRed;

        Gizmos.DrawSphere(
            new Vector3(transform.position.x, transform.position.y + groundOffset, transform.position.z),
            groundRadius
            );
    }
}
