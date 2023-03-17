using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Fish : MonoBehaviour
{

    [Header("Basic Components")]
    private Rigidbody2D rb;

    [Header("Fish Components")]
    public float fishSpeed;
    public float minS;
    public float maxS;

    [Header("Mechanics")]
    [SerializeField] private bool catching;
    public bool gotFish;
    [SerializeField] private bool outLevel;
    public float fishingCounter = 10;
    [SerializeField] private float counterRate;

    [Header("Score Components")]
    public TextMeshProUGUI scoreTxt;
    private int score;
    public TextMeshProUGUI captureTxt;
    private float capture;

    void Start()
    { 
        #region ComponentsAssociation
        score = 0;
        rb = GetComponent<Rigidbody2D>();

        #endregion

    }
    void OnTriggerStay2D(Collider2D Collider)
    {
        #region Fishing

        if (Collider.gameObject.tag == "Player")
        {
            catching = true;
        }
        #endregion

        #region OutLevel

        if (Collider.gameObject.tag == "UpperWall")
        {
            rb.velocity = new Vector2 (rb.velocity.x ,  -1);
            outLevel = true;
        }

        if (Collider.gameObject.tag == "DownWall")
        {
            rb.velocity = new Vector2 (rb.velocity.x , 1);
            outLevel = true;
        }
        #endregion
    }
    void FixedUpdate()
    {
        #region Fishing Movement

        if (outLevel == false)
        {
            float speed = Random.Range(minS, maxS) * fishSpeed;
            float yVelocity = Mathf.Lerp(rb.velocity.y, speed , Time.deltaTime);
            rb.velocity = new Vector2(rb.velocity.x, yVelocity);
        }
        #endregion

        #region Score

        scoreTxt.text = score.ToString();
        captureTxt.text = capture.ToString();
        capture = fishingCounter;
        #endregion

        #region Catching The Fish

        switch (catching)
        {
            case true:
                fishingCounter += Time.deltaTime * counterRate;
                break;
            case false:
                fishingCounter -= Time.deltaTime * counterRate;
                break;
        }
        #endregion

        #region Reset Condition

        if (fishingCounter <= 0)
        {
            Respawn();
        }
        #endregion

        #region Win Condition

        switch (fishingCounter)
        {
            case var fc when fc >= 300:
                gotFish = true;
                transform.position = new Vector2 (0.038f , 0);
                fishingCounter = 150;
                score ++;
                break;
            default:
                break;
        }
        #endregion

        outLevel = false;
        catching = false;
    }

    #region Reset

    void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion
}
