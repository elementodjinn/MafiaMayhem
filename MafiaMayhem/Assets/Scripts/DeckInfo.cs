using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckInfo : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> cards;
    private int count;
    public DeckDisplay display;
    // Start is called before the first frame update
    void Start()
    {
        count = cards.Count;
        display.ChangeDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Adds a specified object to the deck
    public void AddCard(GameObject card)
    {
        if(card.GetComponent<CardDisplay>())
        {
            cards.Add(card);
            ChangeCount(1);
        }
        else
        {
            Debug.Log("You tried to draw a non-card object");
        }
    }
    //Removes top card of deck, future add it to player's hand
    public void DrawCard()
    {
        cards.RemoveAt(0);
        ChangeCount(-1);
    }

    void ChangeCount(int amount)
    {
        count += amount;
        display.ChangeDisplay();
    }

    public int getCount()
    {
        return count;
    }
}
