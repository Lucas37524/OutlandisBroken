using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; set; }

    // ---- Player Health ---- //
    public float currentHealth;
    public float maxHealth;

    // ---- Player Calories ---- //
    public float currentCalories;
    public float maxCalories;

    float distanceTravelled = 0;
    Vector3 lastPosition;

    public GameObject playerBody;

    // ---- Player Hydration ---- //
    public float currentHydrationPercent;
    public float maxHydrationPercent;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        currentCalories = maxCalories;
        currentHydrationPercent = maxHydrationPercent;

        StartCoroutine(DecreaseHydration());
        StartCoroutine(CheckHealthDecay()); // Start health decay coroutine
    }

    IEnumerator DecreaseHydration()
    {
        while (true)
        {
            if (currentHydrationPercent > 0)
            {
                currentHydrationPercent -= 1;
            }

            yield return new WaitForSeconds(10);
        }
    }

    IEnumerator CheckHealthDecay()
    {
        while (true)
        {
            if (currentHydrationPercent < 20 || currentCalories < 300)
            {
                currentHealth -= 2;
                currentHealth = Mathf.Max(currentHealth, 0); // Ensure health doesn't go below 0
            }

            yield return new WaitForSeconds(2); // Health decreases every 5 seconds if conditions are met
        }
    }

    void Update()
    {
        distanceTravelled += Vector3.Distance(playerBody.transform.position, lastPosition);
        lastPosition = playerBody.transform.position;

        if (distanceTravelled >= 4)
        {
            distanceTravelled = 0;
            if (currentCalories > 0)
            {
                currentCalories -= 1;
            }
        }
    }

    public void setHealth(float newHealth)
    {
        currentHealth = Mathf.Clamp(newHealth, 0, maxHealth);
    }

    public void setCalories(float newCalories)
    {
        currentCalories = Mathf.Clamp(newCalories, 0, maxCalories);
    }

    public void setHydration(float newHydration)
    {
        currentHydrationPercent = Mathf.Clamp(newHydration, 0, maxHydrationPercent);
    }
}
