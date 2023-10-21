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
    public List<Sprite> images = new List<Sprite>();

    public GameObject cheeseObject;

    public List<Transform> spawnPoints = new List<Transform>();
    public List<Transform> usedSpawnPoints = new List<Transform>();

    public List<GameObject> placedCheese = new List<GameObject>();

    public float SpawnTime = 5f;
    float spawnTimerCount = 5f;

    public int currentRat = 0;

    float initialStartTimer = 8f;
    void Start()
    {
        
    }

    public void RemoveCheese (GameObject cheese, bool addScore)
    {
        int index = 0;
        for (int i = 0; i < placedCheese.Count; i++)
        {
            if (cheese == placedCheese[i])
            {
                index = i;
            }
        }
        GameObject g = placedCheese[index];
        placedCheese.RemoveAt(index);
        usedSpawnPoints.RemoveAt(index);
        if (addScore)
        {
            scores[currentRat] += 30;
        }
        Destroy(g);
    }

    void HandleCheese()
    {
        if (!playing || gameOver) return;
        spawnTimerCount -= Time.deltaTime;
        if (spawnTimerCount <= 0f)
        {
            List<Transform> notUsedPoints = new List<Transform>();
            foreach (Transform p in spawnPoints)
            {
                if (!usedSpawnPoints.Contains(p))
                {
                    notUsedPoints.Add(p);
                }
            }
            if (notUsedPoints.Count != 0) {
                var randomTrans = notUsedPoints[(int)Random.Range(0, notUsedPoints.Count)];
                placedCheese.Add(Instantiate (cheeseObject, randomTrans.position, Quaternion.identity) as GameObject);
                usedSpawnPoints.Add(randomTrans);
            }
            spawnTimerCount = SpawnTime;
        }
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
            scores[currentRat] += 1 * Time.deltaTime;
            if (currentMummo != null && currentRotta != null) {
            if (currentRotta.GetComponent<PlayerController>().dead && !gameOver)
                {
                    gameOver = true;
                    StartCoroutine(ResetGame());
                    /*
                    List<GameObject> list = new List<GameObject>();
                    foreach (GameObject g in placedCheese)
                    {
                        list.Add(g);
                    }
                    placedCheese = new List<GameObject>();
                    usedSpawnPoints = new List<Transform>();
                    foreach (GameObject g in list)
                    {
                        if (g != null)
                        {
                            Destroy(g);
                        }
                    }
                    */
                }
            }
            HandleCheese();
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
