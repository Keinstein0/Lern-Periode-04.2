using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using static cardScript;

public class stackScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public List<GameObject> cardList;

    public Vector3 lastPosition;

    public enum DisplayType
    {
        TopVisible,
        TopFlipped,
        SpreadVisible,
        SpreadCovered,
        Hidden,
    }
    public enum StackType
    {
        card,
        combination
    }

    public StackType stackType;
    public DisplayType displayType;
    private DisplayType displayTypeLast;
    private bool firstPull = true;

    
    void Start()
    {
        this.gameObject.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if ((this.transform.position) != lastPosition || displayType != displayTypeLast){
            displayTypeLast = displayType;
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
        if (firstPull)
        {
            if (card.GetComponent<cardScript>() != null)
            {
                stackType = StackType.card;
            }
            else
            {
                stackType = StackType.combination;
            }
            firstPull = false;
        }
        
        card.transform.position = (this.transform.position); // Update Position of the Card

        cardList.Add(card);
        OnUpdate();
        

    }

    private void OnUpdate()
    {
        foreach (var card in cardList)
        {
            if (stackType == StackType.combination)
            {
                renderCombination();
            }
            else
            {
                renderCards();
            }
        }
    }
    private void renderCards()
    {
        float offset = 0;
        const float offsetIncrease = (float)1;
        float indexCounter = 0;


        switch (displayType)
        {
            case DisplayType.TopVisible:
                foreach (var card in cardList)
                {
                    cardScript cScript = card.GetComponent<cardScript>();
                    card.transform.position = (this.transform.position);
                    if (card == cardList[^1])
                    {
                        cScript.isVisible = cardScript.Visible.show;
                    }
                    else
                    {
                        cScript.isVisible = cardScript.Visible.hide;
                    }
                }
                break;
            case DisplayType.TopFlipped:
                foreach (var card in cardList)
                {
                    cardScript cScript = card.GetComponent<cardScript>();
                    card.transform.position = (this.transform.position);
                    if (card == cardList[^1])
                    {
                        cScript.isVisible = cardScript.Visible.cover;
                    }
                    else
                    {
                        cScript.isVisible = cardScript.Visible.hide;
                    }
                }
                break;
            case DisplayType.SpreadVisible:
                foreach (var card in cardList)
                {
                    cardScript cScript = card.GetComponent<cardScript>();

                    card.transform.position = new Vector3(this.transform.position.x + offset, this.transform.position.y, indexCounter);
                    offset += offsetIncrease;

                    cScript.isVisible = cardScript.Visible.show;

                    indexCounter++;
                }
                break;
            case DisplayType.SpreadCovered:
                foreach (var card in cardList)
                {
                    cardScript cScript = card.GetComponent<cardScript>();

                    card.transform.position = new Vector3(this.transform.position.x + offset, this.transform.position.y, indexCounter);
                    offset += offsetIncrease;

                    cScript.isVisible = cardScript.Visible.cover;

                    indexCounter++;
                }
                break;
            case DisplayType.Hidden:
                foreach (var card in cardList)
                {
                    cardScript cScript = card.GetComponent<cardScript>();
                    card.transform.position = (this.transform.position);
                    cScript.isVisible = cardScript.Visible.hide;

                }
                break;
        }
    }



    private void renderCombination()
    {
        foreach (var combination in cardList)
        {
            combinationScript cScript = combination.GetComponent<combinationScript>();

            combination.transform.position = (this.transform.position);

            if (combination == cardList[^1])
            {
                cScript.visibility = combinationScript.CombinationVisibility.visible;
            }
            else
            {
                cScript.visibility = combinationScript.CombinationVisibility.hidden;
            }
        }
    }

}
