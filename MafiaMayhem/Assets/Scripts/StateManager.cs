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

    [SerializeField] private HandManager Player1Hand;
    [SerializeField] private HandManager Player2Hand;

    private int turnCount = 0;

    private void Start()
    {
        Player1Hand.showHand();
        thisText.text = "Player 1, pick a card. Player 2, eyes off the screen!";
        backdrop.enabled = true;
        nextButton.enabled = true;
    }
    public void determineCurrentPhase()
    {
        turnCount++;
        Debug.Log(turnCount);
        if (turnCount == 1)
        {
            handDisplay.SetActive(true);
            Player1Hand.showHand();
            thisText.text = "Player 1, pick a card. Player 2, eyes off the screen!";
        }
        else if(turnCount == 2)
        {
            Player2Hand.showHand();
            thisText.text = "Player 2, pick a card. Player 1, no peeking!";
        }
        else if(turnCount == 3)
        {
            handDisplay.SetActive(false);
            thisText.text = "Ready? FIGHT!";
            buttonText.text = "Flip Cards";
            turnCount = 0;
        }
    }
}
