using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 15;
    public float jump = 10;
    public Camera playerCamera;
    private Rigidbody rigidBody;
    private Vector3 moveDir = Vector3.zero;
    [SerializeField] private float friction = 10;
    [SerializeField] private float gravity = 3;
    private float offset = 1;
    public static PlayerMovement Instance;

    void Awake() {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");
        moveDir = ((transform.forward * yMove) + (transform.right * xMove)).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && IsGround()) {
            rigidBody.AddForce(transform.up * jump, ForceMode.Impulse);
        }
    }

    void FixedUpdate() {
        rigidBody.AddForce(moveDir.normalized * moveSpeed/10, ForceMode.Impulse);

        rigidBody.linearVelocity = new Vector3(rigidBody.linearVelocity.x * (100-friction)/100,
        rigidBody.linearVelocity.y - gravity/10, rigidBody.linearVelocity.z * (100-friction)/100);
    }

    private bool IsGround() {
        return Physics.Raycast(transform.position, Vector3.down, offset + .15f);
    }
}
