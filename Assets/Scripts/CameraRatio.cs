using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraRatio : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Application.targetFrameRate = 60;
        // set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)
        float targetaspect = 9f / 16f;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;
        //Debug.LogError(windowaspect);

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // obtain camera component so we can modify its viewport
        Camera camera = GetComponent<Camera>();
        //10:16
        if (windowaspect > 0.62f && windowaspect < 0.63f)
        {
            camera.orthographicSize += 0.1f;
            //Camera.main.GetComponent
        }
        //3:4
        else if (windowaspect > 0.74f && windowaspect < 0.76f)
        {
            camera.orthographicSize += 0.32f;
            //Camera.main.GetComponent
        }
        //18:9
        else if (windowaspect > 0.49f && windowaspect < 0.51f)
        {
            camera.orthographicSize += 0.15f;
        }
        //2:3
        else if (windowaspect > 0.65f && windowaspect < 0.67f)
        {
            camera.orthographicSize += 0.2f;
        }
        //9:19
        else if (windowaspect > 0.46f && windowaspect < 0.48f)
        {
            camera.orthographicSize += 0.2f;
        }
        //9:20
        else if (windowaspect > 0.449f && windowaspect < 0.451f)
        {
            camera.orthographicSize += 0.3f;
        }
    }
}
