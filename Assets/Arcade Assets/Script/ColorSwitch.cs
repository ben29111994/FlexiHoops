using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ColorSwitch : MonoBehaviour {
    bool isSwitched = false;
    Image img;
	// Use this for initialization
	void Start () {
        img = this.GetComponent<Image>();
        switchColor();

    }
	
	// Update is called once per frame
	void Update () {
		//if(!isSwitched)
  //      {
  //          isSwitched = true;
  //          switchColor();
  //      }
    }

    IEnumerator changeColor()
    {
        yield return new WaitForSeconds(1f);
        img.color = new Color32(255, 255, 255, 150);
        yield return new WaitForSeconds(1f);
        img.color = new Color32(255, 255, 255, 255);
        isSwitched = false;
    }

    void switchColor()
    {
        //StartCoroutine(changeColor());
        img.DOFade(0.3f, 1f).SetLoops(-1, LoopType.Yoyo);
    }
}
