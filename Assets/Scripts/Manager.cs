using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 using UnityEngine.SceneManagement;
using TMPro;
using System;


public class Manager : MonoBehaviour
{
    public GameObject GameOver;
    public GameObject GetReady;
    public GameObject play;
    public GameObject Replay;
    public TMP_Text Score;
    public TMP_Text BestScore;
    public TMP_Text congrat;
    public TMP_Text YourScore;
    public int score=0;
    private int maxScore;
    public int[] Scores= new int[1];
    public GameObject bird;
    public GameObject Player;
    public FaceDetector Facecamera;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale=0f;
        GameOver.SetActive(false);
        Replay.SetActive(false);
        maxScore = PlayerPrefs.GetInt("BestScore", 0);
        bird=GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        Score.SetText(""+score);

    }

    public void Restart (){
        score=0;
        Time.timeScale=1f;
        GetReady.SetActive(false);
        Replay.SetActive(false);
        GameOver.SetActive(false);
        if(bird==null){
            Instantiate(Player);
        }
        string currentScene = SceneManager.GetActiveScene().name; 
        SceneManager.LoadScene(currentScene);
      
    }
     public void playBtn(){
        Time.timeScale=1f;
        GetReady.SetActive(false);
        play.SetActive(false);
        Replay.SetActive(false);
        GameOver.SetActive(false);
        Facecamera._webCamTexture.Play();

    }
    public void Gameover(){
        GameOver.SetActive(true);
        Time.timeScale=0f;
        Replay.SetActive(true);
        Array.Resize(ref Scores, Scores.Length + 1);
        Scores[Scores.Length - 1] = score;        
        Facecamera._webCamTexture.Stop();
        for (int i = 0; i < Scores.Length; i++)
        {
            if (Scores[i] > maxScore)
            {
                maxScore = Scores[i];
            }
        }
        if(maxScore==score){
            congrat.SetText("New Best Score!");
        }
        BestScore.SetText("Best Score:"+maxScore);
        YourScore.SetText("Your Score:"+score);
        PlayerPrefs.SetInt("BestScore", maxScore);
        PlayerPrefs.Save();
    }
}
