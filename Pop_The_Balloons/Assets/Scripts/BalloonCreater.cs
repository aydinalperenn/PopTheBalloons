using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonCreater : MonoBehaviour
{
    GameControl gameControlScript;  // game control scriptinden obje olu�turduk

    public GameObject balloon;      // prefab
    float coolDown = 0.75f;         // balonu ka� saniyede bir olu�turmak istiyorsak
    float timer = 0f;               // zaman sayac�

    float leftBound;
    float rightBound;

    void Start()
    {
        gameControlScript = this.gameObject.GetComponent<GameControl>();    // ayn� objenin �zerinde bulunan GameControl Script Coponentini referans oalrak verdik

        // Ekran�n s�n�rlar�n� dinamik olarak belirleyelim
        leftBound = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z)).x + 0.5f;
        rightBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, Camera.main.transform.position.z)).x - 0.5f;
    }

    void Update()
    {
        int temp;       // zaman ilerledik�e balonlar�n h�zlanmas� i�in bir ge�ici de�i�ken
        temp = (int)(gameControlScript.timer / 10) - 6;     // zaman ge�tik�e 10 saniyede bir de�eri d��s�n: -1, -2, -3... , -6
        temp *= -1;     // - ile �arp�ls�n
        if (temp == 0)   // ba�lang�cta balonlar hareket etmeden a�a��da kal�yordu, bu hatay� d�zeltmek i�in
        {
            temp = 1;
        }

        timer -= Time.deltaTime;    // zaman sayac� her saniye d��s�n
        if (timer < 0 && gameControlScript.timer > 0 && gameControlScript.isGameContinue == true)
        {
            GameObject go = Instantiate(balloon, new Vector3(UnityEngine.Random.Range(leftBound, rightBound), -6f, 0), Quaternion.identity) as GameObject;  // zaman sayac�n 0'�n alt�na d��t���nde obje olu�tur
            go.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, UnityEngine.Random.Range(40f * temp, 110f * temp), 0));  // yukar� ��kmas� i�in bir kuvvet uygulad�k (h�z� rastgele belirleniyor)
            timer = coolDown;
        }
    }
}
