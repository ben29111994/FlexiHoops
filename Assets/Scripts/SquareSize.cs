using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SquareSize : MonoBehaviour {
    public float sizeX;
    public float sizeY;
    public float speed;
    public GameObject player;
    public GameObject deathParticle;
    public Material[] materialList;
    //0:moving=1, size=0; 1:moving=0, size=1; 2:moving=1, size=1
    public int squareType = 1;
    float currentStage;
    public GameObject mesh;
    public GameObject invincibleItem;

    // Use this for initialization
    void Start()
    {
        squareType = 1;
        currentStage = PlayerPrefs.GetFloat("currentStage");
        if (currentStage == 0)
        {
            currentStage = 1;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        int currentTheme = ChangeTheme.randomTheme;
        switch(currentTheme)
        {
            case 0:
                mesh.GetComponent<Renderer>().material = materialList[0];
                break;
            case 1:
                mesh.GetComponent<Renderer>().material = materialList[1];
                break;
            case 2:
                mesh.GetComponent<Renderer>().material = materialList[2];
                break;
            case 3:
                mesh.GetComponent<Renderer>().material = materialList[3];
                break;
            case 4:
                mesh.GetComponent<Renderer>().material = materialList[4];
                break;
            case 5:
                mesh.GetComponent<Renderer>().material = materialList[5];
                break;
            case 6:
                mesh.GetComponent<Renderer>().material = materialList[6];
                break;
            case 7:
                mesh.GetComponent<Renderer>().material = materialList[7];
                break;
            case 8:
                mesh.GetComponent<Renderer>().material = materialList[8];
                break;
            case 9:
                mesh.GetComponent<Renderer>().material = materialList[9];
                break;
            default:
                mesh.GetComponent<Renderer>().material = materialList[0];
                break;
        }

        //float controlScore = Control.score;
        //if (currentStage >= 5)
        //{
        //    squareType = Random.Range(0, 10);
        //}
        //if(SpawnSquare.isDouble)
        //{
        //    squareType = 5;
        //}
        //if (squareType < 8)
        //{
        //float size;
        //size = Random.Range(0.25f, 1);
        //transform.localScale = new Vector3(size, 1, size);

        //if (transform.localScale.x <= 0.25f)
        //{
        //sizeX = Random.Range(0.4f, 1.2f);
        //var minSpeed = 1.5f - (0.02f * currentStage);
        //if(minSpeed <= 0.5f)
        //{
        //    minSpeed = 0.5f;
        //}
        //speed = Random.Range(minSpeed, 1.8f);
        //}
        //else if (transform.localScale.x > 0.25f)
        //{
        sizeX = Random.Range(0.1f, 0.15f);
        //    //var minSpeed = 1.5f - (0.02f * currentStage);
        //    //if (minSpeed <= 0.5f)
        //    //{
        //    //    minSpeed = 0.5f;
        //    //}
        //    //speed = Random.Range(minSpeed, 1.8f);
        //}
        speed = Random.Range(0.5f, 1.5f);
            transform.DOScale(new Vector3(sizeX, 1, sizeX), speed).SetLoops(-1, LoopType.Yoyo);
            this.transform.GetChild(1).gameObject.SetActive(false);

            //var randomInvincibleItem = Random.RandomRange(0, 10);
            //if (randomInvincibleItem == 0)
            //{
            //    var item = Instantiate(invincibleItem);
            //    item.transform.parent = transform;
            //    item.transform.localPosition = new Vector3(Random.Range(0.5f, 1), Random.Range(2, 3), 0);
            //    item.transform.parent = null;
            //    var distanceItem = Random.Range(1 + ((currentStage - 1) / 100), 1 + (currentStage / 100));
            //    var movementFrequencyItem = Random.Range(1 + ((currentStage - 1) / 100), 1 + (currentStage / 100));
            //    item.GetComponent<TrignometricMovement>().Distance = new Vector3(distanceItem, 0, 0);
            //    item.GetComponent<TrignometricMovement>().MovementFrequency = new Vector3(movementFrequencyItem, 0, 0);
            //}
        //}
        //else if (squareType >= 8)
        //{

        //    var distance = Random.Range(1 + ((currentStage - 1) / 100), 1 + (currentStage / 100));
        //    if(distance > 3f)
        //    {
        //        distance = 3f;
        //    }
        //    var movementFrequency = Random.Range(1 + ((currentStage - 1) / 100), 1 + (currentStage / 100));
        //    if (movementFrequency > 3f)
        //    {
        //        movementFrequency = 3;
        //    }
        //    GetComponent<TrignometricMovement>().Distance = new Vector3(distance, 0, 0);
        //    GetComponent<TrignometricMovement>().MovementFrequency = new Vector3(movementFrequency, 0, 0);
        //    //var speed = Mathf.Abs(distance - movementFrequency);
        //    sizeX = Random.Range(0.5f, 0.8f);
        //    if (movementFrequency > 2 && distance > 3)
        //    {
        //        sizeX = Random.Range(0.9f, 1.2f);
        //    }
        //    transform.localScale = new Vector3(sizeX, 1, sizeX);

        //    var randomInvincibleItem = Random.RandomRange(0, 10);
        //    if (randomInvincibleItem <= 3)
        //    {
        //        var item = Instantiate(invincibleItem);
        //        item.transform.parent = transform;
        //        item.transform.localPosition = new Vector3(Random.Range(0.5f,1), Random.Range(1, 3), 0);
        //        item.transform.parent = null;
        //        var distanceItem = Random.Range(1 + ((currentStage - 1) / 100), 1 + (currentStage / 100));
        //        var movementFrequencyItem = Random.Range(1 + ((currentStage - 1) / 100), 1 + (currentStage / 100));
        //        item.GetComponent<TrignometricMovement>().Distance = new Vector3(distanceItem, 0, 0);
        //        item.GetComponent<TrignometricMovement>().MovementFrequency = new Vector3(movementFrequencyItem, 0, 0);
        //    }
        //}
    }
    // Update is called once per frame
    void Update()
    {
        if (Control.isDead && !Control.isDisablePlayer || Control.isLoseRace)
        {
            Debug.Log("Dead");
            Control.isDisablePlayer = true;
            if (!Control.isLoseRace)
            {
                GameObject deathParticleInstall = Instantiate(deathParticle, player.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
                Rigidbody rigidItem = player.GetComponent<Rigidbody>();
                rigidItem.isKinematic = false;
                rigidItem.AddForce(new Vector2(250, 250));
                player.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Player")
        //{
        //    var meshPos = transform.GetChild(0).transform.position;
        //    GameObject deathParticleInstall = Instantiate(deathParticle, meshPos, Quaternion.Euler(0, 0, 0)) as GameObject;
        //    Destroy(this.gameObject);
        //}
    }
}
