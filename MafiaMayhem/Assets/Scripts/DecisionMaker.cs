using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionMaker : MonoBehaviour
{
    [SerializeField]
    private StateManager stateManager;

    private GameObject card1;
    private GameObject card2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Does the logic for the two selected cards, deals damage, and then draws for their new hands.
    public void DecideFate()
    {
        List<CardDisplay> cards = stateManager.GetCardsPlayed();
        card1 = cards[0].gameObject;
        card2 = cards[1].gameObject;
        CardInfo p1 = cards[0].getCard();
        CardInfo p2 = cards[1].getCard();
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
        if (p1.CardName == "Reload" && p2.CardName != "A Change in Plans")
        {
            Reload(true);
        }
        if (p2.CardName == "Reload" && p1.CardName != "A Change in Plans")
        {
            Reload(false);
        }
        stateManager.GetPlayer1Hand().drawFullHand();

        stateManager.GetPlayer2Hand().drawFullHand();
    }

    //True means p1 is taking damage, false means p2
    //Damages the played card from the chosen player
    public void Damage(bool player)
    {
        if (player)
        {
            HandManager hand = stateManager.GetPlayer1Hand();
            hand.RemoveCard(card1.gameObject);
        }
        else
        {
            HandManager hand = stateManager.GetPlayer2Hand();
            hand.RemoveCard(card2.gameObject);
        }
    }

    //True means p1 is discarding, false means p2
    //Discards the played card from the chosen player
    public void Discard(bool player)
    {
        if (player)
        {
            HandManager hand = stateManager.GetPlayer1Hand();
            hand.Discard(card1.GetComponent<CardDisplay>());
        }
        else
        {
            HandManager hand = stateManager.GetPlayer2Hand();
            hand.Discard(card1.GetComponent<CardDisplay>());
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

    //Deals the damage of Change In Plans to the desired player
    public void CIP(bool player)
    {
        HandManager hand;
        if (player)
        {
            hand = stateManager.GetPlayer1Hand();
            hand.RemoveCard(card1);
        }
        else
        {
            hand = stateManager.GetPlayer2Hand();
            hand.RemoveCard(card2);
        }
        hand.TakeDamageFromDeck(2);
        hand.DiscardWholeHand();
    }
}
