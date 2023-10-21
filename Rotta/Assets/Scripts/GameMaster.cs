using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    // Start is called before the first frame update
    public FlashTheLight flashyLight;
    public GameObject playerPrefab;
    public GameObject mummoPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Instantiate(playerPrefab, new Vector3(0, 5f, 0), Quaternion.identity);
        }
    }
}
