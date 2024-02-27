using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public bool isGameContinue;

    public GameObject explosionPrefab;

    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI ScoreText;

    public TextMeshProUGUI FinalText;
    public Button btnRestart;
    public Button btnMainMenu;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject newHighScore;


    public float timer = 61f;      // zaman sayacý 61 olmalý
    private int score = 0;          // patlatýlan balon

    [SerializeField] private AudioSource audioSource;



    void Start()
    {
        btnRestart.gameObject.SetActive(false);
        btnMainMenu.gameObject.SetActive(false);
        
        TimeText.text = "TIME: 60";
        ScoreText.text = "SCORE: " + score;

        isGameContinue = true;
    }


    void Update()
    {
        if(isGameContinue == true)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;        // zaman sayacý saniyede 1 azalsýn

                TimeText.text = "TIME: " + (int)timer;


            }
            else    // oyun bittiði zaman
            {
                audioSource.Stop();
                audioSource.Play();

                isGameContinue = false;
                DestroyAll();
                StartCoroutine(Wait());
                
                int highScore = PlayerPrefs.GetInt("HighScore", 0);

                if(score > highScore)
                {
                    highScoreText.text = "HighScore: " + score;
                    newHighScore.SetActive(true);
                    PlayerPrefs.SetInt("HighScore", score);
                }
                else if (score < highScore)
                {
                    highScoreText.text = "HighScore: " + highScore;
                }

                FinalText.text = "Level Completed!";
                btnRestart.gameObject.SetActive(true);
                btnMainMenu.gameObject.SetActive(true);
            }
        }      
    }

    public void DestroyAll()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Balloon");
        for (int i = 0; i < gameObjects.Length; i++)
        {
            GameObject exp = Instantiate(explosionPrefab, gameObjects[i].transform.position, Quaternion.identity) as GameObject;   // patlama animasyonu 
            Destroy(gameObjects[i]);        // kalan balonlar patlasýn
            Destroy(exp, 0.168f);       // patlama animasyonu yok edilsin
        }
    }


    public void AddBalloon()        // balon patlatýldýðýnda kullanýlacak fonksiyon (BalloonControl Scripti içerisinde)
    {
        audioSource.Stop();
        audioSource.Play();
        
        score++;
        ScoreText.text = "SCORE: " + score;
    }

    public void DecreaseBalloon()   // balon kaçtýðýnda kullanýlacak fonksiyon
    {
        score--;
        
        if(score < 0)
        {
            audioSource.Stop();
            audioSource.Play();

            isGameContinue = false;
            DestroyAll();
            StartCoroutine(Wait());
            ScoreText.text = "SCORE: -";
            FinalText.text = "GAME OVER!";

            highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore", 0);
            btnRestart.gameObject.SetActive(true);
            btnMainMenu.gameObject.SetActive(true);
            
        }
        else
        {
            ScoreText.text = "SCORE: " + score;
        }
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
    }

}
