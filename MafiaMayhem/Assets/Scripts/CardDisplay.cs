using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public CardInfo card;

    public Image icon;

    public TMP_Text name;
    public TMP_Text Description;

    // Start is called before the first frame update
    void Start()
    {
        name.text = card.CardName;
        Description.text = card.Description;
        icon.sprite = card.Icon;
    }

    public void setNewCard(CardInfo newCard)
    {
        card = newCard;
        name.text = newCard.CardName;
        Description.text = newCard.Description;
        icon.sprite = newCard.Icon;
    }

    public CardInfo getCard()
    {
        return card;
    }
}
