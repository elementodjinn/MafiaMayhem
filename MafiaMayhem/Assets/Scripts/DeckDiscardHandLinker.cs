using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckDiscardHandLinker : MonoBehaviour
{
    [SerializeField]
    private DeckInfo deck;
    [SerializeField]
    private DeckInfo discard;
    [SerializeField]
    private HandManager hand;

    public void AddCardToDiscard(CardDisplay card)
    {
        discard.AddCard(card.gameObject);
    }

    public void ShuffleDiscardIntoDeck()
    {
        discard.ShuffleDeck();
        int n = discard.getCount();
        GameObject cardToPass;
        while(n > 0)
        {
            cardToPass = discard.DrawCard();
            deck.AddCard(cardToPass);
            n--;
        }
    }

    //Send a card straight from the hand to the discard, mainly a proof of concept
    public void SendCardFromDeckToDiscard()
    {
        discard.AddCard(deck.DrawCard());
    }

    //Taking Damage, for now, doesn't take into account hand.
    public void TakeExtraDamage(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            deck.DrawCard();
        }
    }

    public bool DiscardEmpty()
    {
        if(discard.getCount() > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
