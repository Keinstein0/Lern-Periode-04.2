using System.Collections.Generic;
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

    public CombinationVisibility visibility = CombinationVisibility.covered;
    private CombinationVisibility visibilityLast;

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

    private void OnUpdate()
    {
        float offset = 0;
        const float offsetIncreste = (float)1;

        foreach (var card in cardList)
        {
            card.transform.position = new Vector3(this.transform.position.x + offset, this.transform.position.y, this.transform.position.z);
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
