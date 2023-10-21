using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GibSpawner : MonoBehaviour
{
    public GameObject gib;
    public int gibMin = 7;
    public int gibMax = 9;
    // Start is called before the first frame update
    void Start()
    {
        if (gib == null) return;
        int random = Random.Range(gibMin, gibMax);
        float magicNumber = 0.4f; // here i pull a magical number out of my asshole
        for (int i = 0; i < random; i++)
        {
            GameObject go = Instantiate(gib, transform.position + new Vector3(Random.Range(-magicNumber, magicNumber), Random.Range(-magicNumber, magicNumber)), Random.rotation) as GameObject;
            var rb = go.GetComponent<Rigidbody>();
            Vector3 randomDirection = Random.insideUnitSphere;
            randomDirection.Normalize();
            rb.AddForce(randomDirection * Random.Range(0.1f, 1f),  ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
