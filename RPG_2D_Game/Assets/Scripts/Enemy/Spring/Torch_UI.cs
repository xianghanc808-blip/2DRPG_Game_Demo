using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Torch_UI : MonoBehaviour
{
    public TMP_Text text_Damage;
    public Animator animDamage;
    public Animator animTips;


    private void OnEnable()
    {
        Torch_Health.OnDamage += PlayDamamge;
        SpringNPC_Talk.OnInteractedTips += InteractedTips;
        SpringNPC_Talk.OnEndInteractivedTips += EndInteractedTipes;
    }

    private void OnDisable()
    {
        Torch_Health.OnDamage -= PlayDamamge;
        SpringNPC_Talk.OnInteractedTips -= InteractedTips;
        SpringNPC_Talk.OnEndInteractivedTips -= EndInteractedTipes;
    }

    public void PlayDamamge(int damage, Vector3 enemyPos)
    {
        text_Damage.rectTransform.position = new Vector3(enemyPos.x + 150, enemyPos.y + 100, 0);
        text_Damage.text = "-" + damage;
        animDamage.Play("DamageHealth");
    }

    public void InteractedTips()
    {
        animTips.Play("Tip");
    }
    public void EndInteractedTipes()
    {
        animTips.Play("TipIdle");
    }
}
