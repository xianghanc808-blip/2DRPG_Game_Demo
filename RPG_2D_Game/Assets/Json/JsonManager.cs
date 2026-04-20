using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;
public enum JsonType
{
    JsonUtility,
    LitJson,
}
public class JsonManager
{
    private static JsonManager instance = new JsonManager();
    public static JsonManager Instance => instance;
    private JsonManager() { }

    public void SaveData(string name,object data,JsonType type = JsonType.LitJson)
    {
        string path = Application.persistentDataPath + "/" + name + ".json";
        string jsonStr = "";
        switch (type)
        {
            case JsonType.JsonUtility:
                jsonStr = UnityEngine.JsonUtility.ToJson(data);
                break;
            case JsonType.LitJson:
                jsonStr = JsonMapper.ToJson(data);
                break;
        }
        File.WriteAllText(path, jsonStr);
    }

    public T LoadData<T>(string name,JsonType type = JsonType.LitJson) where T:new()
    {
        string path = Application.streamingAssetsPath + "/" + name + ".json";

        if (!File.Exists(path))
            path = Application.persistentDataPath + "/" + name + ".json";

        if(!File.Exists(path))
            return new T();

        string jsonStr = File.ReadAllText(path);

        T data = default(T);

        switch (type)
        {
            case JsonType.JsonUtility:
                data = UnityEngine.JsonUtility.FromJson<T>(jsonStr);
                break;
            case JsonType.LitJson:
                data = JsonMapper.ToObject<T>(jsonStr);
                break;
        }
        return data;
    }
}
