using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float targetWidth = 1920f; // Referans geniþlik
    public float targetHeight = 1080f; // Referans yükseklik

    void Start()
    {
        AdjustCamera();
    }

    void AdjustCamera()
    {
        Camera mainCamera = Camera.main;

        float aspectRatio = (float)Screen.width / Screen.height;
        float targetAspect = targetWidth / targetHeight;

        if (aspectRatio >= targetAspect)
        {
            mainCamera.orthographicSize = targetHeight / 200f;
        }
        else
        {
            float differenceInSize = targetAspect / aspectRatio;
            mainCamera.orthographicSize = targetHeight / 200f * differenceInSize;
        }
    }
}
