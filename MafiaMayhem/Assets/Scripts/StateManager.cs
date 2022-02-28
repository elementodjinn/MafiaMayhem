using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI thisText;
    [SerializeField] private Image backdrop;
    [SerializeField] private Button nextButton;
    [SerializeField] private TextMeshProUGUI buttonText;

    [SerializeField] private GameObject handDisplay;
    [SerializeField] private GameObject PlayedCards;
    [SerializeField] private GameObject handButtons;

    [SerializeField] private HandManager Player1Hand;
    [SerializeField] private HandManager Player2Hand;
    [SerializeField] private DecisionMaker DM;

    private int turnCount = 1;
    private bool firstTurn = true;
    private void Start()
    {
        //start on player 1's turn
        thisText.text = "Player 1, pick a card. Player 2, eyes off the screen!";
        backdrop.enabled = true;
        nextButton.enabled = true;
    }
    public void determineCurrentPhase() //more controls the screens between turns if that makes sense
    {
        Debug.Log(turnCount);
        if (turnCount == 1) //player 1's turn
        {
            PlayedCards.SetActive(false);
            handDisplay.SetActive(true);
            Player1Hand.showHand();
            thisText.text = "Player 1, pick a card. Player 2, eyes off the screen!";
        }
        else if(turnCount == 2) //player 2's turn
        {
            Player2Hand.showHand();
            thisText.text = "Player 2, pick a card. Player 1, no peeking!";
        }
        else if(turnCount == 3) //sets up pre flip screen
        {
            thisText.text = "Ready? FIGHT!";
            buttonText.text = "Flip Cards";
            DM.DecideFate();
            
            turnCount = 0;
        }
    }

    public void Flip() //flip played cards and compare them
    {
        if(turnCount == 0) //when turncount = 0, we can compare cards
        {
            PlayedCards.SetActive(true);
            handDisplay.SetActive(false);
            handButtons.SetActive(false);
        }
        else
        {
            handButtons.SetActive(true);
        }
    }

    public int currentplayerTurn()
    {
        return turnCount;
    }

    public void incrementTurn()
    {
        turnCount++;
    }

    public HandManager GetPlayer1Hand()
    {
        return Player1Hand;
    }
    public HandManager GetPlayer2Hand()
    {
        return Player2Hand;
    }

    public List<CardDisplay> GetCardsPlayed()
    {
        List<CardDisplay> results = new List<CardDisplay>();
        CardDisplay[] cards = PlayedCards.GetComponentsInChildren<CardDisplay>();
        results.Add(cards[0]);
        results.Add(cards[1]);
        return results;
    }
}
