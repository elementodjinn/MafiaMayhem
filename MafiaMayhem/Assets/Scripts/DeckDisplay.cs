using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeckDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text numberDis;
    public DeckInfo deck;
    
    public void ChangeDisplay()
    {
        numberDis.text = deck.getCount().ToString();
    }
}
