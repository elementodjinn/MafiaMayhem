using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMesh numberDis;
    public DeckInfo deck;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeDisplay()
    {
        numberDis.text = deck.getCount().ToString();
    }
}
