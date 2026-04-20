using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Warrior_Health : MonoBehaviour
{

    public TMP_Text healthText;
    public Animator healthTextAnimator;
    public Animator playerAnimator;
    public Slider hpSlider;

    private void Start()
    {
        healthText.text = DatasManager.Instance.currentHealth + "\\" + DatasManager.Instance.maxHealth;
        hpSlider.maxValue =  DatasManager.Instance.maxHealth;
        hpSlider.value = DatasManager.Instance.currentHealth;
    }

    private void OnEnable()
    {
        DatasManager.OnHealthChange += HealthSliderChange;
        DatasManager.OnAddMashealth += AddMaxHealth;
    }

    private void OnDisable()
    {
        DatasManager.OnHealthChange -= HealthSliderChange;
        DatasManager.OnAddMashealth += AddMaxHealth;
    }


    public void HealthSliderChange(int currentHealth)
    {
        hpSlider.value = currentHealth;
    }

    public void AddMaxHealth(int maxHealth)
    {
        hpSlider.maxValue = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        DatasManager.Instance.currentHealth -= amount;

        healthTextAnimator.Play("HealthDance");

        EventSystem.current.SetSelectedGameObject(null);

        hpSlider.value = DatasManager.Instance.currentHealth;
        hpSlider.maxValue = DatasManager.Instance.maxHealth;

        if (DatasManager.Instance.currentHealth > DatasManager.Instance.maxHealth)
        {
            DatasManager.Instance.currentHealth = DatasManager.Instance.maxHealth;
        }
        else if (DatasManager.Instance.currentHealth <= 0)
        {
            playerAnimator.Play("Dead");
            //Destroy(this, 1f);
            StartCoroutine(ReloadSceneRoutine());
            //PlayerDie();
        }

        healthText.text = DatasManager.Instance.currentHealth + "\\" + DatasManager.Instance.maxHealth;
    }

    public void PlayerDie()
    {
        // 获取当前活动场景的名称并重新加载
        //Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("RPG_Spring");
    }

    private IEnumerator ReloadSceneRoutine()
    {
        Debug.Log("玩家已死亡，2秒后重启...");
        yield return new WaitForSeconds(1f);
        PlayerDie();
    }
}
