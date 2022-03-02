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
    [SerializeField] private GameObject Hider;
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
        if (stateManager.currentplayerTurn() == 0)//if we're in the throw minigame
        {
            Hider.SetActive(false);
            if (assignedCard.card.name == "Throw")
            {
                
            }
        }
        else
        {
            PlayCard(assignedCard.card);
        }
    }

    
}
