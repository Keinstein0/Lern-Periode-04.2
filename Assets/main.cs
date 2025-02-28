using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class main : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject[] cards = new GameObject[56];
    public GameObject cardTemplate;
    public Sprite[] cardTextures = new Sprite[56];


    private string[] colors = { "rot", "blau", "gruen", "schw" };
    private string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "t", "j", "q", "k", "a" };
    private string[] specialCards = { "mahjong", "phoenix", "dog", "dragon" };


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
            }




            cards[i].gameObject.GetComponent<SpriteRenderer>().sprite = cardTextures[i];
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }


    void LoadCards()
    {
        int index = 0;

        // Load normal suit cards (52 total)
        foreach (string color in colors)
        {
            foreach (string value in values)
            {
                string path = $"Sprites/cardImages/{color}{value}";
                Sprite sprite = Resources.Load<Sprite>(path);

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

            index++;
        }
    }
}
