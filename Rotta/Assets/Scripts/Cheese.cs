using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]   
public class Cheese : MonoBehaviour
{
    GameMaster gameMaster;
    public GameObject sound;
    bool dead = false;
    private void Awake()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (dead) return;
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            dead = true;
            gameMaster.RemoveCheese(gameObject, true);
            if (sound != null)
            {
                Instantiate(sound, transform.position, Quaternion.identity);
            }
        }
    }
}
