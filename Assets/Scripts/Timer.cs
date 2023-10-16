using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    float currentTime = 0f;
    float startingTime = 10f;
    // Start is called before the first frame update
    void Awake()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        timerText.text = currentTime.ToString("0.0");

        if(currentTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
