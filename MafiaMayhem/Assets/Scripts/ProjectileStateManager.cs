using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//since the projectile minigame has its own set of states, I felt a seperate manager was necessary
public class ProjectileStateManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI thisText;
    [SerializeField] private Image backdrop;
    [SerializeField] private Button nextButton;
    [SerializeField] private DecisionMaker DM;
    [SerializeField] private GameObject Target;
    [SerializeField] private GameObject SeparationLine;
    [SerializeField] private Button ValidTargetArea;
    [SerializeField] private Button InvalidTargetArea;
    [SerializeField] private GameObject throwingCard;
    private Camera Cam;
    private int currentStage = 0;

    private bool shooting;
    GameObject newTarget;

    [HideInInspector] public bool valid;
    private float chargePower;
    private GameObject spawnedCard;
    private Rigidbody2D RB;
    private bool shot = false;

    public void Start()
    {
        Cam = Camera.main;
        
    }

    public void FixedUpdate()
    {
        if((currentStage == 2 || currentStage == 4) && !backdrop.IsActive() && !shot)
        {
            if(Input.GetMouseButton(0))
            {
                chargePower += Time.deltaTime * 100;
                Debug.Log("Power is at: " + chargePower);
                if(!spawnedCard)
                {
                    spawnedCard = Instantiate(throwingCard, Input.mousePosition, Quaternion.identity);
                    spawnedCard.transform.parent = this.transform;
                    spawnedCard.transform.SetSiblingIndex(1);
                    RB = spawnedCard.GetComponent<Rigidbody2D>();
                }
            }
            else
            {
                shoot();
            }
        }
        else if (shot && RB)
        {

            RB.velocity = RB.velocity * 0.99f;
            Debug.Log(RB.velocity);
            if (RB.velocity.y < 1)
            {
                thisText.gameObject.SetActive(true);
                backdrop.gameObject.SetActive(true);
                nextButton.gameObject.SetActive(true);
            }
        }

    }

    public void nextState()
    {
        currentStage++;
        Debug.Log(currentStage);
        if(currentStage == 1) //player 1 is placing their crosshair
        {
            shooting = false;
            newTarget = null;
            InvalidTargetArea.gameObject.SetActive(true);
            ValidTargetArea.gameObject.SetActive(true);
            SeparationLine.SetActive(true);
        }
        else if (currentStage == 2) //player 2 is taking a shot
        {
            shooting = true;
            SeparationLine.SetActive(true);
        }
        else if(currentStage == 3) //player 2 is placing a crosshair
        {
            shooting = false;
            if(spawnedCard)
            {
                Destroy(spawnedCard);
                spawnedCard = null;
            }
            if(newTarget)
            {
                Destroy(newTarget);
                newTarget = null;
            }
            shot = false;
            SeparationLine.SetActive(true);
        }
        else if(currentStage == 4) //player 1 is taking a shot
        {
            shooting = true;
            SeparationLine.SetActive(true);

        }
        else if(currentStage == 5)
        {
            currentStage = 0;
            DM.projectileMinigame = false;
            if (spawnedCard)
            {
                Destroy(spawnedCard);
                spawnedCard = null;
            }
            if(newTarget)
            {
                Destroy(newTarget);
                newTarget = null;
            }
            shot = false;
            SeparationLine.SetActive(false);

        }
    }

    public void setValid()
    {
        Vector3 mousePosition = Input.mousePosition;
        if (newTarget == null)
        {
            newTarget = Instantiate(Target, mousePosition, Quaternion.identity);
            newTarget.transform.parent = this.transform;
            newTarget.transform.SetSiblingIndex(0);
            if (currentStage == 1)
            {
                thisText.text = "Player 2, shoot your shot!";
            }
            else
            {
                thisText.text = "Player 1, take aim!";
            }
            thisText.gameObject.SetActive(true);
            backdrop.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(true);
            InvalidTargetArea.gameObject.SetActive(false);
            ValidTargetArea.gameObject.SetActive(false);
        }
        Debug.Log("valid target area");
    }

    public void setinValid()
    {
        valid = false;
        Debug.Log("Invalid Target Area");
    }

    private void shoot()
    {
        if(RB)
        {
            RB.velocity = chargePower * Vector2.up;
            chargePower = 0;
            InvalidTargetArea.gameObject.SetActive(false);
            ValidTargetArea.gameObject.SetActive(false);
            shot = true;
        }
    }
}
