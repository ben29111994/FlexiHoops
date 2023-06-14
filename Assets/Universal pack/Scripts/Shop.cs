using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {
    public GameObject[] buttonBuy;
    public GameObject[] buttonUse;

    public float distance;
    public float distance0;

	public float[] distanceObject;
    private float[] target;

    public float positionTarget1;




    float speed = 800;
    Vector3 maxScale;
    Vector3 minScale;
    bool isMoving;
    RectTransform myRect;
    public GameObject[] skinTarget;

    // Use this for initialization
    void Start()
    {
        myRect = GetComponent<RectTransform>();
        target = new float[5];
		distanceObject = new float[4];
        target[0] = positionTarget1;
        target[1] = target[0] - distance;
        target[2] = target[1] - distance;
        target[3] = target[2] - distance;
        target[4] = target[3] - distance;

		distanceObject [0] = distance0;
		distanceObject[1] = distanceObject[0] - distance;
		distanceObject[2] = distanceObject[1] - distance;
		distanceObject[3] = distanceObject[2] - distance;

		Debug.Log (distanceObject [2]);
        maxScale = new Vector3(1, 1, 1);
        minScale = new Vector3(.75f, .75f, .75f);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            isMoving = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMoving = true;
        }


        if (isMoving)
        {

			if (myRect.anchoredPosition.x > distanceObject[0])
            {
                myRect.anchoredPosition = Vector2.MoveTowards(myRect.anchoredPosition, new Vector2(target[0], myRect.anchoredPosition.y), speed * Time.deltaTime); ;
            }
			else if (myRect.anchoredPosition.x < distanceObject[0] && myRect.anchoredPosition.x > distanceObject[1])
            {
                myRect.anchoredPosition = Vector2.MoveTowards(myRect.anchoredPosition, new Vector2(target[1], myRect.anchoredPosition.y), speed * Time.deltaTime); ;
            }
			else if (myRect.anchoredPosition.x < distanceObject[1] && myRect.anchoredPosition.x > distanceObject[2])
            {
                myRect.anchoredPosition = Vector2.MoveTowards(myRect.anchoredPosition, new Vector2(target[2], myRect.anchoredPosition.y), speed * Time.deltaTime); ;
            }
			else if (myRect.anchoredPosition.x < distanceObject[2] && myRect.anchoredPosition.x > distanceObject[3])
            {
                myRect.anchoredPosition = Vector2.MoveTowards(myRect.anchoredPosition, new Vector2(target[3], myRect.anchoredPosition.y), speed * Time.deltaTime); ;
            }
			else if (myRect.anchoredPosition.x < distanceObject[3])
            {
                myRect.anchoredPosition = Vector2.MoveTowards(myRect.anchoredPosition, new Vector2(target[4], myRect.anchoredPosition.y), speed * Time.deltaTime); ;
            }

		}

		for (int i = 0; i < skinTarget.Length; i++) {
			if (myRect.anchoredPosition.x > target [i] - 5f && myRect.anchoredPosition.x < target [i] + 5f) {
				skinTarget [i].transform.localScale = maxScale;
                if (buttonBuy[i] != null)
                {
                    buttonBuy[i].SetActive(true);
                    buttonUse[i].SetActive(false);
                }
                else
                {
                    buttonUse[i].SetActive(true);
                }
			} else {
				skinTarget [i].transform.localScale = minScale;
                if (buttonBuy[i] != null)
                {
                    buttonBuy[i].SetActive(false);
                    buttonUse[i].SetActive(false);
                }
                else
                {
                    buttonUse[i].SetActive(false);
                }
			}
		}
		Debug.Log (myRect.anchoredPosition.x);
    }
}
