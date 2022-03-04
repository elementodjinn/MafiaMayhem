using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    private Camera Cam;
    private int currentStage = 0;

    private bool shooting;
    GameObject newTarget;

    [HideInInspector] public bool valid;

    public void Start()
    {
        Cam = Camera.main;
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
            InvalidTargetArea.gameObject.SetActive(false);
            ValidTargetArea.gameObject.SetActive(false);
            SeparationLine.SetActive(false);
        }
        else if(currentStage == 3) //player 2 is placing a crosshair
        {
            shooting = false;
            newTarget = null;
            InvalidTargetArea.gameObject.SetActive(true);
            ValidTargetArea.gameObject.SetActive(true);
            SeparationLine.SetActive(true);
        }
        else if(currentStage == 4) //player 1 is taking a shot
        {
            shooting = true;
            currentStage = 0;
            DM.projectileMinigame = false;
            InvalidTargetArea.gameObject.SetActive(false);
            ValidTargetArea.gameObject.SetActive(false);
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
        }
        Debug.Log("valid target area");
    }

    public void setinValid()
    {
        valid = false;
        Debug.Log("Invalid Target Area");
    }
}
