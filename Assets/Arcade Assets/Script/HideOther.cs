using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideOther : MonoBehaviour {
    public GameObject canvas;
    public GameObject logo;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
            //if (GameManager.isStartGame == false)
            //{
            //    canvas.SetActive(true);
            //    logo.gameObject.SetActive(true);
            //}
            Time.timeScale = 1f;
        }
	}

    public void btnExitCanvas()
    {
        canvas.SetActive(true);
        logo.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void btnExitCharCanvas()
    {
        this.gameObject.SetActive(false);
        //if (GameManager.isStartGame == false)
        //{
        //    canvas.SetActive(true);
        //    logo.gameObject.SetActive(true);
        //}
        Time.timeScale = 1f;
    }
}
