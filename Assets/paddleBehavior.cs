using UnityEngine;

public class paddleBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float yPos = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            yPos += 5f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            yPos -= 5f * Time.deltaTime;
        }
        transform.position = new Vector2 (8, yPos);
    }
}
