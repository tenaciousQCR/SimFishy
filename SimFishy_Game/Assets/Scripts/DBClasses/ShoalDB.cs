using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SQLite;

namespace DBClasses
{
    public class ShoalDB : MonoBehaviour
    {
        public string ShoalID { get; set; }
        public int Size { get; set; }
        public string Coords { get; set; }
        public List<string> CoordList { get; set; }

        public ShoalDB(string shoalID, int size, string coords)
        {
            ShoalID = shoalID;
            Size = size;
            Coords = coords;
            CoordList = new List<string>() { "0101", "0102", "0103", "0201", "0202", "0203", "0301", "0302", "0303" };
        }

        public void SaveToDB(SQLiteConnection connection, string tablename)
        {
            SQLiteCommand cmnd = connection.CreateCommand();
            cmnd.CommandText = "INSERT INTO " + tablename + " (id, size, coords) VALUES ('" + ShoalID + "', " + Size + "', " + Coords + ")";
            cmnd.ExecuteNonQuery();
        }

        public void UpdateDB(SQLiteConnection connection, string tablename)
        {
            SQLiteCommand cmnd = connection.CreateCommand();
            cmnd.CommandText = "UPDATE " + tablename 
                + " SET size = " + Size 
                + ", coords = '" + Coords 
                + "' WHERE id = '" + ShoalID + "'";
            cmnd.ExecuteNonQuery();
        }

        public void UpdateClass(SQLiteConnection connection, string tablename)
        {
            SQLiteCommand cmnd_read = connection.CreateCommand();
            IDataReader reader;
            string query = "SELECT * FROM " + tablename + " WHERE id='" + ShoalID + "'";
            cmnd_read.CommandText = query;
            reader = cmnd_read.ExecuteReader();
            while (reader.Read())
            {
                //ShoalID = reader[0].ToString();
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
            string query = "SELECT * FROM " + tablename + " WHERE id='" + ShoalID + "'";
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

        public void FishShoal(Vessel boat)
        {
            if (Size >= 20)
            {
                Size -= 20;
                boat.Storage += 20;
            }
        }

        // moving functions
        public void MoveShoalUp()
        {
            //spilt the coordinate sting into an array of characters
            char[] coordArray = Coords.ToCharArray();
            //split the coordinates into x and y
            string x = coordArray[0].ToString() + coordArray[1].ToString();
            string y = coordArray[2].ToString() + coordArray[3].ToString();
            string oldCoords = x + y;
            //turn y into an int and increse its value before making it a string again for comparison
            int yint = int.Parse(y);
            yint++;
            string newy = yint.ToString();
            if (yint < 10)
            {
                newy = "0" + yint.ToString();
            }

            foreach (var i in CoordList)
            {
                char[] coordArray2 = i.ToCharArray();
                string x2 = coordArray2[0].ToString() + coordArray2[1].ToString();
                string y2 = coordArray2[2].ToString() + coordArray2[3].ToString();
                string newCoords = x2 + y2;
                //if the new coordinates are on the list of possible coordinates, the shoal shall move
                if (x == x2 && newy == y2)
                {
                    Debug.Log("shoal: " + ShoalID + " moved from " + Coords + " to " + newCoords);
                    Coords = newCoords;
                }
            }
            if (Coords == oldCoords)
            {
                Debug.Log("shoal: " + ShoalID + " tried to move up but failed");
            }
        }

        public void MoveShoalDown()
        {
            //spilt the coordinate sting into an array of characters
            char[] coordArray = Coords.ToCharArray();
            //split the coordinates into x and y
            string x = coordArray[0].ToString() + coordArray[1].ToString();
            string y = coordArray[2].ToString() + coordArray[3].ToString();
            string oldCoords = x + y;
            //turn y into an int and increse its value before making it a string again for comparison
            int yint = int.Parse(y);
            yint--;
            string newy = yint.ToString();
            if (yint < 10)
            {
                newy = "0" + yint.ToString();
            }

            foreach (var i in CoordList)
            {
                char[] coordArray2 = i.ToCharArray();
                string x2 = coordArray2[0].ToString() + coordArray2[1].ToString();
                string y2 = coordArray2[2].ToString() + coordArray2[3].ToString();
                string newCoords = x2 + y2;
                //if the new coordinates are on the list of possible coordinates, the shoal shall move
                if (x == x2 && newy == y2)
                {
                    Debug.Log("shoal: " + ShoalID + " moved from " + Coords + " to " + newCoords);
                    Coords = newCoords;
                }
            }
            if (Coords == oldCoords)
            {
                Debug.Log("shoal: " + ShoalID + " tried to move down but failed");
            }
        }

        public void MoveShoalLeft()
        {
            //spilt the coordinate sting into an array of characters
            char[] coordArray = Coords.ToCharArray();
            //split the coordinates into x and y
            string x = coordArray[0].ToString() + coordArray[1].ToString();
            string y = coordArray[2].ToString() + coordArray[3].ToString();
            string oldCoords = x + y;
            //turn x into an int and decrease its value before making it a string again for comparison
            int xint = int.Parse(x);
            xint--;
            string newx = xint.ToString();
            if (xint < 10)
            {
                newx = "0" + xint.ToString();
            }

            foreach (var i in CoordList)
            {
                char[] coordArray2 = i.ToCharArray();
                string x2 = coordArray2[0].ToString() + coordArray2[1].ToString();
                string y2 = coordArray2[2].ToString() + coordArray2[3].ToString();
                string newCoords = x2 + y2;
                //if the new coordinates are on the list of possible coordinates, the shoal shall move
                if (newx == x2 && y == y2)
                {
                    Debug.Log("shoal: " + ShoalID + " moved from " + Coords + " to " + newCoords);
                    Coords = newCoords;
                }
            }
            if (Coords == oldCoords)
            {
                Debug.Log("shoal: " + ShoalID + " tried to move left but failed");
            }
        }

        public void MoveShoalRight()
        {
            //spilt the coordinate sting into an array of characters
            char[] coordArray = Coords.ToCharArray();
            //split the coordinates into x and y
            string x = coordArray[0].ToString() + coordArray[1].ToString();
            string y = coordArray[2].ToString() + coordArray[3].ToString();
            string oldCoords = x + y;
            //turn x into an int and increse its value before making it a string again for comparison
            int xint = int.Parse(x);
            xint++;
            string newx = xint.ToString();
            if (xint < 10)
            {
                newx = "0" + xint.ToString();
            }
            

            foreach (var i in CoordList)
            {
                char[] coordArray2 = i.ToCharArray();
                string x2 = coordArray2[0].ToString() + coordArray2[1].ToString();
                string y2 = coordArray2[2].ToString() + coordArray2[3].ToString();
                string newCoords = x2 + y2;
                //if the new coordinates are on the list of possible coordinates, the shoal shall move
                if (newx == x2 && y == y2)
                {
                    Debug.Log("shoal: " + ShoalID + " moved from " + Coords + " to " + newCoords);
                    Coords = newCoords;
                }
            }
            if(Coords == oldCoords)
            {
                Debug.Log("shoal: " + ShoalID + " tried to move right but failed");
            }
        }

        public void MoveRandom()
        {
            int randint = Random.Range(1, 5);

            switch (randint) {
                case 1:
                    MoveShoalUp();
                    break;
                case 2:
                    MoveShoalDown();
                    break;
                case 3:
                    MoveShoalLeft();
                    break;
                case 4:
                    MoveShoalRight();
                    break;
                default:
                    Debug.Log("random error encountered for " + ShoalID);
                    break;
            }
        }
    }
}


