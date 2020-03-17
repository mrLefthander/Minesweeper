using System.Diagnostics;
using UnityEngine;

public class CanvasLoader : MonoBehaviour
{
    [SerializeField] private GameObject standaloneCanvasGameObject;
    [SerializeField] private GameObject androidCanvasGameObject;
    [SerializeField] private Camera UICamera;

    void Awake()
    {
        LoadStandaloneCanvas();
        LoadAndroidCanvas();
        GetComponentInChildren<Canvas>().worldCamera = UICamera;
    }

    [Conditional("UNITY_STANDALONE"), Conditional("UNITY_WEBGL")]
    private void LoadStandaloneCanvas()
    {
        Instantiate(standaloneCanvasGameObject, gameObject.transform);
    }

    [Conditional("UNITY_ANDROID")]
    private void LoadAndroidCanvas()
    {
        Instantiate(androidCanvasGameObject, gameObject.transform);
    }
}
