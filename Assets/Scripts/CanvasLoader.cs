using System;
using System.Diagnostics;
using UnityEngine;

public class CanvasLoader : MonoBehaviour
{
    [SerializeField] private GameObject standaloneCanvasGameObject;
    [SerializeField] private GameObject androidCanvasGameObject;

    void Awake()
    {
        HideAllCanvas();
        LoadAppropriateCanvas();
    }

    private void LoadAppropriateCanvas()
    {
        LoadStandaloneCanvas();
        LoadAndroidCanvas();
    }

    private void HideAllCanvas()
    {
        standaloneCanvasGameObject.SetActive(false);
        androidCanvasGameObject.SetActive(false);
    }

    [Conditional("UNITY_STANDALONE"), Conditional("UNITY_WEBGL")]
    private void LoadStandaloneCanvas()
    {
        standaloneCanvasGameObject.SetActive(true);
        //Instantiate(standaloneCanvasGameObject, gameObject.transform);
    }

    [Conditional("UNITY_ANDROID")]
    private void LoadAndroidCanvas()
    {
        androidCanvasGameObject.SetActive(true);
        //Instantiate(androidCanvasGameObject, gameObject.transform);
    }
}
