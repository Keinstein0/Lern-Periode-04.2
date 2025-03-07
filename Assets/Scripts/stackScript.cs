using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class stackScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public List<GameObject> cardList;

    public Vector3 lastPosition;
    //public Vector2 postition;
    
    
    void Start()
    {
        this.gameObject.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if ((this.transform.position) != lastPosition){
            lastPosition = this.transform.position;
            OnUpdate();
        }
    }

    public GameObject Pull(int index)
    {
        GameObject card = cardList[index];
        
        cardList.Remove(card);
        OnUpdate();
        return card;
    }
    public GameObject PullTop()
    {
        
        GameObject card = cardList[^1]; // Returns the item at the last index
        cardList.Remove(card);

        OnUpdate();

        return card; 
    }
    
    
    public void Push(GameObject card)
    {
        card.transform.position = (this.transform.position); // Update Position of the Card

        cardList.Add(card);
        OnUpdate();

    }

    private void OnUpdate()
    {
        foreach (var card in cardList)
        {
            card.transform.position = (this.transform.position);
            cardScript cScript = card.GetComponent<cardScript>();
            if (card == cardList[^1])
            {
                cScript.isVisible = cardScript.Visible.show;
            }
            else
            {
                cScript.isVisible = cardScript.Visible.hide;
            }
        }
    }

}
