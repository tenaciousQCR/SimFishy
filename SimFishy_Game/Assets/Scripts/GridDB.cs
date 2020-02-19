using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.Data.SQLite;
using System.IO;
using GameClasses;
using DBClasses;

public class GridDB : MonoBehaviour
{
    // Start is called before the first frame update
    public Text shoalIDText1;
    public Text shoalSizeText1;

    public Text shoalIDText2;
    public Text shoalSizeText2;

    public Text shoalIDText3;
    public Text shoalSizeText3;

    public Text vesselIDText;
    public Text vesselStorageText;
    public Text vesselProfitText;

    //Shoals
    Shoal testShoal1 = new Shoal("S01", 0);
    Shoal testShoal2 = new Shoal("S02", 0);
    Shoal testShoal3 = new Shoal("S03", 0);
    Vessel testBoat = new Vessel("V01", 0, 0);

    //ArrayList coordList = new ArrayList[0101, 0102, 0103, 0201, 0202, 0203, 0301, 0302, 0303];


    void Start()
    {
        
    }


    public void UpdateUI()
    {
        //updating shoals
        shoalIDText1.text = "ID: " + testShoal1.ShaolID;
        shoalSizeText1.text = "Size: " + (testShoal1.Size).ToString();

        shoalIDText2.text = "ID: " + testShoal2.ShaolID;
        shoalSizeText2.text = "Size: " + (testShoal2.Size).ToString();

        shoalIDText3.text = "ID: " + testShoal3.ShaolID;
        shoalSizeText3.text = "Size: " + (testShoal3.Size).ToString();

        //updating vessels
        vesselIDText.text = "ID: " + testBoat.VesselID;
        vesselStorageText.text = "Storage: " + (testBoat.Storage).ToString();
        vesselProfitText.text = "Profit: " + (testBoat.Profit).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }
}
