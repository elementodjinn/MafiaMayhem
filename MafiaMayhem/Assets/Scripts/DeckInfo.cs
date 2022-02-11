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
            //Debug.Log("You tried to draw a non-card object");
        }
    }
    //Removes top card of deck, returns the card object
    public GameObject DrawCard()
    {
        count = cards.Count;
        //Debug.Log("Count: " + count);
        if (count == 0)
        {
            return null;
        }
        int index = Random.Range(0, count-1);
        GameObject drawnCard = cards[index];
        cards.RemoveAt(index);
        ChangeCount(-1);
        return drawnCard;
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

    public void ShuffleDeck()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            GameObject temp = cards[i];
            int randomIndex = Random.Range(i, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }
}
