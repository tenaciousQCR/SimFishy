using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SQLite;

namespace DBClasses
{
    public class ShoalDB : MonoBehaviour
    {
        public string ShaolID { get; set; }
        public int Size { get; set; }
        public string Coords { get; set; }

        public ShoalDB(string shaolID, int size, string coords)
        {
            ShaolID = shaolID;
            Size = size;
            Coords = coords;
        }

        public void SaveToDB(SQLiteConnection connection, string tablename)
        {
            SQLiteCommand cmnd = connection.CreateCommand();
            cmnd.CommandText = "INSERT INTO " + tablename + " (id, size, coords) VALUES ('" + ShaolID + "', " + Size + "', " + Coords + ")";
            cmnd.ExecuteNonQuery();
        }

        public void UpdateDB(SQLiteConnection connection, string tablename)
        {
            SQLiteCommand cmnd = connection.CreateCommand();
            cmnd.CommandText = "UPDATE " + tablename 
                + " SET size = " + Size 
                + ", coords = '" + Coords 
                + "' WHERE id = '" + ShaolID + "'";
            cmnd.ExecuteNonQuery();
        }

        public void UpdateClass(SQLiteConnection connection, string tablename)
        {
            SQLiteCommand cmnd_read = connection.CreateCommand();
            IDataReader reader;
            string query = "SELECT * FROM " + tablename + " WHERE id='" + ShaolID + "'";
            cmnd_read.CommandText = query;
            reader = cmnd_read.ExecuteReader();
            while (reader.Read())
            {
                //ShaolID = reader[0].ToString();
                Size = int.Parse(reader[1].ToString());
                Coords = reader[2].ToString();
                //Debug.Log(Application.persistentDataPath);
            }
        }

        public void PrintTest(SQLiteConnection connection, string tablename)
        {
            // Read and print all values in table
            SQLiteCommand cmnd_read = connection.CreateCommand();
            IDataReader reader;
            string query = "SELECT * FROM " + tablename + " WHERE id='" + ShaolID + "'";
            cmnd_read.CommandText = query;
            reader = cmnd_read.ExecuteReader();

            while (reader.Read())
            {
                Debug.Log("id: " + reader[0].ToString());
                Debug.Log("size: " + reader[1].ToString());
                Debug.Log("coords: " + reader[2].ToString());
                Debug.Log(Application.persistentDataPath);
            }
        }
    }
}


