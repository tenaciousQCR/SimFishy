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

        void UpdateDB(SQLiteConnection connection, string tablename) 
        {
            SQLiteCommand cmnd = connection.CreateCommand();
            cmnd.CommandText = "INSERT INTO " + tablename + " (id, size) VALUES (" + Shoal.ShaolID + ", " + Shoal.Size + ")";
            cmnd.ExecuteNonQuery();

        }

        void UpdateClass(SQLiteConnection connection, string tablename)
        {
            SQLiteCommand cmnd_read = connection.CreateCommand();
            IDataReader reader;
            string query = "SELECT * FROM" + tablename + "WHERE id='"+ Shoal.ShaolID +"'";
            cmnd_read.CommandText = query;
            reader = cmnd_read.ExecuteReader();
            Shoal.ShaolID = reader[0].ToString();
            Shoal.Size = int.Parse(reader[1].ToString());
        }


        void PrintTest(SQLiteConnection connection, string tablename)
        {
            // Read and print all values in table
            SQLiteCommand cmnd_read = connection.CreateCommand();
            IDataReader reader;
            string query = "SELECT * FROM" + tablename + "WHERE id='" + Shoal.ShaolID + "'";
            cmnd_read.CommandText = query;
            reader = cmnd_read.ExecuteReader();

            while (reader.Read())
            {
                Debug.Log("id: " + reader[0].ToString());
                Debug.Log("size: " + reader[1].ToString());
                Debug.Log(Application.persistentDataPath);
            }
        }
    }
}


