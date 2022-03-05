using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealProjectileDamage : MonoBehaviour
{
    [SerializeField] private ProjectileStateManager PM;
    [SerializeField] private DecisionMaker DM;

    public bool isTouching = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Throwing Card")
        {
            isTouching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Throwing Card")
        {
            isTouching = false;
            Debug.Log("Overshot");
        }
    }
}
