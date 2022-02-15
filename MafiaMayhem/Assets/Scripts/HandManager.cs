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
        if(currentHand.Count == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                drawCard();
            }
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
}
