#if UNITY_ANDROID

using UnityEngine;

public static class Vibrator
{
    private static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    private static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    private static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

    public static void Vibrate(long milliseconds = 100)
    {
        if (Application.isEditor)
        {
            Handheld.Vibrate();
        }
        else
        {
            vibrator.Call("vibrate", milliseconds);
        }
    }
}
#endif