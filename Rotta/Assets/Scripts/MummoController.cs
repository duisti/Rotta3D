using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummoController : MonoBehaviour
{
    GameMaster gameMaster;
    public float speed = 3f;
    float limitX = 25f;
    float limitZ = 15f;
    public string prefix = "";

    // Start is called before the first frame update
    private void Awake()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hzInput = Input.GetAxis(prefix + "Horizontal");
        float vrtInput = Input.GetAxis(prefix + "Vertical");
        Move(hzInput, vrtInput);

    }

    private void Move(float horizontal, float vertical)
    {
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical) * speed * Time.fixedDeltaTime;
        Vector3 destination = transform.position + moveDirection;
        destination.x = Mathf.Clamp(destination.x, -limitX, limitX);
        destination.z = Mathf.Clamp(destination.z, -limitZ, limitZ);
        //rb.MovePosition(transform.position + moveDirection * Time.fixedDeltaTime);
        transform.position = destination;
    }
}
