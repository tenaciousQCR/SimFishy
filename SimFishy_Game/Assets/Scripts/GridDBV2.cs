using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.Data.SQLite;
using System.IO;
using DBClasses;
using System;

public class GridDBV2 : MonoBehaviour
{
    
    public Text shoalIDText1;
    public Text shoalSizeText1;
    public Text shoalAgeText1;

    public Text shoalIDText2;
    public Text shoalSizeText2;
    public Text shoalAgeText2;

    public Text shoalIDText3;
    public Text shoalSizeText3;
    public Text shoalAgeText3;

    //vessel text
    public Text vesselIDText;
    public Text vesselFuelText;
    public Text vesselMaxFuelText;
    public Text vesselProfitText;
    public Text vesselCaughtText;
    public Text vesselQuotaText;
    public Text vesselStorageText;
    public Text vesselMaxStorageText;

    //port buttons
    public Button refuelBtn;
    public Button sellBtn;

    //menu buttons
    public Button resetBtn;
    public Button saveBtn;
    public Button moveBtn;

    //vessel action buttons
    public Button upBtn;
    public Button downBtn;
    public Button leftBtn;
    public Button rightBtn;
    public Button fishBtn;

    //panels
    public GameObject PortPanel;
    public GameObject VesselPanel;
    public GameObject MenuPanel;
    public GameObject ShoalPanel;

    public GameObject Grid;


    private GridManager GameGrid;

    //Shoals
    ShoalDB testShoal1 = new ShoalDB("GS001", 0, "0000"); // TODO: set the coords in the default field of the class
    ShoalDB testShoal2 = new ShoalDB("GS002", 0, "0000");
    ShoalDB testShoal3 = new ShoalDB("GS003", 0, "0000");
    VesselDB testBoat = new VesselDB("V01", "0000");

    // lists
    List<ShoalDB> shoalList = new List<ShoalDB>();
    List<Square> gridSquareList = new List<Square>();



    void Start()
    {
        //hide panels
        PortPanel.SetActive(false);
        VesselPanel.SetActive(false);
        MenuPanel.SetActive(false);
        ShoalPanel.SetActive(false);

    //grid set up
    GameGrid = Grid.GetComponent<GridManager>();

        //button functions
        sellBtn.onClick.AddListener(sellBtnOnClick);
        refuelBtn.onClick.AddListener(refuelBtnOnClick);

        resetBtn.onClick.AddListener(resetBtnOnClick);
        saveBtn.onClick.AddListener(saveBtnOnClick);
        moveBtn.onClick.AddListener(moveBtnOnClick);

        //vessel move functions
        upBtn.onClick.AddListener(delegate { MoveOnClick(1); });
        downBtn.onClick.AddListener(delegate { MoveOnClick(2); });
        leftBtn.onClick.AddListener(delegate { MoveOnClick(3); });
        rightBtn.onClick.AddListener(delegate { MoveOnClick(4); });

        fishBtn.onClick.AddListener(FishOnClick);

        //load game
        LoadGame();

        //updating shoal list
        shoalList.Add(testShoal1);
        shoalList.Add(testShoal2);
        shoalList.Add(testShoal3);

    }

    /////////////////////////////////////////////// game state functions //////////////////////////////////////// 
    public void LoadGame()
    {
        // Create database
        string connectionLink = "URI=file:" + Application.dataPath + "/" + "My_Test_Database";

        // Open connection
        SQLiteConnection connection = new SQLiteConnection(connectionLink);
        connection.Open();

        //pulls the saved data from the database
        testShoal1.UpdateClass(connection, "gridShoals");
        testShoal2.UpdateClass(connection, "gridShoals");
        testShoal3.UpdateClass(connection, "gridShoals");
        testBoat.UpdateClass(connection, "gridVessels");
        testBoat.PrintTest(connection, "gridVessels");

        connection.Close();
    }

    public void SaveGame()
    {
        // Create database
        string connectionLink = "URI=file:" + Application.dataPath + "/" + "My_Test_Database";
        // Open connection
        SQLiteConnection connection = new SQLiteConnection(connectionLink);
        connection.Open();

        //pulls the saved data from the database
        testShoal1.UpdateDB(connection, "gridShoals");
        testShoal2.UpdateDB(connection, "gridShoals");
        testShoal3.UpdateDB(connection, "gridShoals");
        testBoat.UpdateDB(connection, "gridVessels");
        //testBoat.PrintTest(connection, "gridVessels");

        connection.Close();
    }

    public void ResetGame()
    {
        testShoal1.Size = 400;
        testShoal2.Size = 10000;
        testShoal3.Size = 3000;

        testBoat.Storage = 0;
        testBoat.Profit = 0;
        testBoat.Fuel = testBoat.MaxFuel;
        testBoat.Caught = 0;

        SaveGame();
    }

