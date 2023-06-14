using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Revive : MonoBehaviour {

	Text text;
    bool isFade = false;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
        StartCoroutine(delayFade());
    }

    private void OnEnable()
    {
        text = GetComponent<Text>();
        text.color = new Color32(255, 255, 255, 255);
        //text.CrossFadeAlpha(255, 2, false);
    }

    // Update is called once per frame
    void Update () {
        //Time.timeScale = 0.5f;
        if (isFade)
        {
            Color co = text.color;
            co.a -= Time.deltaTime;
            text.color = co;
        }
    }

    IEnumerator delayFade()
    {
        yield return new WaitForSeconds(3);
        isFade = true;
        if (this.tag == "UnlockText")
        {
            yield return new WaitForSeconds(2);
            this.gameObject.SetActive(false);
        }
    }
}
