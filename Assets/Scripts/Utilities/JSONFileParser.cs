using System.IO;
using UnityEngine;

public class JSONFileParser<T> : Singleton<T> where T : class
{
    public T[] ConvertJSONtoClass<T>(string path)
    {
        string[] files = Directory.GetFiles(path, "*.json");

        T[] convertedJSON = new T[files.Length];

        for (int i = 0; i < files.Length; i++)
        {
            convertedJSON[i] = LoadObjectFromFiles<T>(files[i]);
        }

        return convertedJSON;
    }

    private T LoadObjectFromFiles<T>(string path)
    {
        string fileContent = File.ReadAllText(path);

        return JsonUtility.FromJson<T>(fileContent);
    }
}
