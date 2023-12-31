using System;
using System.Collections;
using System.Collections.Generic;
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
        //CreateGetLogin("juan");
    }

    string CreateJSONLogin(string tabla, string user, string name, string password, int edad, string sexo)
    {
        //Construye JSON para la petición REST         
        string json = $@"{{
            ""username"":""{username}"",
            ""password"":""{this.password}"",
            ""table"":""{tabla}"",
            ""data"": {{
                ""usuario"": ""{user}"",
                ""nombre"": ""{name}"",
                ""password"": ""{password}"",
                ""edad"": ""{edad}"",
                ""sexo"": ""{sexo}""
            }}
        }}";

        return json;
    }

    string CreateJSONSessions(
        string tabla,
        string user,
        string fechaInicioPartida,
        string fechaFinPartida,
        int puzlesPerdidos,
        int puzlesGanados,
        int energyUsed,
        int waterUsed,
        int totalEvents,
        int totalBonfires,
        int totalGold
        )
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
                ""puzlesPerdidos"": ""{puzlesPerdidos}"",
                ""PuzzlesGanados"": ""{puzlesGanados}"",
                ""EnergyUsed"": ""{energyUsed}"",
                ""WaterUsed"": ""{waterUsed}"",
                ""TotalEvents"": ""{totalEvents}"",
                ""TotalBonfires"": ""{totalBonfires}"",
                ""TotalGold"": ""{totalGold}""
            }}
        }}";

        return json;
    }
    
    string CreateJSONGetLogin(string tabla, string user)
    {
        //Construye JSON para la petición REST         
        string json = $@"{{
            ""username"":""{username}"",
            ""password"":""{this.password}"",
            ""table"":""{tabla}"",
            ""filter"": {{
                ""usuario"": ""{user}""
            }}
        }}";

        return json;
    }

    public void CreatePostLogin(string user, string name, string password, int edad, string sexo)
    {
        string data = CreateJSONLogin("WalkingTimeLogin", user, name, password, edad, sexo);

        StartCoroutine(SendPostRequest(data));
    }

    public void CreatePostSessions()
    {
        string data = CreateJSONSessions(
            "WalkingTimeSessions",
            GameManager.instance.nombreJugador,
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UserPerformance.instance.puzzlesLost,
            UserPerformance.instance.puzzlesWon,
            GameManager.instance.totalEnergyUsed,
            GameManager.instance.totalWaterUsed,
            GameManager.instance.totalEvents,
            GameManager.instance.bonfiresVisited,
            GameManager.instance.gold
            );

        StartCoroutine(SendPostRequest(data));
    }

    public void CreateGetLogin(string user)
    {
        string data = CreateJSONGetLogin("WalkingTimeLogin", user);

        StartCoroutine(SendGetRequest(data));
    }

    IEnumerator SendPostRequest(string data)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(uri+"insert", data, contentType))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)

            {
                print("Error: " + www.error);
            }
            else
            {
                print("Respuesta: " + www.downloadHandler.text);
                GameManager.instance.regCorrecto = true;
            }
        }
    }
    
    IEnumerator SendGetRequest(string data)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(uri+"get", data, contentType))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                print("Error: " + www.error);
                
            }
            else
            {
                print("Respuesta: " + www.downloadHandler.text);
                
                string configJson = www.downloadHandler.text;
                DataFields1 aux = JsonUtility.FromJson<DataFields1>(configJson);


                if(aux.data.Count!=0){
                    foreach (var i in aux.data)
                    {

                        // i.user = data;
                        // print(i.user);
                        if (GameManager.instance.password == i.password)
                        {
                            GameManager.instance.passCorrecta = true;
                            GameManager.instance.regCorrecto = false;
                        }
                        
                    }
                }
                else
                {
                    GameManager.instance.regCorrecto = true;
                    GameManager.instance.passCorrecta = false;
                }
            }
        }
    }

    void LoadCredentials()
    {
        // string configPath = "Assets/Data/config.json";
        TextAsset configJson = Resources.Load<TextAsset>("Data/config");

        if (configJson!=null)
        {
            
            var config = JsonUtility.FromJson<Credentials>(configJson.text);

            username = config.username;
            password = config.password;
            uri = config.uri;

            if (!Application.isEditor)
            {
                uri = config.uriCors;
            }
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
        public string uriCors;
    }
    
    [System.Serializable]
    private class DataFields
    {
        public string user;
        public string nombre;
        public string password;
        public int edad;
        public string sexo;
    }
    
    private class DataFields1
    {
        public string result;
        public List<DataFields> data;
    }
}