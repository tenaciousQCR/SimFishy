using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.Data.SQLite;
using System.IO;
using DBClasses;
using System;

public class StateManager : MonoBehaviour
{
    public Text timeText;
    public Text dateText;

    public float timeStart { get; set; } = 0;
    public float day = 1;
    public float month = 1;
    public float year = 2020;

    public Button dayBtn;
    public Button monthBtn;


    // on start 
    void Start()
    {
        //set timer
        timeText.text = Mathf.Round(timeStart).ToString() + ":00";
        //set date
        dateText.text = day + "/" + month + "/" + year;

        //enable buttons
        dayBtn.onClick.AddListener(NextDay);
        monthBtn.onClick.AddListener(NextMonth);
    }

    public void NextMonth()
    {
        timeStart = 0;
        timeText.text = Mathf.Round(timeStart).ToString() + ":00";

        // -------------- new month/year code
        switch (month)
        {
            //if it is december
            case 12:
                //set next year
                day = 1;
                month = 1;
                year++;
                dateText.text = day + "/" + month + "/" + year;
                break;
            case 2:
                //next month
                day = 1;
                month++;
                //set display
                dateText.text = day + "/" + month + "/" + year;
                break;
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
                day = 1;
                month++;
                //set display
                dateText.text = day + "/" + month + "/" + year;
                break;
            case 4:
            case 6:
            case 9:
            case 11:
                day = 1;
                month++;
                dateText.text = day + "/" + month + "/" + year;
                break;
            default:
                Debug.Log("error with NextMonth() method");
                break;
        }
    }
    //methods
    public void NextDay()
    {
        timeStart = 0;
        timeText.text = Mathf.Round(timeStart).ToString() + ":00";

        // -------------- new month/year code
        switch (month)
        {
            //if it is december
            case 12:
                //either next day or next month and year
                if (day < 31)
                {
                    day++;
                }
                else if (day == 31)
                {
                    day = 1;
                    month = 1;
                    year++;
                }
                //set display
                dateText.text = day + "/" + month + "/" + year;
                break;
            case 2:
                //either next day or next month and year
                if (day < 28)
                {
                    day++;
                }
                else if (day == 28)
                {
                    day = 1;
                    month++;
                }
                //set display
                dateText.text = day + "/" + month + "/" + year;
                break;
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
                //either next day or next month
                if (day < 31)
                {
                    day++;
                }
                else if (day == 31)
                {
                    day = 1;
                    month++;
                }
                //set display
                dateText.text = day + "/" + month + "/" + year;
                break;
            case 4:
            case 6:
            case 9:
            case 11:
                //either next day or next month and year
                if (day < 30)
                {
                    day++;
                }
                else if (day == 30)
                {
                    day = 1;
                    month++;
                }
                //set display
                dateText.text = day + "/" + month + "/" + year;
                break;
            default:
                Debug.Log("error with NextDay() method");
                break;
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        //if the time is less than 11pm
        if (timeStart < 23)
        {
            timeStart += Time.deltaTime;
            timeText.text = Mathf.Round(timeStart).ToString() + ":00";

        }
        //if it is 23, go back to 00 to start a new day
        else
        {
            NextDay();
        }

    }


}
