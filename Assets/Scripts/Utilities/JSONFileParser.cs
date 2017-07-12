using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class JSONFileParser<TClass, TType> : MonoSingleton<TClass> where TClass : MonoBehaviour
{
    private string JSONFilesList;

    private List<TType> objects;

    public TType[] ConvertJSONtoClass(string path)
    {
        string[] files = Directory.GetFiles(path, "*.json");

        TType[] convertedJSON = new TType[files.Length];

        for (int i = 0; i < files.Length; i++)
        {
            convertedJSON[i] = LoadObjectFromFiles(files[i]);
        }

        return convertedJSON;
    }

    private TType LoadObjectFromFiles(string path)
    {
        string fileContent = File.ReadAllText(path);
        return JsonUtility.FromJson<TType>(fileContent);
    }

    public IEnumerator ConvertJSONtoClassForWebGL(string folderPath, string listFileName, Action<TType[]> onSuccess)
    {
        string path = Path.Combine(folderPath, listFileName);
        
        yield return StartCoroutine(loadStreamingAsset(path, OnJSONFileListLoadSuccess));
        
        JSONFilesList = JSONFilesList.Replace("\"", "");
        JSONFilesList = JSONFilesList.Replace("[", "");
        JSONFilesList = JSONFilesList.Replace("]", "");
        JSONFilesList = Regex.Replace(JSONFilesList, @"\s+", string.Empty);

        string[] filesName = JSONFilesList.Split(',');

        objects = new List<TType>();

        foreach (string filename in filesName)
        {
            yield return StartCoroutine(loadStreamingAsset(Path.Combine(folderPath, filename), OnJSONFileLoadSuccess));
        }
        
        onSuccess(objects.ToArray());
    }
    
    IEnumerator loadStreamingAsset(string filePath, Action<string> onSuccess)
    {
        string result;
        if (filePath.Contains("://") || filePath.Contains(":///"))
        {
            WWW www = new WWW(filePath);
            yield return www;
            result = www.text;
        }
        else
        {
            result = File.ReadAllText(filePath);
        }

        onSuccess(result);
    }

    private void OnJSONFileListLoadSuccess(string fileText)
    {
        JSONFilesList = fileText;
    }

    private void OnJSONFileLoadSuccess(string fileText)
    {
        objects.Add(JsonUtility.FromJson<TType>(fileText));
    }
}
