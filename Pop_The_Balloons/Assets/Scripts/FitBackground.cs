using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitBackground : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private Camera mainCam;

    private void Awake()
    {
        ScaleBackgroundImage();
    }

    private void ScaleBackgroundImage()
    {
        Vector2 deviceResolution = new Vector2 (Screen.width, Screen.height);

        float srcHeight = Screen.height;
        float srcWidth = Screen.width;

        float DEVICE_SCREEN_ASPECT = srcWidth / srcHeight;

        mainCam.aspect = DEVICE_SCREEN_ASPECT;

        float camHeight = 100.0f * mainCam.orthographicSize * 2.0f;
        float camWidth = camHeight * DEVICE_SCREEN_ASPECT;

        SpriteRenderer backgroundImageSR = background.GetComponent<SpriteRenderer>();
        float bgImgH = backgroundImageSR.sprite.rect.height;
        float bgImgW = backgroundImageSR.sprite.rect.width;

        float bgImg_Scale_Ratio_Height = camHeight / bgImgH;
        float bgImg_Scale_Ratio_Width = camWidth/ bgImgW;

        background.transform.localScale = new Vector3(bgImg_Scale_Ratio_Width, bgImg_Scale_Ratio_Height + 0.01f, 1);


    }

}
