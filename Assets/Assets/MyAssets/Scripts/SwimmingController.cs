using UnityEngine;

public class SwimmingController : MonoBehaviour
{
    public float swimSpeed = 5f;             // Horizontal speed while swimming
    public float swimUpSpeed = 3f;           // Speed when swimming upwards (holding space)
    public float buoyancyForce = 12f;        // Buoyancy force to keep the player afloat
    public float waterDrag = 2f;             // Resistance in the water
    public float waterAngularDrag = 2f;      // Resistance to angular movement in the water
    public float gravityInWater = -1f;       // Reduced gravity in water
    public float floatDownSpeed = 1f;        // Speed at which the player floats down when not pressing space
    public float swimTiltForce = 1f;         // Upward tilt force when swimming (for realism)

    private Rigidbody rb;
    private bool isUnderwater = false;       // Tracks whether the player is underwater or not
    private Vector3 currentMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;  // Disable default gravity; we will handle gravity in water
    }

    // Trigger for entering water
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))  // Assuming all water bodies have a "Water" tag
        {
            EnterWater();
        }
    }

    // Trigger for exiting water
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            ExitWater();
        }
    }

    // When entering water
    private void EnterWater()
    {
        if (!isUnderwater)
        {
            isUnderwater = true;
            // Apply upward buoyancy force when entering the water
            rb.AddForce(Vector3.up * buoyancyForce, ForceMode.Acceleration);
            // Set drag and angular drag to simulate water resistance
            rb.drag = waterDrag;
            rb.angularDrag = waterAngularDrag;
        }
    }

    // When exiting water
    private void ExitWater()
    {
        if (isUnderwater)
        {
            isUnderwater = false;
            // Reset drag values when exiting the water
            rb.drag = 0f;
            rb.angularDrag = 0.05f;
        }
    }

    void Update()
    {
        // Only execute swimming logic if the player is in water
        if (isUnderwater)
        {
            // Get player movement input (WASD or arrows for horizontal movement)
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Handle swimming movement (horizontal)
            Swim(moveHorizontal, moveVertical);

            // Handle vertical movement (up/down)
            if (Input.GetKey(KeyCode.Space)) // Swim Up (e.g., pressing Space)
            {
                SwimUp();
            }
            else // Float Down when not holding space
            {
                FloatDown();
            }

            // Apply gravity-like force when underwater (modified gravity in water)
            rb.AddForce(Vector3.up * gravityInWater, ForceMode.Acceleration);

            // Tilt the player slightly forward when swimming (simulating swimming posture)
            rb.AddTorque(Vector3.right * swimTiltForce, ForceMode.Acceleration);
        }
    }

    // Handle horizontal swimming (left/right, forward/back)
    private void Swim(float moveHorizontal, float moveVertical)
    {
        // Smooth movement vector based on player input
        currentMovement = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        if (currentMovement.magnitude > 0)
        {
            // Apply movement force for swimming horizontally
            rb.AddForce(currentMovement * swimSpeed, ForceMode.Acceleration);
        }
    }

    // Handle swimming upwards (floating when holding space)
    private void SwimUp()
    {
        // Apply upward force for swimming upwards
        rb.AddForce(Vector3.up * swimUpSpeed, ForceMode.Acceleration);
    }

    // Handle floating down when not holding space (slow descent)
    private void FloatDown()
    {
        // Apply a slow downward force to simulate floating down
        rb.AddForce(Vector3.down * floatDownSpeed, ForceMode.Acceleration);
    }
}
