using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationLoaderDesktop : MonoBehaviour, ILocalizationLoader
{
    public event Action<string> OnDataLoaded;

    public void Load(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        
        if (File.Exists(filePath))
        {
            OnDataLoaded?.Invoke(File.ReadAllText(filePath));
        }
        else
        {
            Debug.LogWarning("Cannot find " + fileName + " localization file");
        }
    }
}
