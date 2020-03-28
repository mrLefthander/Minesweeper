#if UNITY_ANDROID

using UnityEngine;

public class CameraControlsAndroid
{
    float cameraSpeed = 0.02f;
    float[] cameraZoomBounds = new float[] { 8.4375f, 33.75f };
    float[] cameraXBounds = new float[] { -10f, 10f };
    float[] cameraYBounds = new float[] { -15f, 15f };

    public void Pan(Vector2 touchDeltaPosition)
    {
        Camera.main.transform.Translate(-touchDeltaPosition.x * cameraSpeed, -touchDeltaPosition.y * cameraSpeed, 0);
        Camera.main.transform.position = new Vector3(
             Mathf.Clamp(Camera.main.transform.position.x, cameraXBounds[0], cameraXBounds[1]),
             Mathf.Clamp(Camera.main.transform.position.y, cameraYBounds[0], cameraYBounds[1]), -10);
    }

    public void Zoom(float difference)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - difference * cameraSpeed, cameraZoomBounds[0], cameraZoomBounds[1]);
    }
}
#endif