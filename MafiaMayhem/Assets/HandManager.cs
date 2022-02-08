using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    private List<GameObject> currentHand;

    [SerializeField] private DeckInfo Deck;

    [SerializeField] private GameObject[] visibleCards;
    // Start is called before the first frame update
    void Start()
    {
        currentHand = new List<GameObject>();
        for(int i = 0; i < 5; i++)
        {
            GameObject currentCard = Deck.DrawCard();
            Debug.Log(currentCard.GetComponent<CardDisplay>().card.name);
            currentHand.Add(currentCard);
            visibleCards[i].GetComponent<CardDisplay>().setNewCard(currentCard.GetComponent<CardDisplay>().card);
        }
    }
}
