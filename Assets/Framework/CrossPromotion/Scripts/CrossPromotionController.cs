using SRS.CrossPromotion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossPromotionController : MonoSingleton<CrossPromotionController>
{
    #region Const parameters
    #endregion

    #region Editor paramters
    [Header("Object references")]
    [SerializeField]
    private GameObject popupUI;
    [SerializeField]
    private GameObject moreGamesUI;
    [SerializeField]
    private Button btnMoreGames;

    [SerializeField]
    private MoreAppsHandler moreAppsHandler;
    [SerializeField]
    private MoreAppsScrollController moreAppsScrollController;
    [SerializeField]
    private MoreAppsPopupController moreAppsPopupController;
    #endregion

    #region Normal paramters
    #endregion

    #region Encapsulate
    #endregion

    public void ShowPopupUI()
    {
        popupUI.SetActive(true);
        btnMoreGames.gameObject.SetActive(false);
    }

    public void HidePopupUI()
    {
        popupUI.SetActive(false);

        if (moreAppsHandler.IsComplete)
            btnMoreGames.gameObject.SetActive(true);
    }

    public void ShowMoreGamesUI()
    {
        moreGamesUI.SetActive(true);
    }

    public void HideMoreGamesUI()
    {
        moreGamesUI.SetActive(false);
    }

    public IEnumerator C_CheckPopupUI()
    {
        float curTime = Time.realtimeSinceStartup;

        while(Time.realtimeSinceStartup - curTime < 1.0f)
        {
            yield return null;
            if (moreAppsPopupController.HasItem)
                ShowPopupUI();
        }
    }

    public void CheckPopupUI()
    {
        StartCoroutine(C_CheckPopupUI());
    }

#if UNITY_EDITOR
    public void Awake()
    {
        StartCoroutine(C_CheckPopupUI());
    } 
#endif

    public void Active()
    {
        btnMoreGames.gameObject.SetActive(true);
    }

    public void Deactive()
    {
        btnMoreGames.gameObject.SetActive(false);
    }

}
