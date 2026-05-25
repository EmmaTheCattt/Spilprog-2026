using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class SQL : MonoBehaviour
{
    private const string playerInfo = "playerInfo.db";
    private string databasePath;

    private void Awake()
    {
        databasePath = Path.Combine(Application.persistentDataPath, playerInfo);
        Debug.Log(databasePath);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateDB();

        CreatePlayer("Magnus C.", "MinKode", 2883);
        CreatePlayer("Camilla", "MinKode", 1000);
        CreatePlayer("Valdemar", "MinKode", 4);
        CreatePlayer("Rose", "MinKode", 600);
        CreatePlayer("Emma", "MinKode", 3);
        CreatePlayer("Alex", "MinKode", 1000);
    }

   
    private string ConnectionString => $"URI=file:{databasePath}";


    public void CreateDB()
    {
        Directory.CreateDirectory(Application.persistentDataPath);

        using (SqliteConnection connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS players (name VARCHAR(30), code VARCHAR(30), rating REAL)";
                command.ExecuteNonQuery();
            }
        }
    }

    public void CreatePlayer(string name, string code, float rating)
    {
        using (SqliteConnection connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            using (SqliteCommand command = connection.CreateCommand())
            {

                string text0 = "SELECT EXISTS(SELECT * FROM players WHERE name = '{0}' AND code = '{1}')";
                command.CommandText = string.Format(text0, name, code);
                using (IDataReader reader0 = command.ExecuteReader())
                {


                    while (reader0.Read())
                    {
                        var exist = reader0.GetValue(0);
                        string exist2 = exist.ToString();

                        if (exist2 == "1")
                        {
                            Debug.Log("player already exists");
                            return;
                        }



                    }


                }
                string text = "INSERT INTO players (name, code, rating) VALUES ('{0}', '{1}', '{2}')";
                command.CommandText = string.Format(text, name, code, rating);

                command.ExecuteNonQuery();
            }
        }
    }

    public float GetRating(string name, string code)
    {
        using (SqliteConnection connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            using (SqliteCommand command = connection.CreateCommand())
            {

                string text0 = "SELECT EXISTS(SELECT * FROM players WHERE name = '{0}' AND code = '{1}')";
                command.CommandText = string.Format (text0, name, code);
                using (IDataReader reader0 = command.ExecuteReader())
                {


                    while (reader0.Read())
                    {
                        var exist = reader0.GetValue(0);
                        string exist2 = exist.ToString();
                       
                        if (exist2 == "0")
                        {
                            Debug.Log("player does not exist");
                            return 0;
                        }
                     


                    }
               

                }
                string text = "SELECT rating FROM players WHERE name = '{0}' AND code = '{1}'";
                command.CommandText = string.Format(text, name, code);
                using (IDataReader reader = command.ExecuteReader())
                {


                    while (reader.Read())
                    {

                        float currentRating = (float)reader["rating"];
                        Debug.Log(currentRating);
                        return currentRating;

                    }

                }



            }
        }
        return 0;

    }

    public void UpdateRating(string name, float newRating)
    {
        using (SqliteConnection connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            using (SqliteCommand command = connection.CreateCommand())
            {
                string text = "UPDATE players SET rating = '{1}' WHERE name = '{0}'";
                command.CommandText = string.Format(text, name, newRating);

                command.ExecuteNonQuery();
            }
        }

    }
}
