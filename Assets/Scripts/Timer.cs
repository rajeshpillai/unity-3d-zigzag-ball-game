using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI txtTimer;


    [HideInInspector]
    float timer = 0.0f;
    private void Awake()
    {
        txtTimer.text = "00:00:00";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        if (GameManager.instance.gameOver == true) return;
        if  (GameManager.instance.gameState != 1) return;
        
        timer += Time.deltaTime;

        float hours = Mathf.Floor(timer / 3600);
        float minutes = Mathf.Floor(timer / 60);
        float seconds = Mathf.RoundToInt(timer % 60);
        

        string hh = hours.ToString();
        string mm = minutes.ToString();
        string ss = seconds.ToString();


        if (hours < 10)
        {
            hh = "0" + hours.ToString();
        }

        if (minutes < 10)
        {
            mm = "0" + minutes.ToString();
        }
        if (seconds < 10)
        {
            ss = "0" + Mathf.RoundToInt(seconds).ToString();
        }

        txtTimer.text =  hh + ":" + mm + ":" + ss;

        //DateTime time = DateTime.Now;
        //string hour = LeadingZero(time.Hour);
        //string minute = LeadingZero(time.Minute);
        //string second = LeadingZero(time.Second);
        //txtTimer.text = hour + ":" + minute + ":" + second;


        //int seconds = (int)timer % 60;
        //txtTimer.text = seconds.ToString();

        //System.TimeSpan calc = System.TimeSpan.FromSeconds(time);
        //prettyTime = string.Format("{0}:{1}:{2}", calc.Hours, calc.Minutes, calc.Seconds);
    }

    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }
}
