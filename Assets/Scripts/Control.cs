using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using MoreMountains.NiceVibrations;
using UnityStandardAssets.ImageEffects;
//using GameAnalyticsSDK;

public class Control : MonoBehaviour {
    public static Control instance;
	public GameObject[] players;
    public Material[] materialList;
    //public static float score;
    public static float combo;
    public static bool isInvincible;
    public Text invincibleText;
    public GameObject invincibleEffect;
    //public GameObject scoreText;
    public GameObject squareParticle;
	public Rigidbody rigid;
	public float speed;
	public float jumpForce = 100;
	public static Control control;
	public static bool isDead = false;
    public static bool isLoseRace = false;
    public static bool isDisablePlayer = false;
    public static bool isStarted = false;
    public enum PlayState
	{
		idle,
		jump
	}
	public PlayState state;
	// Gravity Scale editable on the inspector
	// providing a gravity scale per object

	public float gravityScale = 1.0f;
    public Slider map;
    public GameObject WinPanel;
    public GameObject LosePanel;
    public GameObject StagePanel;
    string rule;
    public GameObject FinishLine;
    public GameObject HalfWayLine;
    public GameObject HalfWayText;
    public GameObject GoDownText;
    public GameObject GameManager;
    public GameObject Assasin;
    public Text Stage;
    public float currentStage;
    public static float RotateValue = 0;
    public GameObject replayButton;
    public Text perfectText;
    public BlurOptimized screenBlur;
    //public GameObject confetti;
    public Text level;
    public GameObject[] sliderParts;
    public bool isInverted;
    //public GameObject sign;
    public GameObject cloud1;
    public GameObject cloud2;
    public static float range;
    public string[] combo1;
    public string[] combo2;
    public string[] combo3;
    public string[] combo4;
    public AudioSource scoreSound;
    Coroutine lastRoutine = null;
    public Light lightCombo;

    // Global Gravity doesn't appear in the inspector. Modify it here in the code
    // (or via scripting) to define a different default gravity for all objects.

