using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    private List<GameObject> currentHand;

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
            for (int i = currentHand.Count; i < 5; i++)
            {
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
        GameObject cardObject = card.gameObject;
        RemoveCard(cardObject);
        DDHL.AddCardToDiscard(card);
    }

    //Removes a card from the hand
    public void RemoveCard(GameObject card)
    {
        currentHand.Remove(card);
    }

    public void DiscardWholeHand()
    {
        for(int i = 0; i < 5; i++)
        {
            GameObject card = currentHand[0];
            currentHand.RemoveAt(0);
            DDHL.AddCardToDiscard(card.GetComponent<CardDisplay>());
        }
    }

    public void TakeDamageFromDeck(int i)
    {
        DDHL.TakeExtraDamage(i);
    }
}
