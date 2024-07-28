using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonCreater : MonoBehaviour
{
    GameControl gameControlScript;  // game control scriptinden obje oluþturduk

    public GameObject balloon;      // prefab
    float coolDown = 0.75f;         // balonu kaç saniyede bir oluþturmak istiyorsak
    float timer = 0f;               // zaman sayacý

    float leftBound;
    float rightBound;

    void Start()
    {
        gameControlScript = this.gameObject.GetComponent<GameControl>();    // ayný objenin üzerinde bulunan GameControl Script Coponentini referans oalrak verdik

        // Ekranýn sýnýrlarýný dinamik olarak belirleyelim
        leftBound = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z)).x + 0.5f;
        rightBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, Camera.main.transform.position.z)).x - 0.5f;
    }

    void Update()
    {
        int temp;       // zaman ilerledikçe balonlarýn hýzlanmasý için bir geçici deðiþken
        temp = (int)(gameControlScript.timer / 10) - 6;     // zaman geçtikçe 10 saniyede bir deðeri düþsün: -1, -2, -3... , -6
        temp *= -1;     // - ile çarpýlsýn
        if (temp == 0)   // baþlangýcta balonlar hareket etmeden aþaðýda kalýyordu, bu hatayý düzeltmek için
        {
            temp = 1;
        }

        timer -= Time.deltaTime;    // zaman sayacý her saniye düþsün
        if (timer < 0 && gameControlScript.timer > 0 && gameControlScript.isGameContinue == true)
        {
            GameObject go = Instantiate(balloon, new Vector3(UnityEngine.Random.Range(leftBound, rightBound), -6f, 0), Quaternion.identity) as GameObject;  // zaman sayacýn 0'ýn altýna düþtüðünde obje oluþtur
            go.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, UnityEngine.Random.Range(40f * temp, 110f * temp), 0));  // yukarý çýkmasý için bir kuvvet uyguladýk (hýzý rastgele belirleniyor)
            timer = coolDown;
        }
    }
}
