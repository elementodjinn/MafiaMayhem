using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckDiscardHandLinker : MonoBehaviour
{
    [SerializeField]
    private DeckInfo deck;
    [SerializeField]
    private DeckInfo discard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void SendCardFromDeckToDiscard()
    {
        discard.AddCard(deck.DrawCard());
    }
}
