using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System;

public class LocalizationLoaderWebGL: MonoBehaviour, ILocalizationLoader
{
    public event Action<string> OnDataLoaded;

    public void Load(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        StartCoroutine(LocalizationGetRequest(filePath));
    }

    public IEnumerator LocalizationGetRequest(string filePath)
    {
        UnityWebRequest request = UnityWebRequest.Get(filePath);
        UnityWebRequestAsyncOperation async = request.SendWebRequest();
        while (!async.isDone) { yield return null; }

        if (request.isNetworkError || request.isHttpError)
        {
            OnDataLoaded?.Invoke(request.error);
        }
        else
        {
            OnDataLoaded?.Invoke(request.downloadHandler.text);
        }
    }
}
