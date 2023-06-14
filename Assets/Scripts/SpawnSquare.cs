using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SpawnSquare : MonoBehaviour
{
    public GameObject[] squares;
    public float distanceSpawn = 5;
    public float spawnPosition = 5;
    static bool isEnable;

    Color squareColor;
    private Vector3 lastPosition = new Vector3(0, 0, 0);
    GameObject tempSquare;
    public GameObject player;
    bool isDestroy = false;
    public static SpawnSquare spawnSquareInstance;
    //public EZObjectPool objectPool;
    public float currentStage;
    public static bool isDouble = false;

    // Use this for initialization
    void Start()
    {
        currentStage = PlayerPrefs.GetFloat("currentStage");
        if (currentStage == 0)
        {
            currentStage = 1;
        }
        GetRandomSquares(spawnPosition);
    }
    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            //float controlScore = Control.score;
            if (player.transform.position.y - lastPosition.y >= distanceSpawn)
            {
                GetRandomSquares(spawnPosition);
            }
        }
    }

    void GetRandomSquares(float spawnPosition)
    {
        var randomChangeValue = Random.Range(0, 10);
        var percentHardSquare = 7 - currentStage * 0.02f;
        if(percentHardSquare >= 6)
        {
            percentHardSquare = 6;
        }
        if (randomChangeValue > percentHardSquare)
        {
            spawnPosition = 3.75f;
        }
        else
        {
            spawnPosition = 5f - (currentStage * 0.01f);
            if(spawnPosition <= 4)
            {
                spawnPosition = 4;
            }
        }
        //float controlScore = Control.score;
        var randomSquare = Random.Range(0, 4);
        //if (currentStage <= 5)
        //{
        //    randomSquare = 0;
        //}
        //if (currentStage >= 6)
        //{
        //    randomSquare = Random.Range(0, 2);
        //}
        //if (currentStage >= 11)
        //{
        //    randomSquare = Random.Range(0, 3);
        //}
        //if (currentStage >= 16)
        //{
        //    randomSquare = Random.Range(0, 4);
        //}
        //if (currentStage < 16)
        //{
        //    isDouble = false;
        //}
        //Double Square
        //else
        //{
        //    var randomShowUp = Random.Range(0, 10);
        //    if (randomShowUp > (9 - (currentStage / 50)))
        //    {
        //        isDouble = true;
        //    }
        //    else
        //        isDouble = false;
        //}
        GameObject square;
        //objectPool.TryGetNextObject(new Vector3(0, lastPosition.y + spawnPosition, 0), Quaternion.Euler(new Vector3(0, 0, 0)), out square);
        square = Instantiate(squares[randomSquare], new Vector3(0, lastPosition.y + spawnPosition, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
        if (square.gameObject.tag == "ScoringZone")
        {
            float size;
            size = Random.Range(0.5f, 1.2f);
            square.transform.localScale = new Vector3(size, 1, size);
        }
        square.transform.SetParent(this.transform);
        lastPosition.y = square.transform.position.y;
        if(lastPosition.y + 10 >= Control.instance.HalfWayLine.transform.position.y)
        {
            this.enabled = false;
        }

        //if (isDouble)
        //{
        //    var randomSquare2 = Random.Range(0, 4);
        //    GameObject square2;
        //    square2 = Instantiate(squares[randomSquare2], square.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        //    if (square2.gameObject.tag == "ScoringZone")
        //    {
        //        float size;
        //        size = Random.Range(0.25f, 1);
        //        square.transform.localScale = new Vector3(size, 1, size);
        //    }
        //    square2.transform.SetParent(this.transform);
        //}
    }
}
