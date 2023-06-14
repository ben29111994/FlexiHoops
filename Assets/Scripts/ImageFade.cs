using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFade : MonoBehaviour
{

    Image image;
    bool isFade = false;

    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(delayFade());
    }

    private void OnEnable()
    {
        image = GetComponent<Image>();
        image.color = new Color32(255, 255, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFade)
        {
            Color co = image.color;
            co.a -= Time.deltaTime;
            image.color = co;
        }
    }

    IEnumerator delayFade()
    {
        yield return new WaitForSeconds(3);
        isFade = true;
    }
}
