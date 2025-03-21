using UnityEngine;

public class playerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject inventoryStack;
    public stackScript inventoryScript;

    public GameObject combinationTemp;
    public combinationScript combinationTempScript;

    Vector2 defaultPosition;
    
    public void Initialise()
    {
        inventoryStack = Instantiate(inventoryStack, this.transform.position, Quaternion.identity);
        inventoryScript = inventoryStack.GetComponent<stackScript>();
        inventoryScript.displayType = stackScript.DisplayType.TopFlipped;

        combinationTemp = Instantiate(combinationTemp, this.transform.position, Quaternion.identity);
        combinationTempScript = combinationTemp.GetComponent<combinationScript>();
        combinationTempScript.visibility = combinationScript.CombinationVisibility.hidden;

        defaultPosition = transform.position;
    }
    public void OnTurnBegin()
    {
        inventoryScript.displayType = stackScript.DisplayType.SpreadVisible;
        combinationTempScript.visibility = combinationScript.CombinationVisibility.visible;

        transform.position = new Vector2(-6,(float)-0.5);
    }



    public bool OnTurnUpdate()
    {
        int loopIndexer = 0;
        foreach (GameObject card in inventoryScript.cardList)
        {
            cardScript cardsScript = card.GetComponent<cardScript>();
            
            if (cardsScript.checkClick())
            {
                combinationTempScript.Push(inventoryScript.Pull(loopIndexer));
            }
            loopIndexer++;
        }
        loopIndexer = 0;
        foreach (GameObject card in combinationTempScript.cardList)
        {
            cardScript cardsScript = card.GetComponent<cardScript>();

            if (cardsScript.checkClick())
            {
                inventoryScript.Push(combinationTempScript.Pull(loopIndexer));
            }
            loopIndexer++;
        }

        
        if (Input.GetKey(KeyCode.Space) && combinationTempScript.verifyCards())
        {
            Debug.Log("VERIFIED DEPOSIT    "+ combinationTempScript.combinationType);
        }




        return false;
    }

    public void OnTurnEnd()
    {
        transform.position = defaultPosition;
    }
    
    void Start()
    {
        this.gameObject.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        inventoryStack.transform.position = this.transform.position;
        combinationTemp.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 2);
    }
}
