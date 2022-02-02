using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Roles")]
public class RoleInfo : ScriptableObject
{
    public string CardName;
    public string CharacterName;
    public string Description;
    public int Cooldown;

    public Sprite Icon;
}
