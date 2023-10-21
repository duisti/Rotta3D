using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{

    public GameMaster gameMaster;
    public int indexToRead = 0;
    public TMP_Text text;
    // Start is called before the first frame update

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float score = gameMaster.scores[indexToRead];
        score = Mathf.Round(score);
        text.text = score.ToString(); 
    }
}
