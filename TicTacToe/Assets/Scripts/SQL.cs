using Mono.Data.Sqlite;
using System.Data;
using System.IO;
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

        GetRating();

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

    public void GetRating()
    {
        using (SqliteConnection connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM players;";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.Log(reader["name"]);
                    }
                }
            }
        }

    }
}
