using UnityEngine;

public class cardScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Sprite cardTexture;
    public Sprite coveredTexture;

    public GameObject Card;

    public string name;
    public int value;

    private bool wasClicked = false;
    
    public enum Visible
    {
        hide,
        cover,
        show,
        onLaunch
    }

    public Visible isVisible;
    private Visible isVisibleLast = Visible.onLaunch;
    
    
    
    
    
    void Start()
    {
        isVisible = Visible.show;
        Card.name = name;
    }

    // Update is called once per frame
    void Update()
    {
        if (isVisible != isVisibleLast || true)
        {
            switch (isVisible)
            {
                case Visible.show:
                    this.gameObject.transform.localScale = new Vector3((float)1.375, (float)1.5, 1);
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = cardTexture;
                    break;
                case Visible.cover:
                    this.gameObject.transform.localScale = new Vector3((float)1.375, (float)1.5, 1);
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = coveredTexture;
                    break;
                case Visible.hide:
                    this.gameObject.transform.localScale = new Vector3(0, 0, 0);
                    break;
            }
            

        }
        isVisibleLast = isVisible;
        

        
    }
    void OnMouseDown()
    {
        wasClicked = true;
    }

    public bool checkClick()
    {
        bool tempClick = wasClicked;
        wasClicked = false;
        return tempClick;
    }
    
}
