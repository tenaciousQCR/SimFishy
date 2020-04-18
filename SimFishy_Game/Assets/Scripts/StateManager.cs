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
    //date and time display
    public Text timeText;
    public Text dateText;

    //skip time buttons 
    public Button dayBtn;
    public Button monthBtn;

    // text fields
    public Text monthTitleText;
    public Text minTempText;
    public Text maxTempText;
    public Text monthDescText;

    //panels
    public GameObject MonthPopup;

    // values that are saved

    //date and time default new game values
    public float timeStart { get; set; } = 0;
    public float day { get; set; } = 1;
    public float month { get; set; } = 1;
    public float year { get; set; } = 2020;

    // GAME STATE VALUES
    public string username { get; set; } = "TEST";
    public float profit { get; set; } = 0;
    public float loss { get; set; } = 0;
    public float fishPop { get; set; } = 0;



    // on start 
    void Start()
    {

        // disable popups by default
        MonthPopup.SetActive(false);

        //set timer
        timeText.text = Mathf.Round(timeStart).ToString() + ":00";
        //set date
        dateText.text = day + "/" + month + "/" + year;

        // use month popup by default atm -------- TODO add check for if its the month start etc
        ActivateMonthPopup();

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
                ActivateMonthPopup(); // ---------------- replace with year
                dateText.text = day + "/" + month + "/" + year;
                break;
            case 2:
                //next month
                day = 1;
                month++;
                ActivateMonthPopup();
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
                ActivateMonthPopup();
                //set display
                dateText.text = day + "/" + month + "/" + year;
                break;
            case 4:
            case 6:
            case 9:
            case 11:
                day = 1;
                month++;
                ActivateMonthPopup();
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
                    ActivateMonthPopup(); // ------------ replace with year popup
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
                    ActivateMonthPopup();
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
                    ActivateMonthPopup();
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
                    ActivateMonthPopup();
                }
                //set display
                dateText.text = day + "/" + month + "/" + year;
                break;
            default:
                Debug.Log("error with NextDay() method");
                break;
        }
    }

    public void ActivateMonthPopup()
    {

        //switch statement checks the month and edits the text accordingly
        switch (month)
        {
            // January
            case 1:
                //set the text on the popup
                monthTitleText.text = "January";
                minTempText.text = (6.5).ToString();
                maxTempText.text = (8.6).ToString();
                break;

            // February
            case 2:
                //set the text on the popup
                monthTitleText.text = "February";
                minTempText.text = (6.1).ToString();
                maxTempText.text = (7.9).ToString();
                break;

            // March
            case 3:
                //set the text on the popup
                monthTitleText.text = "March";
                minTempText.text = (5.9).ToString();
                maxTempText.text = (7.9).ToString();
                break;

            // April
            case 4:
                //set the text on the popup
                monthTitleText.text = "April";
                minTempText.text = (6.8).ToString();
                maxTempText.text = (8.9).ToString();
                break;

            // May
            case 5:
                //set the text on the popup
                monthTitleText.text = "May";
                minTempText.text = (8.1).ToString();
                maxTempText.text = (10.4).ToString();
                break;

            // June
            case 6:
                //set the text on the popup
                monthTitleText.text = "June";
                minTempText.text = (9.8).ToString();
                maxTempText.text = (13.3).ToString();
                break;

            // July
            case 7:
                //set the text on the popup
                monthTitleText.text = "July";
                minTempText.text = (11.2).ToString();
                maxTempText.text = (15.5).ToString();
                break;

            // August
            case 8:
                //set the text on the popup
                monthTitleText.text = "August";
                minTempText.text = (12).ToString();
                maxTempText.text = (15).ToString();
                break;

            // September
            case 9:
                //set the text on the popup
                monthTitleText.text = "September";
                minTempText.text = (12.1).ToString();
                maxTempText.text = (14.2).ToString();
                break;

            // October
            case 10:
                //set the text on the popup
                monthTitleText.text = "October";
                minTempText.text = (11.3).ToString();
                maxTempText.text = (13.3).ToString();
                break;

            // November
            case 11:
                //set the text on the popup
                monthTitleText.text = "November";
                minTempText.text = (10).ToString();
                maxTempText.text = (12.2).ToString();
                break;

            // December
            case 12:
                //set the text on the popup
                monthTitleText.text = "December";
                minTempText.text = (7.6).ToString();
                maxTempText.text = (10.7).ToString();
                break;
        }
        //display popup
        MonthPopup.SetActive(true);
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
