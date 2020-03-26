using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SQLite;

namespace DBClasses
{
    public class VesselDB : MonoBehaviour
    {
        public string VesselID { get; set; }
        public int Storage { get; set; }
        public int Profit { get; set; }
        public string Coords { get; set; }
        public List<string> CoordList { get; set; }

        public VesselDB(string vesselID, int storage, int profit, string coords)
        {
            VesselID = vesselID;
            Storage = storage;
            Profit = profit;
            Coords = coords;
            CoordList = new List<string>() {
                "0211", "0311", "0411", "0511", "0611",
        "0110", "0210", "0310", "0410", "0510", "0610",
        "0109", "0209", "0309", "0409", "0509", "0609", "0709",
                "0208", "0308", "0408", "0508", "0608", "0708", "0808",
                "0207", "0307", "0407", "0507", "0607", "0707", "0807",
                "0206", "0306", "0406", "0506", "0606", "0706", "0806",
                "0205", "0305", "0405", "0505", "0605", "0705", "0805",
                        "0304", "0404", "0504", "0604", "0704", "0804",
                        "0303", "0403", "0503", "0603",
                                "0402", "0502", "0602",
                                "0401", "0501"};
    }

        public void SaveToDB(SQLiteConnection connection, string tablename)
        {
            SQLiteCommand cmnd = connection.CreateCommand();
            cmnd.CommandText = "INSERT INTO " + tablename
                + " (id, storage, profit, coords) VALUES ('" + VesselID + "', " + Storage + ", " + Profit + "', " + Coords + ")";
            cmnd.ExecuteNonQuery();
        }

        public void UpdateDB(SQLiteConnection connection, string tablename)
        {
            SQLiteCommand cmnd = connection.CreateCommand();
            cmnd.CommandText =
                "UPDATE " + tablename
                + " SET storage = " + Storage
                + ", profit = " + Profit
                + ", coords = '" + Coords
                + "' WHERE id = '" + VesselID + "'";
            cmnd.ExecuteNonQuery();
        }

        public void UpdateClass(SQLiteConnection connection, string tablename)
        {
            SQLiteCommand cmnd_read = connection.CreateCommand();
            IDataReader reader;
            string query = "SELECT * FROM " + tablename + " WHERE id='" + VesselID + "'";
            cmnd_read.CommandText = query;
            reader = cmnd_read.ExecuteReader();
            while (reader.Read())
            {
                //VesselID = reader[0].ToString();
                Storage = int.Parse(reader[1].ToString());
                Profit = int.Parse(reader[2].ToString());
                Coords = reader[3].ToString();
                //Debug.Log(Application.persistentDataPath);
            }
        }

        public void PrintTest(SQLiteConnection connection, string tablename)
        {
            // Read and print all values in table
            SQLiteCommand cmnd_read = connection.CreateCommand();
            IDataReader reader;
            string query = "SELECT * FROM " + tablename + " WHERE id='" + VesselID + "'";
            cmnd_read.CommandText = query;
            reader = cmnd_read.ExecuteReader();

            while (reader.Read())
            {
                Debug.Log("Vessel id: " + reader[0].ToString());
                Debug.Log("Vessel storage: " + reader[1].ToString());
                Debug.Log("Vessel profit: " + reader[2].ToString());
                Debug.Log("coords: " + reader[3].ToString());
                Debug.Log(Application.persistentDataPath);
            }
        }

        //movement
        public void MoveUp()
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
            //incase the coord is single digits 
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
                    Debug.Log("Boat: " + VesselID + " moved from " + Coords + " to " + newCoords);
                    Coords = newCoords;
                }
            }
            if (Coords == oldCoords)
            {
                Debug.Log("Boat: " + VesselID + " tried to move up but failed");
            }
        }

        public void MoveDown()
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
                    Debug.Log("Boat: " + VesselID + " moved from " + Coords + " to " + newCoords);
                    Coords = newCoords;
                }
            }
            if (Coords == oldCoords)
            {
                Debug.Log("Boat: " + VesselID + " tried to move down but failed");
            }
        }

        public void MoveLeft()
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
                    Debug.Log("Boat: " + VesselID + " moved from " + Coords + " to " + newCoords);
                    Coords = newCoords;
                }
            }
            if (Coords == oldCoords)
            {
                Debug.Log("Boat: " + VesselID + " tried to move left but failed");
            }
        }

        public void MoveRight()
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
                    Debug.Log("Boat: " + VesselID + " moved from " + Coords + " to " + newCoords);
                    Coords = newCoords;
                }
            }
            if (Coords == oldCoords)
            {
                Debug.Log("Boat: " + VesselID + " tried to move right but failed");
            }
        }


    }
}
    
