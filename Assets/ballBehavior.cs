using System;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class ballBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float xpos = 0;
    float ypos = 0;

    float xvel = 7f;
    float yvel = 7f;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(xpos, ypos);
        xpos += xvel * Time.deltaTime;
        ypos += yvel * Time.deltaTime;

        if (xpos > 9 || xpos < -9)
        {
            xvel *= -1;
        }

        if (ypos > 5 || ypos < -5)
        {
            yvel *= -1;
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Paddle")
        {
            // Reverse direction on collision with the paddle
            xvel *= -1;
        }
    }


}
