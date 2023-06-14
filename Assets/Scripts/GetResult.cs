using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetResult : MonoBehaviour {
	public GameObject scoreText;
	public GameObject bestScoreText;
	// Use this for initialization
	void Start () {
		scoreText.GetComponent<Text>().text = PlayerPrefs.GetFloat("current").ToString();
		bestScoreText.GetComponent<Text>().text = PlayerPrefs.GetFloat("best").ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
