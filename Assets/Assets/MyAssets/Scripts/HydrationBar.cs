using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HydrationBar : MonoBehaviour
{
    // Start is called before the first frame update

    private Slider slider;
    public Text hydrationCounter;

    public GameObject playerState;

    private float currentHydration, maxHydration;
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHydration = playerState.GetComponent<PlayerState>().currentHydrationPercent;
        maxHydration = playerState.GetComponent<PlayerState>().maxHydrationPercent;

        float fillValue = currentHydration / maxHydration; // 0 - 1
        slider.value = fillValue;

        hydrationCounter.text = currentHydration + "%"; 
    }
}


