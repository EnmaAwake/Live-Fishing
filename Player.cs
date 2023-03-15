using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float jumpForce;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Time.timeScale = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x , 1 * jumpForce);

            if(Time.timeScale == 0f)
            {
                Time.timeScale = 1f;
            }
        }
    }
}
