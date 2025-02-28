using UnityEngine;

public class Instatiat : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject card;
    
    
    void Start()
    {
        Instantiate(card,new Vector2(0,0),Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
