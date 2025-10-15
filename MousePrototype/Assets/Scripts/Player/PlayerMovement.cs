using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 15;
    public float jump = 10;
    public Camera playerCamera;
    private Rigidbody rb;
    private Vector3 moveDir = Vector3.zero;
    [SerializeField] private float friction = 10;
    [SerializeField] private float gravity = 3;
    private float offset = 1;
    public static PlayerMovement Instance;

    //for wwise "events" (sound instances)
    [Header("Wwise Events")]
    //footstep for mouse
    public AK.Wwise.Event mouseFootstep;

    //to prevent footstep sound overlapping
    private bool footstepIsPlaying = false;
    //time since last footstep sound played
    private float lastFootstepTime = 0f;

    void Awake() {
        Instance = this;
        //initialize last footstep time to current time
        lastFootstepTime = Time.time;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");
        moveDir = ((transform.forward * yMove) + (transform.right * xMove)).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && IsGround())
        {
            rb.AddForce(transform.up * jump, ForceMode.Impulse);
        }
        //play footstep sound for mouse
        if (!footstepIsPlaying)
        {
            mouseFootstep.Post(gameObject);
            //sets last footstep time to current time
            lastFootstepTime = Time.time;
            footstepIsPlaying = true;
        }
        else
        {
            if (moveSpeed > 1)
            {
                if (Time.time - lastFootstepTime > 3100 / moveSpeed * Time.deltaTime)
                {
                    footstepIsPlaying = false;
                }
            }
        }
        
    }

    void FixedUpdate() 
    {
        rb.AddForce(moveDir.normalized * moveSpeed/10, ForceMode.Impulse);

        rb.linearVelocity = new Vector3(rb.linearVelocity.x * (100-friction)/100,
        rb.linearVelocity.y - gravity/10, rb.linearVelocity.z * (100-friction)/100);
    }

    private bool IsGround() 
    {
        return Physics.Raycast(transform.position, Vector3.down, offset + .15f);
    }
}
