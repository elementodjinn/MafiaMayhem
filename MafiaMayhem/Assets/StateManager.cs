using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StateManager : MonoBehaviour
{
    private int currentTurn = 1;

    [SerializeField] private TMP_Text betweenTurnsText;

    public void updateTurn()
    {
        currentTurn++;
        if (currentTurn == 1)
        {
            betweenTurnsText.text = "Player 1's turn";
        }
        else if (currentTurn == 2)
        {
            betweenTurnsText.text = "Player 2's turn";
        }
        else if(currentTurn == 3)
        {
            betweenTurnsText.text = "Ready to flip?";
            currentTurn = 0;
        }
    }
}
