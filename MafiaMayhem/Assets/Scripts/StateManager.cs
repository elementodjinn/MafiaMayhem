using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI thisText;
    [SerializeField] private TextMeshProUGUI VictoryText;
    [SerializeField] private Image backdrop;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button RoundButton;
    [SerializeField] private Button ThrowButton;
    [SerializeField] private Button ProjectileButton;
    [SerializeField] private TextMeshProUGUI buttonText;

    [SerializeField] private GameObject handDisplay;
    [SerializeField] private GameObject PlayedCards;
    [SerializeField] private GameObject handButtons;
    [SerializeField] private GameObject hider;

    [SerializeField] public HandManager Player1Hand;
    [SerializeField] public HandManager Player2Hand;
    [SerializeField] private DecisionMaker DM;


    [Header("Minigame Canvas'")]
    [SerializeField] private GameObject MeleeCanvas;
    [SerializeField] private GameObject throwCanvas;
    [SerializeField] private GameObject ProjectileCanvas;
    [SerializeField] private GameObject VictoryCanvas;

    //used specifically for melee minigame, becomes true when one player presses their key
    private bool p1wins = false; 
    private bool p2wins = false;

    //used to prevent players from hurting themselves multiple times in one turn
    private bool p1SelfDamage = false;
    private bool p2SelfDamage = false;

    private int turnCount = 1;
    private bool firstTurn = true;

    [HideInInspector] public bool whoseThrowing; //if true, p1 played the throw card in a throw minigame, if false, p2 did it
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
        if(MeleeCanvas.activeInHierarchy && (!p1wins && !p2wins))
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
                DM.Damage(false);
                Player2Hand.drawFullHand();
            }
            else if(p2wins)
            {
                Debug.Log("Player 2 wins");
                DM.Damage(true);
                Player1Hand.drawFullHand();
            }
            DM.meleeMinigame = false;
            RoundButton.gameObject.SetActive(true);
            

        }
        //if the melee minigame is inactive, then pressing E or P results in a false start
        //during a false start, pressing a key does damage to yourself
        else if (turnCount == 0) 
        { 
            if(Input.GetKeyDown(KeyCode.E) && !p1SelfDamage)
            {
                DM.Damage(true);
                Player1Hand.drawFullHand();
                p1SelfDamage = true;
            }

            if (Input.GetKeyDown(KeyCode.P) && !p2SelfDamage)
            {
                DM.Damage(false);
                Player2Hand.drawFullHand();
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
            buttonText.text = "Next";
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
            RoundButton.gameObject.SetActive(true);
        }
        else if(DM.throwMinigame)
        {
            Debug.Log("throw Minigame");
            RoundButton.gameObject.SetActive(false);
            ThrowButton.gameObject.SetActive(true);
            if(DM.getP1PlayedCard() != null && DM.getP1PlayedCard().getCard().CardName == "Throw")
            {
                Player2Hand.showHand();
                whoseThrowing = true;
            }
            else if(DM.getP2PlayedCard() != null && DM.getP2PlayedCard().getCard().CardName == "Throw")
            {
                Player1Hand.showHand();
                whoseThrowing = false;
            }

        }
        else if(DM.projectileMinigame)
        {
            Debug.Log("Target Practice");
            RoundButton.gameObject.SetActive(false);
            ProjectileButton.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("No Minigames");
            RoundButton.gameObject.SetActive(true);
        }
    }

    public void throwMinigame()
    {
        throwCanvas.SetActive(true);
        PlayedCards.SetActive(false);
    }

    public void ProjectileMinigame()
    {
        ProjectileCanvas.SetActive(true);
        PlayedCards.SetActive(false);
    }

    public void CheckforVictor()
    {
        
        if(Player1Hand.DeckisEmpty())
        {
            VictoryCanvas.SetActive(true);
            VictoryText.text = "Player 2 Wins!";
        }
        else if(Player2Hand.DeckisEmpty())
        {
            VictoryCanvas.SetActive(true);
            VictoryText.text = "Player 1 wins!";
        }
    }
}
