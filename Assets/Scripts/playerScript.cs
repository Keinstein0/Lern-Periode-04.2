using UnityEngine;

public class playerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject inventoryStack;
    public stackScript inventoryScript;
    
    public void Initialise()
    {
        inventoryStack = Instantiate(inventoryStack, this.transform.position, Quaternion.identity);
        inventoryScript = inventoryStack.GetComponent<stackScript>();
        inventoryScript.displayType = stackScript.DisplayType.SpreadVisible;
    }
    
    void Start()
    {
        this.gameObject.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
