using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static int highScore;
    public static int currentScore;
    
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<Music>().PlayMusic();
    }
    public static void IncScore()
    {
        currentScore++;        
    }
 

    public static void RunOver()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (currentScore > highScore) {
            Debug.Log("New Highscore: " + currentScore);
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save();
        }
        currentScore = 0;
        SceneManager.LoadScene("Waypoint Mode");
    }
}
