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

    private int turnCount = 1;

    private void Start()
    {
        thisText.text = "Player 1, pick a card";
        backdrop.enabled = true;
        nextButton.enabled = true;
    }
    public void determineCurrentPhase()
    {
        turnCount++;
        if (turnCount == 1)
        {
            thisText.text = "Player 1, pick a card. Player 2, eyes off the screen!";
        }
        else if(turnCount == 2)
        {
            thisText.text = "Player 2, pick a card. Player 1, no peeking!";
        }
        else if(turnCount == 3)
        {
            thisText.text = "Ready? FIGHT!";
            buttonText.text = "Flip Cards";
            turnCount = 0;
        }
    }
}
