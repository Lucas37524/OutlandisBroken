using UnityEngine;

public class EyeLevelWaterTrigger : MonoBehaviour
{
    public AudioClip waterSound;  // The sound to play when touching water
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Get the AudioSource component attached to the EyeLevel object
        audioSource = GetComponent<AudioSource>();

        // Ensure the AudioSource is present
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource found on EyeLevel object!");
        }
    }

    // Detect collision with an object tagged "Water"
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            // Play the water sound if there is one
            if (waterSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(waterSound);
            }
            else
            {
                Debug.LogWarning("No water sound assigned to EyeLevel.");
            }
        }
    }
}
