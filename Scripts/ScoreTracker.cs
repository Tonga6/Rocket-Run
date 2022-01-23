using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    TextMeshProUGUI score;
    GameObject way;
    int currScore;

    void Start()
    {
        way = GameObject.FindGameObjectWithTag("Way");
        score = GetComponent<TextMeshProUGUI>();
        currScore = 0;
    }

    public void UpdateScore()
    {
        currScore++;
        score.text = GameControl.currentScore.ToString();
        if (currScore > PlayerPrefs.GetInt("HighScore", 0))
            score.color = new Color32(100, 225, 164, 170); 
    }
}
