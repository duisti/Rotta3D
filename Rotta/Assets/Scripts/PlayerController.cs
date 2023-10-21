using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 20f;
    public float dashSpeed = 100f;
    float dashTimer = 3f;
    float timerDown = 3f;
    bool dashed = false;
    float rotationSpeed = 150f;
    Rigidbody rb;
    GameMaster gameMaster;
    public string prefix = "";

    public bool dead = false;

    public List<GameObject> DeathEffects = new List<GameObject>();
    public GameObject Model;

    private void Awake()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dead) return;
        float hzInput = Input.GetAxis(prefix+"Horizontal");
        float vrtInput = Input.GetAxis(prefix + "Vertical");
        Move(hzInput, vrtInput);
    }

    private void Update()
    {
        if (dead) return;
        float hzInput = Input.GetAxis(prefix + "Horizontal");
        float vrtInput = Input.GetAxis(prefix + "Vertical");
        if (Input.GetButton(prefix + "Fire1") && !dashed)
        {
            StartCoroutine(Dash(hzInput, vrtInput));
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Die();
        }
    }
    private IEnumerator Dash(float hz, float vrt)
    {
        Vector3 inputDir = new Vector3(hz, 0f, vrt);
        if (inputDir == Vector3.zero)
        {
            yield break;
        }
        dashed = true;
        timerDown = dashTimer;
        print("dashing");
        Vector3 force = new Vector3(hz, 0, vrt);
        force = Vector3.Normalize(force);
        rb.AddForce(force * dashSpeed, ForceMode.Impulse);
        while (timerDown > 0)
        {
            timerDown -= Time.deltaTime;
            yield return null;
        }

        dashed = false;

        // Perform desired actions when the countdown is finished, e.g., play a sound or show a message
    }
    private void Move(float horizontal, float vertical)
    {
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical) * speed;
        //rb.MovePosition(transform.position + moveDirection * Time.fixedDeltaTime);
        //rb.MovePosition(transform.position + moveDirection * Time.fixedDeltaTime);
        rb.velocity = rb.velocity + moveDirection * Time.fixedDeltaTime;
        rb.MoveRotation(LookDirection(horizontal, vertical));
    }

    private Quaternion LookDirection(float horizontalInput, float verticalInput)
    {
        Vector3 lookDir = new Vector3(horizontalInput, 0f, verticalInput);
        Quaternion returned = Quaternion.identity;
            if (lookDir != Vector3.zero)
            {
                lookDir = lookDir.normalized;
                Quaternion lookRotation = Quaternion.LookRotation(lookDir, Vector3.up);

                returned = Quaternion.Lerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            }
            else
        {
            returned = transform.rotation;
        }

        return returned;
    }

    public void Die()
    {
        dead = true;
        // effects
        if (DeathEffects.Count != 0)
        {
            foreach (GameObject g in DeathEffects)
            {
            Instantiate(g, transform.position, Quaternion.identity);
            }
        }
        // model
        if (Model != null)
        {
            //Model.SetActive(false);
        }
    }

}
