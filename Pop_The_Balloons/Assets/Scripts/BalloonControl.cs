using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonControl : MonoBehaviour
{
    public GameObject explosionPrefab;      // patlama animasyonu prefab�
    GameControl control;

    private void Start()
    {
        // ismi _Scripts_General olan oyun objesinin GameControl isimli componentini (script dosyas�n�) bizim script de�i�kenimie atad�k
        control = GameObject.Find("_Scripts_General").GetComponent<GameControl>();
    }

    private void Update()
    {

        if(this.gameObject.transform.position.y > 5.86f)
        {
            control.DecreaseBalloon();
            Destroy(this.gameObject);
        }
        
    }


    private void OnMouseDown()
    {
        GameObject go = Instantiate(explosionPrefab, transform.position, transform.rotation) as GameObject;
        Destroy(this.gameObject);
        Destroy(go, 0.168f);
        control.AddBalloon();
    }

}
