using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetCurentChar : MonoBehaviour {
    public GameObject assetName;
    public GameObject themeAssetName;
    public string charName;
    public string themeName;
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        assetName = GameObject.FindGameObjectWithTag("SkinLoad");
        if (assetName != null)
        {
            charName = assetName.GetComponent<Text>().text.ToString();
            if (charName != "Locked")
            {
                PlayerPrefs.SetString("CurrentChar", charName);
            }
        }
        themeAssetName = GameObject.FindGameObjectWithTag("ThemeLoad");
        if (themeAssetName != null)
        {
            themeName = themeAssetName.GetComponent<Text>().text.ToString();
            PlayerPrefs.SetString("CurrentTheme", themeName);
        }
    }
}
