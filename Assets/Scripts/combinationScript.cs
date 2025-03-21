using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static cardScript;

public class combinationScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public List<GameObject> cardList;

    public Vector3 lastPosition;

    public enum CombinationVisibility
    {
        visible,
        covered,
        hidden
    }

    public enum CombinationType
    {
        einzelkarte, // one card
        doubleKarte, // two cards with same value
        tripleKarte, // three cards with same value
        fullHouse, // double + triple
        treppe, // strasse of doubles
        strasse, // values going up by one for at least 5 cards
        bombe, // 4* the same card
        noStackValue // The Combination doens't yet have a fixed type
    }

    public CombinationVisibility visibility = CombinationVisibility.covered;
    private CombinationVisibility visibilityLast;

    public CombinationType combinationType = CombinationType.noStackValue;
    public int value;
    public int optLength; 

    void Start()
    {
        this.gameObject.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if ((this.transform.position) != lastPosition || visibility != visibilityLast)
        {
            lastPosition = this.transform.position;
            visibilityLast = visibility;
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

    public bool verifyCards(CombinationType optCombination = CombinationType.noStackValue) // WARNING: Uses limited recursion, Max depth: 2
    {
        bool valid = false;
        optLength = 0;
        value = 0;

        CombinationType combinationTypeFinal = (optCombination == CombinationType.noStackValue) ? combinationType : optCombination;

        
        switch (combinationTypeFinal)
        {
            case CombinationType.einzelkarte:
                valid = cardList.Count == 1;
                value = valid ? Value(cardList[0]) : 0;
                break;
            case CombinationType.doubleKarte:
                valid = cardList.Count == 2 && Value(cardList[0]) == Value(cardList[1]);
                value = valid ? Value(cardList[0]) : 0;
                break;
            case CombinationType.tripleKarte:
                valid = cardList.Count == 3 && (Value(cardList[0]) == Value(cardList[1]) && Value(cardList[1]) == Value(cardList[2]));
                value = valid ? Value(cardList[0]) : 0;
                break;
            case CombinationType.fullHouse:
                if (cardList.Count == 5)
                {
                    int[] values = new int[5];
                    for (int i = 0; i < 5; i++) { values[i] = Value(cardList[i]); }
                    Array.Sort(values);
                    valid = values[0] == values[1] && values[3] == values[4] && (values[2] == values[1] || values[2] == values[3]);
                }
                value = valid ? Value(cardList[4]) : 0;
                break;
            case CombinationType.strasse:
                if (cardList.Count >= 5) // Minimal amount of Cards is 5
                {
                    
                    int[] values = new int[cardList.Count];
                    for (int i = 0; i < cardList.Count; i++) { values[i] = Value(cardList[i]); }
                    Array.Sort(values);
                    
                    valid = values.Max() - values.Min() + 1 == values.Length && values.Distinct().Count() == values.Length;

                    value = (valid) ? Value(cardList[0]) : 0;
                    optLength = (valid) ? values.Length : 0;
                }

                break;
            case CombinationType.treppe:
                if (cardList.Count >= 4) // Minimal amount of Cards is 5
                {

                    int[] values = new int[cardList.Count];
                    for (int i = 0; i < cardList.Count; i++) { values[i] = Value(cardList[i]); }
                    Array.Sort(values);

                    valid = (values.Max() - values.Min() + 1)*2 == values.Length && (values.Distinct().Count())*2 == values.Length;

                    value = (valid) ? Value(cardList[0]) : 0;
                    optLength = (valid) ? values.Length : 0;
                }

                break;
            case CombinationType.bombe:
                valid = cardList.Count == 4 && (Value(cardList[0]) == Value(cardList[1]) && Value(cardList[0]) == Value(cardList[2]) && Value(cardList[0]) == Value(cardList[3]));
                value = (valid) ? Value(cardList[0]) : 0;
                break;
            case CombinationType.noStackValue: 
                // If the combination is Freshly created it's type gets discovered here
                CombinationType[] allTypes = {CombinationType.einzelkarte, CombinationType.doubleKarte, CombinationType.tripleKarte, CombinationType.treppe, CombinationType.strasse, CombinationType.fullHouse, CombinationType.bombe };
                foreach (CombinationType type in allTypes)
                {
                    // Loops through all types to find a match, if found it exits the function
                    if (verifyCards(type))
                    {
                        valid = true;
                        combinationType = type;
                        break;
                    }
                }
                // If no match is found it returns false (invalid combination) if one was found the type gets fixed and it returns true (combination valid)
                valid = false;
                break;
        }

        return valid;
    }


    private int Value(GameObject card)
    {
        return card.GetComponent<cardScript>().value;
    }



    private void OnUpdate()
    {
        float offset = 0;
        const float offsetIncreste = (float)1;

        foreach (var card in cardList)
        {
            card.transform.position = new Vector3(this.transform.position.x + offset, this.transform.position.y, this.transform.position.z + offset);
            offset += offsetIncreste;



            cardScript cScript = card.GetComponent<cardScript>();

            switch (visibility)
            {
                case CombinationVisibility.visible:
                    cScript.isVisible = cardScript.Visible.show;
                    break;
                case CombinationVisibility.covered:
                    cScript.isVisible = cardScript.Visible.cover;
                    break;
                case CombinationVisibility.hidden:
                    cScript.isVisible = cardScript.Visible.hide;
                    break;
            }
        }
    }
}
