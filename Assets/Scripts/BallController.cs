using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed;
    public GameObject particle;
    Rigidbody rb;
    bool started = false;
    bool gameOver;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        started = false;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            if (Input.GetMouseButtonDown(0))  // 0 -> Left mouse button
            {
                rb.velocity = new Vector3(speed, 0, 0);
                started = true;

                GameManager.instance.StartGame();
            }
        }

        // Cast the ray downward, 1 unit long, starting from ball's postion
        var collide = Physics.Raycast(transform.position, Vector3.down, 1f);

        Debug.DrawRay(transform.position, Vector3.down, Color.red);

        if (!collide)
        {
            gameOver = true;

            // Make ball fall down
            rb.velocity = new Vector3(0, -25f, 0);

            // When game over, then don't follow the ball
            Camera.main.GetComponent<CameraFollow>().gameOver = true;

            GameManager.instance.GameOver();

        }

        if (Input.GetMouseButtonDown(0) && !gameOver)  // 0 -> Left mouse button
        {
            SwitchDirection();
        }
    }

    // Switch between x and z direction
    void SwitchDirection ()
    {
        if (rb.velocity.z > 0)
        {
            rb.velocity = new Vector3(speed, 0, 0);  //Change direction to x dir
        } else if (rb.velocity.x > 0)
        {
            rb.velocity = new Vector3(0, 0, speed); // Change direction to z dir
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Diamond")
        {
            ScoreManager.instance.BonusPoints();
            GameObject part = Instantiate(particle, other.gameObject.transform.position, Quaternion.identity) as GameObject;
            Destroy(other.gameObject);  // Destroy the diamond
            Destroy(part, 1f);
        }
    }
}
