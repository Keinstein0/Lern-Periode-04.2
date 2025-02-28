using UnityEngine;

public class cardScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Sprite cardTexture;
    
    public enum Visible
    {
        hide,
        cover,
        show
    }

    public Visible isVisible;
    
    
    
    
    
    void Start()
    {
        isVisible = Visible.show;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
