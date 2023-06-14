using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

public class UnityAdsManager: MonoSingleton<UnityAdsManager>
{
	//public const string UAds_SYMBOL = "UNITY_ADS";

	//rewardedVideo
#if UNITY_ADS
	private string GooglePlayGameID = "1595694";
	private string AppStoreGameID = "1595695";
	private string gameID;

	private float timeLastAdsShow = 0;
	public float timeBetweenAds = 120;
	private bool firstTime = true;
	public bool skipLevel = false;

	// Use this for initialization
	void Awake()
	{
#if UNITY_ANDROID
		gameID = GooglePlayGameID;
#elif UNITY_IOS
		gameID = AppStoreGameID;
#endif
		Advertisement.Initialize(gameID);
	}

	private bool CanShowAds()
	{
		if (firstTime)
		{
			firstTime = false;
			timeLastAdsShow = Time.realtimeSinceStartup;

			return true;
		}
		else
		{
			float currentTime = Time.realtimeSinceStartup;

			if (currentTime - timeLastAdsShow >= timeBetweenAds)
			{
				timeLastAdsShow = currentTime;
				return true;
			}
			else
			{
				return false;
			}
		}
	}

	//[System.Diagnostics.Conditional(UAds_SYMBOL)]
	public void ShowAds(string placementID = "")
	{
		StartCoroutine(C_WaitForAd());

		if (string.Equals(placementID, ""))
			placementID = null;

		ShowOptions options = new ShowOptions();
		options.resultCallback = AdCallbackhandler;

		if (Advertisement.IsReady(placementID) && CanShowAds())
			Advertisement.Show(placementID, options);
	}

	public void ShowRewardVideo(string placementID = "rewardedVideo")
    {
        StartCoroutine(C_WaitForAd());

        if (string.Equals(placementID, ""))
            placementID = null;
        ShowOptions options = new ShowOptions();
		options.resultCallback = RewardCallback;

        if (Advertisement.IsReady(placementID))
            Advertisement.Show(placementID, options);
    }

	void RewardCallback(ShowResult reward)
	{
		switch (reward)
		{
			case ShowResult.Finished:
				//Debug.Log("Ad Finished. Rewarding player...");
				//FindObjectOfType<ButtonManager>().levelCompleteCanvas.SetActive(true);
    //            int activeScene = SceneManager.GetActiveScene().buildIndex;
    //            if (activeScene + 1 < SceneManager.sceneCountInBuildSettings)
    //            {
    //                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
    //            }
                break;
			case ShowResult.Skipped:
				Debug.Log("Ad skipped. Son, I am dissapointed in you");
				break;
			case ShowResult.Failed:
				Debug.Log("I swear this has never happened to me before");
				break;
		}
	}

	void AdCallbackhandler(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("Ad Finished. Rewarding player...");
                break;
            case ShowResult.Skipped:
                Debug.Log("Ad skipped. Son, I am dissapointed in you");
                break;
            case ShowResult.Failed:
                Debug.Log("I swear this has never happened to me before");
                break;
        }
        Time.timeScale = 1f;
	}

	IEnumerator C_WaitForAd()
	{
		float currentTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        yield return null;

		while (Advertisement.isShowing)
			yield return null;

		Time.timeScale = currentTimeScale;
    }
	
#endif
}