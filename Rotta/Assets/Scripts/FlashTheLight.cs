using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class FlashTheLight : MonoBehaviour
{
    // Start is called before the first frame update
    Light light;
    float originalIntensity = 0.1f;
    float currentIntensity = 0.1f;
    float flashIntensity = 35f;
    float flashTime = 100f;

    private void Awake()
    {
        light = GetComponent<Light>();
        originalIntensity = light.intensity;
        currentIntensity = originalIntensity;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        light.intensity = Mathf.MoveTowards(currentIntensity, originalIntensity, flashTime * Time.deltaTime);
        currentIntensity = light.intensity;
    }

    public void Flash()
    {
        light.intensity = flashIntensity;
        currentIntensity = light.intensity;
    }
}
