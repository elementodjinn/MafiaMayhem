using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPlayer : MonoBehaviour
{
    [SerializeField] private CardDisplay Playcard1;
    [SerializeField] private CardDisplay Playcard2;
    [SerializeField] private CardDisplay assignedCard;

    [SerializeField] private StateManager stateManager;
    public void PlayCard(CardInfo newCard)
    {
        //if player 1's turn, put this card in player 1's spot
        if(stateManager.currentplayerTurn() == 1)
        {
            Playcard1.setNewCard(newCard);
        }
        else if (stateManager.currentplayerTurn() == 2)
        {
            Playcard2.setNewCard(newCard);
        }
    }

    public void chooseCard()
    {
        PlayCard(assignedCard.card);
    }
}
