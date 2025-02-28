using UnityEngine;

public class cardScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Sprite cardTexture;
    public Sprite coveredTexture;

    public GameObject Card;

    public string name;
    public int value;
    
    public enum Visible
    {
        hide,
        cover,
        show
    }

    public Visible isVisible;
    private Visible isVisibleLast = Visible.hide;
    
    
    
    
    
    void Start()
    {
        isVisible = Visible.show;
        Card.name = name;
    }

    // Update is called once per frame
    void Update()
    {
        if (isVisible != isVisibleLast)
        {
            switch (isVisible)
            {
                case Visible.show:
                    this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = cardTexture;
                    break;
                case Visible.cover:
                    this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = coveredTexture;
                    break;
                case Visible.hide:
                    this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    break;
            }
            

        }
        isVisibleLast = isVisible;
        

        
    }
}
