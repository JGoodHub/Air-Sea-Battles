using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ConfigService : Singleton<ConfigService>
{
    [System.Serializable]
    public struct ConfigResponce
    {
        public string id;
        public int time_limit;
        public int points_per_plane;
        public int default_high_score;

        public ConfigResponce(string id, int time_limit, int points_per_plane, int default_high_score)
        {
            this.id = id;
            this.time_limit = time_limit;
            this.points_per_plane = points_per_plane;
            this.default_high_score = default_high_score;
        }
    }

    public delegate void ConfigResponceRecieved(ConfigService sender, ConfigResponce responce);
    public event ConfigResponceRecieved OnConfigResponceRecieved;

    public ConfigData config;

    protected override void Awake()
    {
        base.Awake();
        if (dying)
            return;

        transform.parent = null;
        DontDestroyOnLoad(gameObject);

        config.Initalise();

        OnConfigResponceRecieved += (sender, responce) =>
        {
            config.id = responce.id;
            config.timeLimit = responce.time_limit;
            config.pointPerPlane = responce.points_per_plane;
            config.defaultHighScore = responce.default_high_score;
        };

        //Send the request slightly delayed to give other scripts a change to subscribe to its callback
        Invoke("RequestConfigData", 0.05f);
    }

    public void RequestConfigData()
    {
        StartCoroutine(SendConfigRequestCoroutine());
    }

    private IEnumerator SendConfigRequestCoroutine()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://content.gamefuel.info/api/client_programming_test/air_battle_v1/content/config/config");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);

            //Return a response containing default values
            ConfigResponce responce = new ConfigResponce("config", 30, 1, 100);
            OnConfigResponceRecieved?.Invoke(this, responce);
        }
        else
        {
            ConfigResponce responce = JsonUtility.FromJson<ConfigResponce>(www.downloadHandler.text);
            OnConfigResponceRecieved?.Invoke(this, responce);
        }
    }
}
