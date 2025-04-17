using UnityEngine;

public class WaterSource : MonoBehaviour
{
    public float hydrationAmount = 10f; // Amount of hydration restored

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            PlayerState.Instance.setHydration(PlayerState.Instance.currentHydrationPercent + hydrationAmount);
        }
    }
}
