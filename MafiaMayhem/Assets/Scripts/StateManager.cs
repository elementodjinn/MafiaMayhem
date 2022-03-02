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


    [Header("Minigame Canvas'")]
    [SerializeField] private GameObject MeleeCanvas;
    [SerializeField] private GameObject throwCanvas;
    [SerializeField] private GameObject ProjectileCanvas;

    //used specifically for melee minigame, becomes true when one player presses their key
    private bool p1wins = false; 
    private bool p2wins = false;

    //used to prevent players from hurting themselves multiple times in one turn
    private bool p1SelfDamage = false;
    private bool p2SelfDamage = false;

    private int turnCount = 1;
    private bool firstTurn = true;
    private void Start()
    {
        //start on player 1's turn
        thisText.text = "Player 1, pick a card. Player 2, eyes off the screen!";
        backdrop.enabled = true;
        nextButton.enabled = true;
        MeleeCanvas.SetActive(false);
    }

    private void Update()
    {
        if(MeleeCanvas.activeInHierarchy)
        {
            
            if(Input.GetKeyDown(KeyCode.E) && p2wins == false) //if player 1 hit E
            {
                p1wins = true;
            }

            if(Input.GetKeyDown(KeyCode.P) && p1wins == false) //if Player 2 hit P
            {
                p2wins = true;
            }

            if(p1wins)
            {
                Debug.Log("Player 1 wins");
            }
            else if(p2wins)
            {
                Debug.Log("Player 2 wins");
            }

            
        }
        //if the melee minigame is inactive, then pressing E or P results in a false start
        //during a false start, pressing a key does damage to yourself
        else if (turnCount == 0) 
        { 
            if(Input.GetKeyDown(KeyCode.E) && !p1SelfDamage)
            {
                DM.Damage(true);
                p1SelfDamage = true;
            }

            if (Input.GetKeyDown(KeyCode.P) && !p2SelfDamage)
            {
                DM.Damage(false);
                p2SelfDamage = true;
            }
        }

    }

    public void determineCurrentPhase() //more controls the screens between turns if that makes sense
    {
        //Debug.Log(turnCount);
        if (turnCount == 1) //player 1's turn
        {
            PlayedCards.SetActive(false);
            handDisplay.SetActive(true);
            p1SelfDamage = false;
            p2SelfDamage = false;
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
            minigameCheck();
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

    public void minigameCheck()
    {
        if(DM.meleeMinigame)
        {
            Debug.Log("melee Minigame");
            MeleeCanvas.SetActive(true);
        }
        else if(DM.throwMinigame)
        {
            Debug.Log("throw Minigame");
            Player2Hand.showHand();
            throwCanvas.SetActive(true);

        }
        else if(DM.projectileMinigame)
        {
            Debug.Log("Target Practice");
        }
    }
}
