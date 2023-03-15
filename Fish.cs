using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Fish : MonoBehaviour
{
    private Rigidbody2D rb;
    public float fishingCounter = 50;
    public bool gotFish;
    [SerializeField] private bool catching;
    [SerializeField] private bool outLevel;
    [SerializeField] private float fishSpeed;
    [SerializeField] private float minS;
    [SerializeField] private float maxS;

    public TextMeshProUGUI scoreTxt;
    private int score;

    public TextMeshProUGUI captureTxt;
    private float capture;

    void Start()
    {      
        score = 0;
        rb = GetComponent<Rigidbody2D>();

    }
    void OnTriggerStay2D(Collider2D Collider)
    {
        if (Collider.gameObject.tag == "Player")
        {
            catching = true;
        }

        if (Collider.gameObject.tag == "UpperWall")
        {
            rb.velocity = new Vector2 (rb.velocity.x ,  -5);
            outLevel = true;
        }

        if (Collider.gameObject.tag == "DownWall")
        {
            rb.velocity = new Vector2 (rb.velocity.x , 5);
            outLevel = true;
        }
    }
    void FixedUpdate()
    {
        if (outLevel == false)
        {
            rb.velocity = new Vector2(rb.velocity.x , Random.Range(minS, maxS) * fishSpeed);
        }

        scoreTxt.text = score.ToString();
        captureTxt.text = capture.ToString();
        capture = fishingCounter;

        if (catching == true)
        {
            fishingCounter ++;
        }
        else
        {
            fishingCounter --;
        }

        if (fishingCounter <= 0)
        {
            Respawn();
        }

        if(fishingCounter >= 300)
        {
            gotFish = true;
            transform.position = new Vector2 (0.038f , 0);
            fishingCounter = 150;
        }

        if(gotFish == true)
        {
            score = score + 1;
        }

        gotFish = false;
        catching = false;
        outLevel = false;
    }

    void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
