using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = 0.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "score:" + QuickTimeRush.score.ToString();
    }
}
