using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;


public class ButtonManager : MonoBehaviour
{
    [Header("UI")]

    //public Text bestScoreText;
    public GameObject buttonMute;
    public Sprite muteSprite;
    public Sprite playSprite;
	public GameObject logo;
    GameObject player;
    public GameObject[] objectsInGame;
    public static ButtonManager instance;

    private void Awake()
	{
		Time.timeScale = 1f;
	}

    void Start()
    {
        instance = this;
        Time.timeScale = 1f;
        //bestScoreText.text = PlayerPrefs.GetFloat("BestScore").ToString();
        //Reset all pref
        //PlayerPrefs.DeleteAll();
    }

	//private void Update()
	//{
 //       if(SceneManager.GetActiveScene().buildIndex != 0 && Input.GetKeyDown(KeyCode.Escape))
 //       {
 //           var currentScene = SceneManager.GetActiveScene().buildIndex;
 //           SceneManager.LoadScene(0);
 //       }
 //   }

    public void ButtonMute()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.buttonClick);
        if (AudioListener.pause == false)
        {
            AudioListener.pause = true;
            buttonMute.GetComponentInChildren<Image>().sprite = muteSprite;
        }
        else
        {
            AudioListener.pause = false;
            buttonMute.GetComponentInChildren<Image>().sprite = playSprite;
        }
    }

    public void ButtonMoreGames()
	{
        SoundManager.instance.PlaySound(SoundManager.instance.buttonClick);
        Application.OpenURL("https://play.google.com/store/apps/dev?id=5581886918361803159");
	}

	public void btnExitGame()
	{
        SoundManager.instance.PlaySound(SoundManager.instance.buttonClick);
        Application.Quit();
	}

	public void ButtonRate()
	{
        SoundManager.instance.PlaySound(SoundManager.instance.buttonClick);
#if UNITY_ANDROID
        Application.OpenURL(AppInfo.Instance.PLAYSTORE_LINK);
		#elif UNITY_IOS
		Application.OpenURL(AppInfo.Instance.APPSTORE_LINK);
		#endif
	}
	public void ButtonPlay()
	{
        SoundManager.instance.PlaySound(SoundManager.instance.buttonClick);
        foreach (var item in objectsInGame)
        {
            item.SetActive(true);
        }
		logo.gameObject.SetActive(false);
	}

	public void ButtonHome()
	{
        SoundManager.instance.PlaySound(SoundManager.instance.buttonClick);
        Time.timeScale = 1f;
        foreach(var item in objectsInGame)
        {
            item.SetActive(false);
        }
		SceneManager.LoadScene(0);
	}

	public void ButtonReplay()
	{
        SoundManager.instance.PlaySound(SoundManager.instance.buttonClick);
        SceneManager.LoadScene(0);
		Time.timeScale = 1f;
        //if (PlayerPrefs.GetInt("AdsFree") == 1)
        //    UnityAdsManager.Instance.timeBetweenAds = 600;
	}

	public void ButtonFacebook(){
        SoundManager.instance.PlaySound(SoundManager.instance.buttonClick);
        Application.OpenURL (AppInfo.Instance.FACEBOOK_LINK);
	}

    //public void btnremoveAds()
    //{
    //    SoundManager.instance.PlaySound(SoundManager.instance.buttonClick);
    //    UnityAdsManager.Instance.ShowRewardVideo();
    //    PlayerPrefs.SetInt("AdsFree", 1);
    //}
}
