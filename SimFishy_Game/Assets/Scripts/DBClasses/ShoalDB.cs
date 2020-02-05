using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SQLite;
using GameClasses;

namespace DBClasses
{
    public class ShoalDB : MonoBehaviour
    {
        public string ShoalDBID { get; set; }
        public Shoal Shoal { get; set; }

        public ShoalDB(string shoalDBID, Shoal shoal)
        {
            ShoalDBID = shoalDBID;
            Shoal = shoal;
        }

        void UpdateDB(SQLiteConnection connection) 
        {

        }

        void UpdateClass(SQLiteConnection connection)
        {

        }


        void InsertTest(SQLiteConnection connection, int id, int val, string tablename)
        {
            // Insert values in table
            SQLiteCommand cmnd = connection.CreateCommand();
            cmnd.CommandText = "INSERT INTO " + tablename + " (id, val) VALUES (" + id + ", " + val + ")";
            cmnd.ExecuteNonQuery();
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}


