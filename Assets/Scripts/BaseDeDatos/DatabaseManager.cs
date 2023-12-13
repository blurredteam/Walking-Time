using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance;
    
    string username;
    string password;
    string uri;
    string contentType = "application/json";

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
        LoadCredentials();
        CreatePostLogin("juan","ee","11");
    }

    string CreateJSONLogin(string tabla, string user, string name, string password)
    {
        //Construye JSON para la petición REST         
        string json = $@"{{
            ""username"":""{username}"",
            ""password"":""{this.password}"",
            ""table"":""{tabla}"",
            ""data"": {{
                ""usuario"": ""{user}"",
                ""nombre"": ""{name}"",
                ""password"": ""{password}""
            }}
        }}";

        return json;
    }
    
    string CreateJSONSessions(string tabla, string user, string fechaInicioPartida, string fechaFinPartida, int puzlesPerdidos)
    {
        //Construye JSON para la petición REST         
        string json = $@"{{
            ""username"":""{username}"",
            ""password"":""{this.password}"",
            ""table"":""{tabla}"",
            ""data"": {{
                ""usuario"": ""{user}"",
                ""fechaFinPartida"": ""{fechaFinPartida}"",
                ""fechaInicioPartida"": ""{fechaInicioPartida}"",
                ""puzlesPerdidos"": ""{puzlesPerdidos}""
            }}
        }}";

        return json;
    }

    public void CreatePostLogin(string user, string name, string password)
    {
        string data = CreateJSONLogin("WalkingTimeLogin", user, name, password);

        StartCoroutine(SendPostRequest(data));
        
    }

    public void CreatePostSessions()
    {
        string data = CreateJSONSessions(
            "WalkingTimeSessions",
            "juan",
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 
            UserPerformance.instance.puzzlesLost);
            

        StartCoroutine(SendPostRequest(data));
    }
    
    IEnumerator SendPostRequest(string data)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(uri, data, contentType))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                print("Error: " + www.error);
            }
            else
            {
                print("Respuesta: " + www.downloadHandler.text);
            }
        }
    }

    void LoadCredentials()
    {
        string configPath = "Assets/Data/config.json";

        if (File.Exists(configPath))
        {
            string configJson = File.ReadAllText(configPath);
            var config = JsonUtility.FromJson<Credentials>(configJson);

            username = config.username;
            password = config.password;
            uri = config.uri;
        }
        else
        {
            Debug.LogError("Config file not found!");
        }
    }

    [System.Serializable]
    private class Credentials
    {
        public string username;
        public string password;
        public string uri;
    }
}
