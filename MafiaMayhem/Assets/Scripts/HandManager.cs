using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> currentHand;

    [SerializeField] private DeckInfo Deck;

    [SerializeField] private DeckDiscardHandLinker DDHL;

    [SerializeField] private GameObject[] visibleCards;
    // Start is called before the first frame update
    void Start()
    {
        currentHand = new List<GameObject>();
        drawFullHand();
    }

    public void drawFullHand()
    {
        //Debug.Log("Trying to Draw");
        for (int i = currentHand.Count; i < 5; i++)
        {
            //Debug.Log("Drawing");
            drawCard();
        }

    }

    public void drawCard()
    {
        GameObject currentCard = Deck.DrawCard();
        if (currentCard)
        {
            //Debug.Log(currentCard.GetComponent<CardDisplay>().card.name);
            currentHand.Add(currentCard);
        }
        else if (!DDHL.DiscardEmpty())
        {
            DDHL.ShuffleDiscardIntoDeck();
            currentCard = Deck.DrawCard();
            currentHand.Add(currentCard);
        }
        else
        {
            //Debug.Log("no CurrentCard");
        }
    }

    public void showHand()
    {
        //Debug.Log(currentHand.Count);
        for (int i = 0; i < 5; i++)
        {
            GameObject currentCard = currentHand[i];
            visibleCards[i].GetComponent<CardDisplay>().setNewCard(currentCard.GetComponent<CardDisplay>().card);
        }
    }

    public CardInfo getCardInHand(int i)
    {
        return currentHand[i].GetComponent<CardDisplay>().card;
    }

    //Removes a card from the hand and adds it to the discard pile
    public void Discard(CardDisplay card)
    {
        GameObject cardObject = null;
        foreach(GameObject c in currentHand)
        {
            if(c.GetComponent<CardDisplay>().card == card.getCard())
            {
                cardObject = c;
            }
        }
        currentHand.Remove(cardObject);
        DDHL.AddCardToDiscard(cardObject);
    }

    //Removes a card from the hand
    public void RemoveCard(CardDisplay card)
    {
        Debug.Log("Removing Card" + card.name);
        GameObject cardObject = null;
        foreach (GameObject c in currentHand)
        {
            if (c.GetComponent<CardDisplay>().card == card.getCard())
            {
                cardObject = c;
            }
        }
        currentHand.Remove(cardObject);
    }

    public void DiscardWholeHand()
    {
        int j = currentHand.Count;
        for (int i = 0; i < j; i++)
        {
            GameObject card = currentHand[0];
            currentHand.RemoveAt(0);
            DDHL.AddCardToDiscard(card);
        }
    }

    public void TakeDamageFromDeck(int i)
    {
        DDHL.TakeExtraDamage(i);
    }

    public bool DeckisEmpty()
    {
        if(Deck.getCount() == 0)
        {
            return true;
        }
        return false;
    }
}
