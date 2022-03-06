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
    [SerializeField] private DecisionMaker DM;
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
            Debug.Log(assignedCard.card.name);
            if (assignedCard.card.name == "Throw")
            {
                if(stateManager.whoseThrowing)//true when p1 played the throw card, false if p2 played the throw card
                {
                    DM.Damage(false);
                }
                else
                {
                    DM.Damage(true);
                }
                stateManager.GetPlayer1Hand().drawFullHand();
                stateManager.GetPlayer2Hand().drawFullHand();
            }
            DM.throwMinigame = false;
        }
        else
        {
            PlayCard(assignedCard.card);
        }
    }

    
}
