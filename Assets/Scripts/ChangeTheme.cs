using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GameAnalyticsSDK;
using UnityEngine.UI;

public class ChangeTheme : MonoBehaviour {
    public static ChangeTheme instance;
    Color color;
    public static int randomTheme;
    //public int theme;
    public Text themeInput;

    // Use this for initialization
    void Awake () {
        //shapeList = GameObject.FindGameObjectsWithTag("Square");
        instance = this;
        ChangeBackground();
        //GameAnalytics.Initialize();
	}

    public void ChangeBackground()
    {
        randomTheme = Random.Range(0, 8);/* PlayerPrefs.GetInt("currentTheme");*/
        switch (randomTheme)
        {
            case 0:
                ColorUtility.TryParseHtmlString("#8a8a8a", out color);
                Camera.main.backgroundColor = color;

                break;
            case 1:
                ColorUtility.TryParseHtmlString("#38A872", out color);
                Camera.main.backgroundColor = color;
                break;
            case 2:
                ColorUtility.TryParseHtmlString("#172441", out color);
                Camera.main.backgroundColor = color;
                break;
            case 3:
                ColorUtility.TryParseHtmlString("#000000", out color);
                Camera.main.backgroundColor = color;
                break;
            case 4:
                ColorUtility.TryParseHtmlString("#7eb49c", out color);
                Camera.main.backgroundColor = color;
                break;
            case 5:
                ColorUtility.TryParseHtmlString("#006b9d", out color);
                Camera.main.backgroundColor = color;
                break;
            case 6:
                ColorUtility.TryParseHtmlString("#172441", out color);
                Camera.main.backgroundColor = color;
                break;
            case 7:
                ColorUtility.TryParseHtmlString("#5a387b", out color);
                Camera.main.backgroundColor = color;
                break;
            //case 8:
            //    ColorUtility.TryParseHtmlString("#5a387b", out color);
            //    Camera.main.backgroundColor = color;
            //    break;
            //case 9:
            //    ColorUtility.TryParseHtmlString("#ef9b60", out color);
            //    Camera.main.backgroundColor = color;
            //    break;
            //case 10:
            //    ColorUtility.TryParseHtmlString("#a3bb00", out color);
            //    Camera.main.backgroundColor = color;
            //    break;
            //case 11:
            //    ColorUtility.TryParseHtmlString("#008993", out color);
            //    Camera.main.backgroundColor = color;
            //    break;
            //case 12:
            //    ColorUtility.TryParseHtmlString("#535456", out color);
            //    Camera.main.backgroundColor = color;
            //    break;
            //case 13:
            //    ColorUtility.TryParseHtmlString("#9196AB", out color);
            //    Camera.main.backgroundColor = color;
            //    break;
            //case 14:
            //    ColorUtility.TryParseHtmlString("#FF8D8D", out color);
            //    Camera.main.backgroundColor = color;
            //    break;
            default:
                ColorUtility.TryParseHtmlString("#8a8a8a", out color);
                Camera.main.backgroundColor = color;
                break;
        }
    }
}
