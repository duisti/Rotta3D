using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    // Start is called before the first frame update
    public FlashTheLight flashyLight;
    public GameObject playerPrefab;
    public GameObject mummoPrefab;

    public GameObject currentRotta;
    public GameObject currentMummo;

    public AudioSource countDown;

    public bool playing = false;
    public bool gameOver = false;
    float countDownTimer = 5f;

    float resetTimer = 3f;

    public List<float> scores = new List<float>();
    public List<string> prefixes = new List<string>();

    public int currentRat = 0;

    float initialStartTimer = 8f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (initialStartTimer > 0f)
        {
            initialStartTimer -= Time.deltaTime;
            return;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Instantiate(playerPrefab, new Vector3(0, 5f, 0), Quaternion.identity);
        }
        if (!playing)
        {
            StartCoroutine(GameCountDown());    
        } else
        {
            if (currentMummo != null && currentRotta != null) {
            if (currentRotta.GetComponent<PlayerController>().dead && !gameOver)
                {
                    StartCoroutine(ResetGame());
                }
            }
        }
    }

    private IEnumerator GameCountDown()
    {
        playing = true;
        gameOver = false;
        currentRotta = Instantiate(playerPrefab, Vector3.up, Quaternion.identity) as GameObject;
        var script = currentRotta.GetComponent<PlayerController>();
        script.prefix = prefixes[currentRat];
        countDownTimer = 5f;
        countDown.Play();
        while (countDownTimer > 0f)
        {
            countDownTimer -= Time.deltaTime;
            yield return null;
        }
        currentMummo = Instantiate(mummoPrefab, Vector3.up, Quaternion.identity) as GameObject;
        var mummoScript = currentMummo.GetComponent<MummoController>();
        if (currentRat == 0)
        {
            mummoScript.prefix = prefixes[1];
        }
        else mummoScript.prefix = prefixes[0];
    }

    private IEnumerator ResetGame()
    {
        //logiikkaa
        gameOver = true;
        resetTimer = 3f;
        while (resetTimer > 0f)
        {
            resetTimer -= Time.deltaTime;
            yield return null;
        }
        //logiikkaa
        Destroy(currentRotta);
        Destroy(currentMummo);
        currentRat++;
        if (currentRat > 1)
        {
            currentRat = 0;
        }
        playing = false;
    }
}
