using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Square : MonoBehaviour
{
    public Button GridSquare { get; set; }
    public string Coords { get; set; }

    public Square(Button gridSquare, string coords)
    {
        GridSquare = gridSquare;
        Coords = coords;
    }
}