    //////////////////////////////////////////// button functions ///////////////////////////////////////
    public void sellBtnOnClick()
    {
        //if the boat is holding fish
        if (testBoat.Storage > 0)
        {
            //if the weight in storage is less than the quota left
            if (testBoat.Storage <= testBoat.Quota-testBoat.Caught)
            {
                //pay the boat and store the fish
                testBoat.Profit += (testBoat.Storage) * 5;
                testBoat.Caught += testBoat.Storage;
                testBoat.Storage = 0;
            }
            //the player has caught too much
            else if (testBoat.Storage > testBoat.Quota - testBoat.Caught)
            {
                //see how much the player can sell
                int space = testBoat.Quota - testBoat.Caught;
                //give money
                testBoat.Profit += (space) * 5;
                //add to quota
                testBoat.Caught += space;
                //calculate losses
                int loss = testBoat.Storage-space;

                //empty storage
                testBoat.Storage = 0;
                //inform player of losses
                Debug.Log("cannont sell more than quota: you have lost " + loss + "kg of fish");
            }
            else
            {
                Debug.Log("error while attempting to sell fish");
            }
        }
    }

    public void refuelBtnOnClick()
    {
        if (testBoat.Fuel < testBoat.MaxFuel)
        {
            testBoat.Profit -= 20;
            testBoat.Fuel += 5;
        }
    }

    public void resetBtnOnClick()
    {
        ResetGame();
        SaveGame();
    }

    public void saveBtnOnClick()
    {
        SaveGame();
    }

    public void moveBtnOnClick()
    {
        foreach (var i in shoalList)
        {
            i.MoveRandom();
        }
    }

    //////////////////////////////////////////////////////////////////// movement, fishing and grid click //////////////////////////// ////////////////////////////
    public void FishOnClick()
    {
        foreach (var i in shoalList)
        {
            if (i.Coords == testBoat.Coords)
            {
                i.FishShoal(testBoat);
            }
        }
    }

    public void MoveOnClick(int direction)
    {
        //switch used for each direction
        switch (direction)
        {
            case 1:
                testBoat.MoveUp();
                break;
            case 2:
                testBoat.MoveDown();
                break;
            case 3:
                testBoat.MoveLeft();
                break;
            case 4:
                testBoat.MoveRight();
                break;
            default:
                break;
        }

    }


    /////////////////////////////////// other functions //////////////
    /*
    public void FishShoal(ShoalDB shoal)
    {
        if (shoal.Size >= 20)
        {
            shoal.Size -= 20;
            testBoat.Storage += 20;
        }
    }
    */
    public void UpdateVesselUI()
    {
        //updating vessels
        vesselIDText.text = "ID: " + testBoat.VesselID;
        vesselFuelText.text = "Fuel: " + (testBoat.Fuel).ToString();
        vesselMaxFuelText.text = "/ " + (testBoat.MaxFuel).ToString();
        vesselProfitText.text = "Profit: " + (testBoat.Profit).ToString();
        vesselCaughtText.text = "Quota (kg): " + (testBoat.Caught).ToString();
        vesselQuotaText.text = "/ " + (testBoat.Quota).ToString();
        vesselStorageText.text = "Storage (kg) " + (testBoat.Storage).ToString();
        vesselMaxStorageText.text = "/ " + (testBoat.MaxStorage).ToString();



        if (testBoat.isAtPort())
        {
            PortPanel.SetActive(true);
        }
        else
        {
            PortPanel.SetActive(false);
        }
    }

    public void UpdateShoalUI()
    {
        //updating shoals
        shoalIDText1.text = "ID: " + testShoal1.ShoalID;
        shoalSizeText1.text = "Size: " + (testShoal1.Size).ToString();
        shoalAgeText1.text = "Age: " + (testShoal1.Age).ToString();

        shoalIDText2.text = "ID: " + testShoal2.ShoalID;
        shoalSizeText2.text = "Size: " + (testShoal2.Size).ToString();
        shoalAgeText2.text = "Age: " + (testShoal2.Age).ToString();

        shoalIDText3.text = "ID: " + testShoal3.ShoalID;
        shoalSizeText3.text = "Size: " + (testShoal3.Size).ToString();
        shoalAgeText3.text = "Age: " + (testShoal3.Age).ToString();
    }

    public void UpdateUI()
    {
        UpdateVesselUI();
        UpdateShoalUI();
    }

    public void UpdateGrid()
    {
        //wipe the grid first
        foreach (var square in GameGrid.gridSquareList)
        {
            square.GridSquare.GetComponentInChildren<Text>().text = "";
        }

        //update boat location
        foreach (var square in GameGrid.gridSquareList)
        {
            if (testBoat.Coords == square.Coords)
            {
                square.GridSquare.GetComponentInChildren<Text>().text = (square.GridSquare.GetComponentInChildren<Text>().text) + testBoat.VesselID + " ";
            }
        }

        //update with shoal ids
        foreach (var i in shoalList)
        {
            foreach (var square in GameGrid.gridSquareList)
            {
                if (i.Coords == square.Coords)
                {
                    square.GridSquare.GetComponentInChildren<Text>().text = (square.GridSquare.GetComponentInChildren<Text>().text) + i.ShoalID + " ";
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        UpdateGrid();
    }
}
