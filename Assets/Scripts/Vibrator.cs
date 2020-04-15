using UnityEngine;

public static class Vibrator
{
#if UNITY_ANDROID
    private static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    private static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    private static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
    private static AndroidJavaClass unityPlayer;
    private static AndroidJavaObject currentActivity;
    private static AndroidJavaObject vibrator;
#endif

    public static void Vibrate(long milliseconds = 100)
    {
        if (AudioManager.instance.isVibrating)
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
}