using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.TextCore.Text;
using static UnityEngine.Rendering.DebugUI;
public class main : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject[] cards = new GameObject[56];
    public GameObject cardTemplate;
    public GameObject stackTemplate;
    public GameObject combinationTemplate;
    public GameObject playerTemplate;
    public Sprite[] cardTextures = new Sprite[56];
    public string[] cardNames = new string[56];
    public int[] cardValues = new int[56];


    private string[] colors = { "rot", "blau", "gruen", "schw" };
    private string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "t", "j", "q", "k", "a" };
    private string[] specialCards = { "mahjong", "phoenix", "dog", "dragon" };

    private string[] actualColors = { "red", "blue", "green", "black" };


    void Start()
    {
        LoadCards();

        for (int i = 0; i < cards.Length; i++)
        {
            string cardName;


            
            cards[i] = Instantiate(cardTemplate, new Vector2(i, 0), Quaternion.identity);
            cardScript cScript = cards[i].GetComponent<cardScript>();
            if (cScript != null)
            {
                cScript.cardTexture = cardTextures[i]; // Store the assigned sprite
                cScript.name = cardNames[i];
                cScript.value = cardValues[i];
            }
        }

        // ------------------------- Initialising pickup Stack -----------------------------------

        cards = cards.OrderBy(x => Random.value).ToArray();

        GameObject distributorStack = Instantiate(stackTemplate, new Vector2(0,0), Quaternion.identity);
        stackScript distributorStackScript = distributorStack.GetComponent<stackScript>();
        distributorStackScript.displayType = stackScript.DisplayType.TopFlipped;


        foreach (GameObject card in cards)
        {
            distributorStackScript.Push(card);
        }

        // ------------------------- Initialising Players -----------------------------------------

        GameObject player_1 = Instantiate(playerTemplate, new Vector2(5, 3), Quaternion.identity);
        GameObject player_2 = Instantiate(playerTemplate, new Vector2(-5, 3), Quaternion.identity);
        GameObject player_3 = Instantiate(playerTemplate, new Vector2(5, -3), Quaternion.identity);
        GameObject player_4 = Instantiate(playerTemplate, new Vector2(-5, -3), Quaternion.identity);

        playerScript player_1_script = player_1.GetComponent<playerScript>();
        playerScript player_2_script = player_2.GetComponent<playerScript>();
        playerScript player_3_script = player_3.GetComponent<playerScript>();
        playerScript player_4_script = player_4.GetComponent<playerScript>();

        player_1_script.Initialise();
        player_2_script.Initialise();
        player_3_script.Initialise();
        player_4_script.Initialise();

        //player_1_script.inventoryScript.Push(distributorStackScript.PullTop());
        Debug.Log(distributorStackScript.cardList.Count() / 4);
        int cnt = distributorStackScript.cardList.Count() / 4;

        for (int i = 0; i < cnt; i++)
        {
            Debug.Log(i);
            player_1_script.inventoryScript.Push(distributorStackScript.PullTop());
            player_2_script.inventoryScript.Push(distributorStackScript.PullTop());
            player_3_script.inventoryScript.Push(distributorStackScript.PullTop());
            player_4_script.inventoryScript.Push(distributorStackScript.PullTop());
        }



    }




    // Update is called once per frame
    void Update()
    {
        
    }


    void LoadCards() //Made by GPT because it's boring and mind-numbing and boring
    {
        int index = 0;

        // Load normal suit cards (52 total)
        foreach (string color in colors)
        {
            foreach (string value in values)
            {
                string path = $"Sprites/cardImages/{color}{value}";
                Sprite sprite = Resources.Load<Sprite>(path);

                cardValues[index] = index % 13 + 2;
                cardNames[index] = $"{color}-{value}"; 

                if (sprite != null)
                {
                    cardTextures[index] = sprite;
                }
                else
                {
                    Debug.LogWarning($"Sprite not found: {path}");
                }

                index++;
            }
        }

        // Load special cards (last 4 slots)
        foreach (string specialCard in specialCards)
        {
            string path = $"Sprites/cardImages/{specialCard}";
            Sprite sprite = Resources.Load<Sprite>(path);

            if (sprite != null)
            {
                cardTextures[index] = sprite;
            }
            else
            {
                Debug.LogWarning($"Sprite not found: {path}");
            }

            cardNames[index] = specialCard;
            cardValues[index] = 0;

            if (specialCard == "dragon")
            {
                cardValues[index] = 15;
            }

            index++;
        }
    }
    public List<GameObject> ShuffleGameObjects(List<GameObject> objects)
    {
        List<GameObject> shuffledList = new List<GameObject>(objects);
        int count = shuffledList.Count;
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(i, count);
            (shuffledList[i], shuffledList[randomIndex]) = (shuffledList[randomIndex], shuffledList[i]);
        }
        return shuffledList;
    }

}

