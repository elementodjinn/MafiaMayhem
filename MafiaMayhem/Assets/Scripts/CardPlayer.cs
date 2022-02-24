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

    //Does the logic for the two selected cards, deals damage, and then draws for their new hands.
    private void DecideFate()
    {
        CardInfo p1 = Playcard1.card;
        CardInfo p2 = Playcard2.card;
        if (p1 == p2)
        {
            if (p1.CardName == "Melee")
            {
                //Both take 1 damage
                Damage(true);
                Damage(false);
            }
            else if (p1.CardName == "Pistol")
            {
                //Minigame
            }
            else
            {
                //Nothing happens, both cards are discarded.
                Discard(true);
                Discard(false);
            }
        }
        //If p1 beats p2
        else if (p1.beats.Contains(p2))
        {
            if (p1.CardName == "A Change in Plans")
            {
                //Change In Plans function p1 v p2
                CIP(false);
            }
            else
            {
                if (p1.Minigame.Contains(p2))
                {
                    if (p1.CardName == "Guard")
                    {
                        //Guard Minigame
                    }
                    else if (p1.CardName == "Throw")
                    {
                        //Damage Function
                        Damage(false);
                        //Throw Minigame
                    }
                }
                else
                {
                    //Damage Function (Melee v Throw, Pistol v Melee, All damaging ones v reload)
                    Damage(false);
                }
            }
        }
        else if (p2.beats.Contains(p1))
        {
            if (p2.CardName == "A Change in Plans")
            {
                //Change In Plans function p2 v p1
                CIP(true);
            }
            else
            {
                if (p2.Minigame.Contains(p1))
                {
                    if (p2.CardName == "Guard")
                    {
                        //Guard Minigame
                    }
                    else if (p2.CardName == "Throw")
                    {
                        //Damage Function
                        Damage(true);
                        //Throw Minigame
                    }
                }
                else
                {
                    //Damage Function (Melee v Throw, Pistol v Melee, All damaging ones v reload)
                    Damage(true);
                }
            }
        }
        else
        {
            Discard(true);
            Discard(false);
        }
        if(p1.CardName == "Reload" && p2.CardName != "A Change in Plans")
        {
            Reload(true);
        }
        if(p2.CardName == "Reload" && p1.CardName != "A Change in Plans")
        {
            Reload(false);
        }
        stateManager.GetPlayer1Hand().drawFullHand();

        stateManager.GetPlayer2Hand().drawFullHand();
    }

    public void Damage(bool player)
    {
        if (player)
        {
            HandManager hand = stateManager.GetPlayer1Hand();
            hand.RemoveCard(Playcard1.gameObject);
        }
        else
        {
            HandManager hand = stateManager.GetPlayer2Hand();
            hand.RemoveCard(Playcard2.gameObject);
        }
    }

    //True means p1, false means p2
    //Discards the played card from the chosen player
    public void Discard(bool player)
    {
        if(player)
        {
            HandManager hand = stateManager.GetPlayer1Hand();
            hand.Discard(Playcard1);
        }
        else
        {
            HandManager hand = stateManager.GetPlayer2Hand();
            hand.Discard(Playcard2);
        }
    }

    public void Reload(bool player)
    {
        HandManager hand;
        if (player)
        {
            hand = stateManager.GetPlayer1Hand();
        }
        else
        {
            hand = stateManager.GetPlayer2Hand();
        }
        hand.DiscardWholeHand();
    }

    public void CIP(bool player)
    {
        HandManager hand;
        if (player)
        {
            hand = stateManager.GetPlayer1Hand();
            hand.RemoveCard(Playcard1.gameObject);
        }
        else
        {
            hand = stateManager.GetPlayer2Hand();
            hand.RemoveCard(Playcard2.gameObject);
        }
        hand.TakeDamageFromDeck(2);
        hand.DiscardWholeHand();
    }
}
