using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGlow : MonoBehaviour {
    GameObject starName;
    Color starColor;
    Color updateColor;
    // Use this for initialization
    void Start () {
        if(this.tag == "Galaxy")
        {
            starName = this.transform.gameObject;
        }
        else
        starName = this.transform.GetChild(0).gameObject;
        starColor = starName.GetComponent<SpriteRenderer>().color;
    }
	
	// Update is called once per frame
	void Update () {
        starName.GetComponent<SpriteRenderer>().color = Color.Lerp(new Color(starColor.a, starColor.b, starColor.g, 0), new Color(starColor.a, starColor.b, starColor.g, 1), Mathf.PingPong(Time.time, 2) / 2);
    }
}
