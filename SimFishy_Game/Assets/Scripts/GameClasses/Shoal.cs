using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameClasses
{
    public class Shoal : MonoBehaviour
    {
        public string ShaolID { get; set; }
        public int Size { get; set; }

        public Shoal(string shaolID, int size)
        {
            ShaolID = shaolID;
            Size = size;
        }
    }
}

