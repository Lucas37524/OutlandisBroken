using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    FirstPersonMovement movement;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<FirstPersonMovement>() != null)
        {
            movement = other.GetComponent<FirstPersonMovement>();
            movement.isSwimming = true;
            other.GetComponent<Jump>().enabled = false;
            other.GetComponent <Crouch>().enabled = false;
        }
        if (other.CompareTag("EyeLevel"))
        {
            other.GetComponentInParent<Rigidbody>().useGravity = false;
            if(movement != null)
            {
                movement.ResetVelocity();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<FirstPersonMovement>() != null)
        {
            FirstPersonMovement movement = other.GetComponent<FirstPersonMovement>();
            movement.isSwimming = false;
            other.GetComponent<Jump>().enabled = true;
            other.GetComponent<Crouch>().enabled = true;
            movement.ResetVelocity();
        }
    }
}
