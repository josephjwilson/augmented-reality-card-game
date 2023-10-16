using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public TextMeshProUGUI healthPointText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenseText;

    void Update()
    {
        healthPointText.text = GetComponentInParent<PlayerStats>().healthPoints.ToString();
        attackText.text = GetComponentInParent<PlayerStats>().attack.ToString();
        defenseText.text = GetComponentInParent<PlayerStats>().defense.ToString();
    }
}
