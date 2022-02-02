using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Action")]
public class CardInfo : ScriptableObject
{
    public string CardName;
    public string Description;
    public Sprite Icon;

    public CardInfo[] beats;

    public CardInfo[] LosesTo;

    public CardInfo[] Minigame;

}
