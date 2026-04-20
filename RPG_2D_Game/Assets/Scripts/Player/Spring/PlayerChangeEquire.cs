using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChangeEquire : MonoBehaviour
{
    public Player_Bow playerBow;
    public Warrior_Combat playerCombate;
    public Sprite WarriorSprite;
    public Sprite ArcherSprite;
    public Image image;
    private bool isWarriorOrArcher;
    public CanvasGroup warriorCanvas;
    public CanvasGroup archerCanvas;
    public TMP_Text namePlayer;

    public void ChangePlayerEquipment()
    {
        SoundMusics.Instance.PlaySound("switch-a");
        playerBow.enabled = !playerBow.enabled;
        if (playerBow.enabled)
        {
            archerCanvas.alpha = 1;
            warriorCanvas.alpha = 0;
            namePlayer.text = "…‰ ÷";
        }
        else
        {
            archerCanvas.alpha = 0;
            warriorCanvas.alpha = 1;
            namePlayer.text = "’Ω ø";
        }
        playerCombate.enabled = !playerCombate.enabled;
        image.sprite = isWarriorOrArcher ? WarriorSprite : ArcherSprite;
        isWarriorOrArcher = !isWarriorOrArcher;
    }

  
}
