using System.Collections;
using System.Collections.Generic;
using Unity.AppUI.UI;
using UnityEngine;

public class MummoController : MonoBehaviour
{
    GameMaster gameMaster;
    public float speed = 3f;
    float limitX = 25f;
    float limitZ = 15f;
    public string prefix = "";
    public LayerMask layerMask;
    AudioSource shotgunSound;
    float shotgunCooldown = 1f;
    float shotgunTimer = 0f;
    public GameObject SmallGibs;
    float speedBoost = 0.15f;

    // Start is called before the first frame update
    private void Awake()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        shotgunSound = GetComponent<AudioSource>(); 
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

    private void Update()
    {

        if (shotgunTimer < 0f)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.5f, layerMask);

            foreach (Collider hitCollider in hitColliders)
            {
                Shoot(hitCollider);
            }
        }
        shotgunTimer -= Time.deltaTime;
        speed += speedBoost * Time.deltaTime;
    }

    void Shoot(Collider hitCollider)
    {

        shotgunTimer = shotgunCooldown;
        PlayerController controller = hitCollider.gameObject.GetComponent<PlayerController>();
        if (controller != null)
        {
            if (!controller.dead)
            {
                controller.Die();
            }
            Instantiate(SmallGibs, controller.transform.position, Quaternion.identity);
        }
        //effect

        shotgunSound.Play();
        if (gameMaster != null)
        {
            gameMaster.flashyLight.Flash();
        }
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
