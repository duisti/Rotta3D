using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureUpdater : MonoBehaviour
{
    public GameMaster gameMaster;
    public Image picture;
    public int indexToRead = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (indexToRead == gameMaster.currentRat)
        {
            picture.sprite = gameMaster.images[0];
        }
        else picture.sprite = gameMaster.images[1];
    }
}