    public static float globalGravity = -9.81f;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        //PlayerPrefs.DeleteAll();
        //score = PlayerPrefs.GetFloat("BestScore");
        //scoreText.GetComponent<Text>().text = score.ToString();
        combo = 1;
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "game");
        isInvincible = false;
        isInverted = false;
        screenBlur = GameObject.FindObjectOfType<BlurOptimized>();
        RotateValue = 0;
        isLoseRace = false;
        isStarted = false;
        isDead = false;
        isDisablePlayer = false;
        StartCoroutine(delayShowStage());
		rigid.useGravity = false;
        map.maxValue = HalfWayLine.transform.position.y;
        range = map.maxValue;

        sliderParts[0].GetComponent<Image>().color = Camera.main.backgroundColor;
        sliderParts[1].GetComponent<Image>().color = Camera.main.backgroundColor;
        sliderParts[2].GetComponent<Image>().color = Camera.main.backgroundColor;
        sliderParts[3].GetComponent<Text>().text = currentStage.ToString();
        sliderParts[4].GetComponent<Text>().text = (currentStage + 1).ToString();

        //var currentTheme = ChangeTheme.randomTheme;
        //switch (currentTheme)
        //{
        //    case 0:
        //        GetComponentInChildren<Renderer>().material = materialList[0];
        //        break;
        //    case 1:
        //        GetComponentInChildren<Renderer>().material = materialList[1];
        //        break;
        //    case 2:
        //        GetComponentInChildren<Renderer>().material = materialList[2];
        //        break;
        //    case 3:
        //        GetComponentInChildren<Renderer>().material = materialList[3];
        //        break;
        //    case 4:
        //        GetComponentInChildren<Renderer>().material = materialList[4];
        //        break;
        //    case 5:
        //        GetComponentInChildren<Renderer>().material = materialList[5];
        //        break;
        //    case 6:
        //        GetComponentInChildren<Renderer>().material = materialList[6];
        //        break;
        //    case 7:
        //        GetComponentInChildren<Renderer>().material = materialList[7];
        //        break;
        //    case 8:
        //        GetComponentInChildren<Renderer>().material = materialList[8];
        //        break;
        //    case 9:
        //        GetComponentInChildren<Renderer>().material = materialList[9];
        //        break;
        //    default:
        //        GetComponentInChildren<Renderer>().material = materialList[1];
        //        break;
        //}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDead)
        {
            if (!isInverted)
            {
                map.value = transform.position.y / 2;
            }
            else
            {
                Debug.Log(range / 2);
                map.value = (range / 2) + ((range / 2) - transform.position.y/2);
            }
        }
        //if (transform.position.y >= map.maxValue / 1.6f)
        //{
        //    var spawnEnemy = GetComponent<SpawnSquare>();
        //    spawnEnemy.enabled = false;
        //}
        //if (isLoseRace)
        //{
        //    GameManager.SetActive(false);
        //}
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rigid.AddForce(gravity, ForceMode.Acceleration);

        if (isDead)
        {
            if (SceneManager.GetActiveScene().buildIndex != 1)
            {
                LosePanel.SetActive(true);
                int progress = (int)(map.value * 100 / map.maxValue);
                LosePanel.GetComponentInChildren<Text>().text = progress + "% COMPLETED";
                map.transform.parent = LosePanel.transform;
                StartCoroutine(delayReplayButton());
            }
            rigid.useGravity = false;
        }
    }

    private void Update()
    {
        CamFollow();
    }

    IEnumerator delayReplayButton()
    {
        yield return new WaitForSeconds(1);
        replayButton.SetActive(true);
    }

	public void Jump()
	{
        if (!isDead && !isDisablePlayer)
        {
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.up * jumpForce);

            //Play normal sound
            SoundManager.instance.PlaySound(SoundManager.instance.jump);
        }
    }

    void CamFollow()
    {
        if (!isDead && isStarted)
        {
            if (!isInverted)
            {
                if (transform.position.y >= Camera.main.transform.position.y)
                {
                    Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(0, transform.position.y + 1, -12), 2 * Time.deltaTime);
                    Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, Quaternion.Euler(0, 0, RotateValue), Time.deltaTime / 2);
                }
                var camSpeed = currentStage / 100;
                if (camSpeed >= 1.5f)
                {
                    camSpeed = 1.5f;
                }
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(0, Camera.main.transform.position.y + camSpeed + 0.5f, -12), 1 * Time.deltaTime);
                if (Mathf.Abs(cloud1.transform.position.y - Camera.main.transform.position.y) >= 3)
                {
                    //if (Mathf.Abs(Camera.main.transform.position.y - cloud1.transform.position.y) >= 6)
                    //{
                    //    cloud1.transform.position = Vector3.Lerp(cloud1.transform.position, new Vector3(0, Camera.main.transform.position.y - 6, -2), 3 * Time.deltaTime);
                    //}
                    //else
                        cloud1.transform.position = Vector3.Lerp(cloud1.transform.position, new Vector3(0, Camera.main.transform.position.y - 3, -7), 2 * Time.deltaTime);
                    //cloud1.transform.position = Vector3.Lerp(cloud1.transform.position, new Vector3(0, cloud1.transform.position.y + camSpeed + 0.5f, -2), 2 * Time.deltaTime);
                }
                cloud1.transform.position = Vector3.Lerp(cloud1.transform.position, new Vector3(0, cloud1.transform.position.y + camSpeed + 0.7f, -7), 1 * Time.deltaTime);
                if (Mathf.Abs(Camera.main.transform.position.y - transform.position.y) >= 11)
                {
                    MMVibrationManager.Vibrate();
                    Handheld.Vibrate();
                    GetComponent<BoxCollider>().enabled = false;
                    isDead = true;
                    //SoundManager.instance.PlaySound(SoundManager.instance.dead);
                }
            }
            else
            {
                if (transform.position.y <= Camera.main.transform.position.y)
                {
                    Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(0, transform.position.y - 1, -12), 2 * Time.deltaTime);
                    Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, Quaternion.Euler(0, 0, RotateValue), Time.deltaTime / 2);
                }
                var camSpeed = currentStage / 100;
                if (camSpeed >= 1.5f)
                {
                    camSpeed = 1.5f;
                }
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(0, Camera.main.transform.position.y - camSpeed - 0.5f, -12), 1 * Time.deltaTime);
                if (Mathf.Abs(Camera.main.transform.position.y - cloud2.transform.position.y) >= 3)
                {                  
                    //if (Mathf.Abs(Camera.main.transform.position.y - cloud2.transform.position.y) >= 6)
                    //{
                    //    cloud2.transform.position = Vector3.Lerp(cloud2.transform.position, new Vector3(0, Camera.main.transform.position.y + 6, -2), 3 * Time.deltaTime);
                    //}
                    //else
                        cloud2.transform.position = Vector3.Lerp(cloud2.transform.position, new Vector3(0, Camera.main.transform.position.y + 3, -7), 2 * Time.deltaTime);
                    //cloud2.transform.position = Vector3.Lerp(cloud2.transform.position, new Vector3(0, cloud2.transform.position.y - camSpeed - 0.5f, -2), 2 * Time.deltaTime);
                }
                cloud2.transform.position = Vector3.Lerp(cloud2.transform.position, new Vector3(0, cloud2.transform.position.y - camSpeed - 0.7f, -7), 1 * Time.deltaTime);
                //if (Mathf.Abs(Camera.main.transform.position.y + transform.position.y) >= 11)
                //{
                //    MMVibrationManager.Vibrate();
                //    Handheld.Vibrate();
                //    GetComponent<BoxCollider>().enabled = false;
                //    isDead = true;
                //    SoundManager.instance.PlaySound(SoundManager.instance.dead);
                //}
            }
        }
    }

    IEnumerator delayRotateCamera()
    {
        yield return new WaitForSeconds(3);
        RotateValue = Random.Range(0, 360);
        StartCoroutine(delayRotateCamera());
    }

    IEnumerator delayDestroy(GameObject tempSquare)
    {
        MMVibrationManager.Vibrate();
        if (!isInverted)
        {
            while (!isInverted)
            {
                yield return null;
            }
            //GameObject squareParticleInstall = Instantiate(squareParticle, tempSquare.transform.GetChild(1).gameObject.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            tempSquare.GetComponent<BoxCollider>().enabled = true;
        }
        //tempSquare.SetActive(false);
        //Destroy(tempSquare);
    }

    IEnumerator delayShowStage()
    {
        currentStage = PlayerPrefs.GetFloat("currentStage");
        if (currentStage == 0)
        {
            currentStage = 1;
        }
        var finishLineRange = 30 + currentStage;
        if(finishLineRange > 100)
        {
            finishLineRange = 100;
        }
        HalfWayLine.transform.position = new Vector3(0, finishLineRange, -5);
        //if ((currentStage - 3) % 10 == 0 || (currentStage - 7) % 10 == 0)
        //{
        //    rule = "\n" + "You have company on the way";
        //    GetComponent<SpawnSquare>().enabled = true;
        //}
        //if (currentStage % 10 == 0 || (currentStage - 5) % 10 == 0)
        //{
        //    rule = "\n" + "Assassin is available to track you down";
        //    Assasin.SetActive(true);
        //}
        //if ((currentStage - 6) % 10 == 0 || (currentStage - 7) % 10 == 0)
        //{
        //    rule = "\n" + "The road of life is not always smooth";
        //    StartCoroutine(delayRotateCamera());
        //}
        //if ((currentStage - 8) % 10 == 0 || (currentStage - 9) % 10 == 0)
        //{
        //    rule = "\n" + "Obstacles are movable";
        //}
        //if (currentStage % 10 == 0)
        //{
        //    rule = "\n" + "All challenges available";
        //    StartCoroutine(delayRotateCamera());
        //    Assasin.SetActive(true);
        //}
        StagePanel.SetActive(true);
        StagePanel.GetComponentInChildren<Text>().text = "LEVEL " + currentStage + rule;
        Stage.text = currentStage.ToString();
        yield return new WaitForSeconds(2);
        StagePanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Square" && !isDead && !isInvincible)
        {
            MMVibrationManager.Vibrate();
            Handheld.Vibrate();
            //Debug.Log("Hit");
            GetComponent<BoxCollider>().enabled = false;
            isDead = true;
            screenBlur.enabled = true;
            //SoundManager.instance.PlaySound(SoundManager.instance.dead);
        }
        if (other.tag == "ScoringZone")
        {
            if (!isDead)
            {
                //if(isInvincible)
                //{
                //    isInvincible = false;
                //    invincibleEffect.GetComponent<ParticleSystem>().Stop();
                //    invincibleText.DOColor(new Color32(255, 244, 35, 0), 0.5f);
                //}
                //combo = 0;
                //score++;
                //scoreText.GetComponent<Text>().text = score.ToString();
                perfectText.transform.localScale = new Vector3(1, 1, 1);

                //if (other.transform.localScale.x <= 0.3f)
                //{

                if (combo >= 2)
                {
                    if (combo >= 3)
                    {
                        if (combo >= 4)
                        {
                            combo++;
                            perfectText.color = new Color32(255, 255, 255, 255);
                            perfectText.text = combo4[Random.Range(0, combo4.Length)];
                            perfectText.DOKill();
                            perfectText.DOFade(0, 0.8f);
                            scoreSound.pitch = 1.3f;
                            scoreSound.Play();
                            //lightCombo.DOIntensity(2, 0.5f);
                        }
                        else
                        {
                            combo++;
                            perfectText.color = new Color32(253, 255, 0, 255);
                            perfectText.text = combo3[Random.Range(0, combo3.Length)];
                            perfectText.DOKill();
                            perfectText.DOFade(0, 0.8f);
                            scoreSound.pitch = 1.2f;
                            scoreSound.Play();
                            //lightCombo.DOIntensity(1, 0.5f);
                        }
                    }
                    else
                    {
                        combo++;
                        perfectText.color = new Color32(191, 62, 255, 255);
                        perfectText.text = combo2[Random.Range(0, combo2.Length)];
                        perfectText.DOKill();
                        perfectText.DOFade(0, 0.8f);
                        scoreSound.pitch = 1.1f;
                        scoreSound.Play();
                        //lightCombo.DOIntensity(0.5f, 0.5f);
                    }
                }
                else
                {
                    combo++;
                    perfectText.color = new Color32(74, 255, 99, 255);
                    perfectText.text = combo1[Random.Range(0, combo1.Length)];
                    perfectText.DOKill();
                    perfectText.DOFade(0, 0.8f);
                    scoreSound.pitch = 1f;
                    scoreSound.Play();
                    //lightCombo.DOIntensity(0.2f, 0.5f);
                }
                //}
                if (lastRoutine != null)
                {
                    StopCoroutine(lastRoutine);
                }
                lastRoutine = StartCoroutine(delayCombo());
                iTween.PunchScale(perfectText.gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.3f);
                other.GetComponent<BoxCollider>().enabled = false;
                //var perfectZone = other.transform.GetChild(1);
                //perfectZone.gameObject.SetActive(false);
                StartCoroutine(delayDestroy(other.gameObject));
            }
        }
        //if (other.tag == "Perfect")
        //{
        //    Debug.Log("Perfect");
        //    combo++;
        //    //score+=combo;
        //    //if(combo >= 3)
        //    //{
        //    //    isInvincible = true;
        //    //    rigid.velocity = Vector2.zero;
        //    //    transform.DOMoveY(transform.position.y + 15, 1.5f);
        //    //    SoundManager.instance.PlaySound(SoundManager.instance.jump);
        //    //    invincibleEffect.SetActive(true);
        //    //    invincibleEffect.GetComponent<ParticleSystem>().Play();
        //    //    invincibleText.DOColor(new Color32(255, 244, 35, 255), 0.5f);
        //    //    StartCoroutine(delayInvincible());
        //    //}
        //    //scoreText.GetComponent<Text>().text = score.ToString();
        //    perfectText.transform.localScale = new Vector3(1, 1, 1);
        //    perfectText.color = new Color32(213, 35, 255, 255);
        //    perfectText.text = "PERFECT!";
        //    if (combo >= 2)
        //    {
        //        perfectText.text = "PERFECT X " + combo.ToString();
        //    }
        //    iTween.PunchScale(perfectText.gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.3f);
        //    perfectText.DOColor(new Color32(213, 35, 255, 0), 1);
        //    other.gameObject.SetActive(false);
        //    var greatZone = other.transform.parent;
        //    greatZone.GetComponent<BoxCollider>().enabled = false;
        //    StartCoroutine(delayDestroy(greatZone.gameObject, other.gameObject));
        //}
        if (other.tag == "FinishLine")
        {
            if (!isDead)
            {
                if (isInverted)
                {
                    //confetti.SetActive(true);
                    //PlayerPrefs.SetFloat("BestScore", score);
                    GetComponent<BoxCollider>().enabled = false;
                    GameManager.SetActive(false);
                    rigid.useGravity = false;
                    rigid.isKinematic = true;
                    var currentStage = PlayerPrefs.GetFloat("currentStage");
                    if (currentStage == 0)
                    {
                        currentStage = 1;
                    }
                    currentStage++;
                    Debug.Log(currentStage);
                    PlayerPrefs.SetFloat("currentStage", currentStage);
                    Control.isDisablePlayer = true;
                    StartCoroutine(delayRestart());
                }
                else
                {
                    isInverted = true;
                    cloud1.transform.DOMoveY(-50, 2);
                    cloud2.transform.localPosition = new Vector3(0, HalfWayLine.transform.position.y + 8, -2);
                    //cloud.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    //cloud2.transform.localScale = new Vector3(15, -15, 15);
                    //sign.transform.localScale = new Vector3(-1, 1, 1);
                    //sign.transform.DOScale(new Vector3(-1f, 1f, 1f), 2);
                    HalfWayLine.GetComponent<BoxCollider>().enabled = false;
                    //HalfWayLine.transform.DOScale(new Vector3(0, 0, 0), 0.5f);
                    HalfWayText.transform.DOScale(new Vector3(0, 0, 0), 0.5f);
                    GoDownText.SetActive(true);
                    GoDownText.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
                    FinishLine.SetActive(true);
                }
            }
        }
        if(other.tag == "Invincible")
        {
            other.gameObject.SetActive(false);
            isInvincible = true;
            //transform.DOMove(new Vector3(0, transform.position.y + 12.5f, 0), 1.5f);
            rigid.velocity = Vector2.zero;
            if(!isInverted)
            rigid.AddForce(Vector2.up * (jumpForce*3));
            SoundManager.instance.PlaySound(SoundManager.instance.jump);
            invincibleEffect.SetActive(true);
            invincibleEffect.GetComponent<ParticleSystem>().Play();
            invincibleText.DOColor(new Color32(255, 244, 35, 255), 0.5f);
            StartCoroutine(delayInvincible());
        }
    }

    IEnumerator delayCombo()
    {
        yield return new WaitForSeconds(1.5f);
        combo = 1;
    }

    IEnumerator delayRestart()
    {
        screenBlur.enabled = true;
        WinPanel.SetActive(true);
        yield return new WaitForSeconds(3);
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        //FacebookAnalytics.Instance.LogGame_endEvent(1);
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "game", (int)currentStage);
        SceneManager.LoadScene(currentScene);
    }

    IEnumerator delayInvincible()
    {
        isDisablePlayer = true;
        yield return new WaitForSeconds(1);
        isDisablePlayer = false;
        yield return new WaitForSeconds(4);
        isInvincible = false;
        invincibleEffect.GetComponent<ParticleSystem>().Stop();
        invincibleText.DOColor(new Color32(255, 244, 35, 0), 0.5f);
    }

    public void btnStartGame()
	{
        //FacebookAnalytics.Instance.LogGame_startEvent(1);
        if (level.text != "")
        {
            var setLevel = float.Parse(level.text.ToString());
            PlayerPrefs.SetFloat("currentStage", setLevel);
            SceneManager.LoadScene(0);
        }
        var inputField = level.transform.parent;
        inputField.gameObject.SetActive(false);

        var themeInput = ChangeTheme.instance.themeInput;
        if (themeInput.text != "")
        {
            var setTheme = int.Parse(themeInput.text.ToString());
            PlayerPrefs.SetInt("currentTheme", setTheme);
            SceneManager.LoadScene(0);
        }
        themeInput.gameObject.transform.parent.gameObject.SetActive(false);

        ButtonManager.instance.ButtonPlay();
        screenBlur.enabled = false;
        isStarted = true;
		this.GetComponent<TrignometricMovement>().enabled = false;
		rigid.isKinematic = false;
		rigid.AddForce(new Vector2(0, jumpForce));
        //sign.transform.DOScale(new Vector3(1, 1, 1), 2);
    }

    public void ReplayButton()
    {
        //FacebookAnalytics.Instance.LogGame_endEvent(1);
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "game", (int)currentStage);
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
