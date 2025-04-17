using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    //Swimming
    [HideInInspector]
    public bool isSwimming;
    public float swimSpeed;
    public Transform target;

    //Normal
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();



    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (InventorySystem.Instance.isOpen == false) 
        {
            if (isSwimming != true)
            {
                if (rigidbody.useGravity != true)
                {
                    rigidbody.useGravity = true;
                }
                // Update IsRunning from input.
                IsRunning = canRun && Input.GetKey(runningKey);

                // Get targetMovingSpeed.
                float targetMovingSpeed = IsRunning ? runSpeed : speed;
                if (speedOverrides.Count > 0)
                {
                    targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
                }

                // Get targetVelocity from input.
                Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

                // Apply movement.
                rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
            }
            else
            {
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    transform.position += target.forward * swimSpeed * Time.deltaTime;
                }
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    transform.position -= target.forward * swimSpeed * Time.deltaTime;
                }
            }
        }
            
    }
    public void ResetVelocity()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
}
