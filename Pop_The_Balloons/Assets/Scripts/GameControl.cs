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


    public float timer = 61f;      // zaman sayac� 61 olmal�
    private int score = 0;          // patlat�lan balon

    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        audioSource.volume = SoundLevels.sfxLevel;
    }


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
                timer -= Time.deltaTime;        // zaman sayac� saniyede 1 azals�n

                TimeText.text = "TIME: " + (int)timer;


            }
            else    // oyun bitti�i zaman
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
            Destroy(gameObjects[i]);        // kalan balonlar patlas�n
            Destroy(exp, 0.168f);       // patlama animasyonu yok edilsin
        }
    }


    public void AddBalloon()        // balon patlat�ld���nda kullan�lacak fonksiyon (BalloonControl Scripti i�erisinde)
    {
        audioSource.Stop();
        audioSource.Play();
        
        score++;
        ScoreText.text = "SCORE: " + score;
    }

    public void DecreaseBalloon()   // balon ka�t���nda kullan�lacak fonksiyon
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
