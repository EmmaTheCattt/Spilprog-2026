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

        CreatePlayer("Magnus Carlsen", "MinKode", 2883);
        CreatePlayer("Camilla", "MinKode", 1000);

    }

    // Update is called once per frame
    void Update()
    {
        
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
                command.CommandText = "CREATE TABLE IF NOT EXISTS players (name VARCHAR(30), code VARCHAR(30), rating INT)";
                command.ExecuteNonQuery();
            }
        }
    }

    public void CreatePlayer(string name, string code, int rating)
    {
        using (SqliteConnection connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            using (SqliteCommand command = connection.CreateCommand())
            {
                string text = "INSERT INTO players (name, code, rating) VALUES ('{0}', '{1}', '{2}')";
                command.CommandText = string.Format(text, name, code, rating);

                command.ExecuteNonQuery();
            }
        }
    }

    public void GetRating(string name, string code)
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
                            return;
                        }
                     


                    }
               

                }
                string text = "SELECT rating FROM players WHERE name = '{0}' AND code = '{1}'";
                command.CommandText = string.Format(text, name, code);
                using (IDataReader reader = command.ExecuteReader())
                {


                    while (reader.Read())
                    {

                        int currentRating = (int)reader["rating"];
                        Debug.Log(currentRating);

                    }

                }







            }
        }

    }

    public void UpdateRating(string name, string code, int newRating)
    {
        using (SqliteConnection connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            using (SqliteCommand command = connection.CreateCommand())
            {
                string text = "UPDATE players SET rating = '{2}' WHERE name = '{0}' AND code = '{1}'";
                command.CommandText = string.Format(text, name, code, newRating);

                command.ExecuteNonQuery();
            }
        }

    }
}
