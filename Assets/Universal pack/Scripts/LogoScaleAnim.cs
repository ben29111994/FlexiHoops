using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LogoScaleAnim : MonoBehaviour {
	
	Text ima;

	void Start () {
		ima = GetComponent<Text> ();
	}
	


	void Update () {
		anlpha ();
	}


	void anlpha(){
		var co = ima.color;
		co.a += Time.deltaTime * 0.5f ;
		ima.color = co;
	}


}
