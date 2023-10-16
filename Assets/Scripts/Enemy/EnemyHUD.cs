using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHUD : MonoBehaviour
{
    public Enemy enemy;
    public Slider hpSlider;
    public TextMeshProUGUI sliderValue;

    void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public void SetHUD()
    {
        hpSlider.maxValue = enemy.maxHP;
        hpSlider.value = enemy.maxHP;
        sliderValue.text = enemy.currentHP.ToString("0000");
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
        sliderValue.text = hp.ToString("0000");

        if (hpSlider.value <= 0)
            sliderValue.text = "0000";

    }
}
