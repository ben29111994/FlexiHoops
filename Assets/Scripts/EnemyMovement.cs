using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour {
    public GameObject effect;
    Rigidbody rigid;
    public float gravityScale = 1.0f;
    static float customTime = 0.5f;
    static float squareScale = 1;
    public GameObject currentSquare;
    bool isDetectSquare = false;
    bool isDetectSquareBehind = false;
    bool isDie = false;
    AudioSource soundEffect;

    // Global Gravity doesn't appear in the inspector. Modify it here in the code
    // (or via scripting) to define a different default gravity for all objects.

    public static float globalGravity = -9.81f;

    // Use this for initialization
    void Start()
    {
        soundEffect = GetComponent<AudioSource>();
        var currentStage = PlayerPrefs.GetFloat("currentStage");
        if (currentStage == 0)
        {
            currentStage = 1;
        }
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rigid.AddForce(gravity, ForceMode.Acceleration);
    }

    private void Update()
    {
        if (Control.isStarted)
        {
            rigid.isKinematic = false;
            if (isDetectSquare && !isDie)
            {
                squareScale = currentSquare.transform.localScale.x;
                if (squareScale > 0.5f)
                {
                    EnemyJump(500);
                    Debug.Log("Jump!");
                    isDetectSquare = false;
                }
                else
                {
                    isDetectSquare = false;
                    Debug.Log("Hold!");
                }
            }

            if (isDetectSquareBehind && !isDie)
            {
                EnemyJump(300);
            }
        }
        else
        {
            rigid.useGravity = false;
            rigid.isKinematic = true;
        }
    }

    void EnemyJump(float force)
    {
        rigid.velocity = Vector2.zero;
        rigid.AddForce(Vector2.up * force);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EnemyJump(500);
            SoundManager.instance.PlaySound(SoundManager.instance.kick);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Square")
        {
            if (this.name != "Assasin")
            {
                isDie = true;
                GameObject itemParticleInstall = Instantiate(effect, new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(0, 0, 0)) as GameObject;
                //SoundManager.instance.PlaySound(SoundManager.instance.dead);
                Rigidbody rigidItem = this.GetComponent<Rigidbody>();
                rigidItem.isKinematic = false;
                rigidItem.AddForce(new Vector2(250, 250));
                this.GetComponent<BoxCollider>().enabled = false;
                Destroy(this.gameObject);
            }
        }

        if(other.tag == "AIDetect")
        {
            isDetectSquare = true;
            currentSquare = other.transform.parent.gameObject;
        }

        if (other.tag == "AIDetectBehind")
        {
            isDetectSquareBehind = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "AIDetect")
        {
            isDetectSquare = false;
        }

        if (other.tag == "AIDetectBehind")
        {
            isDetectSquareBehind = false;
        }
    }

    //IEnumerator delayDestroy()
    //{
    //    yield return new WaitForSeconds(10);
    //    Destroy(this.gameObject);
    //}
}
