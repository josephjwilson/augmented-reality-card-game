using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    //Name and Sprite
    string name;
    public Sprite sprite;
    public TextMeshProUGUI text;

    //Slider and values 
    public Slider hpSlider;
    public TextMeshProUGUI sliderValue;
    private int maxHP;
    public int currentHP;

    public void SetHUD()
    {
        //Sanity check for no username
        if (PlayerData.username == null)
            name = "Error";
        else
            name = PlayerData.username;

        //Changes health based on difficulty
        switch (PlayerData.difficultyIndex) 
        {
            case 0:
                maxHP = 5000;
                break;
            case 1:
                maxHP = 4000;
                break;
            case 2:
                maxHP = 3500;
                break;
            default:
                maxHP = 4000;
                break;
        }

        currentHP = maxHP;
        text.text = name;
        hpSlider.maxValue = maxHP;
        hpSlider.value = currentHP;
        sliderValue.text = currentHP.ToString("0000");
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
        sliderValue.text = hp.ToString("0000");

        if (hpSlider.value <= 0)
            sliderValue.text = "0000";
    }

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;
        SetHP(currentHP);
        if (currentHP <= 0)
            return true;
        else
            return false;
    }
}
