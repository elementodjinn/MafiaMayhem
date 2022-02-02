using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoleDisplay : MonoBehaviour
{
    public RoleInfo Role;

    public Image icon;

    public TMP_Text title;
    public TMP_Text Character;
    public TMP_Text Description;
    public TMP_Text Cooldown;

    // Start is called before the first frame update
    void Start()
    {
        title.text = Role.CardName;
        Character.text = Role.CharacterName;
        Description.text = Role.Description;
        Cooldown.text = Role.Cooldown.ToString();
        icon.sprite = Role.Icon;
    }
}
