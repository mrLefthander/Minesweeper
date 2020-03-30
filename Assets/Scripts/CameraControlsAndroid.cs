using UnityEngine;

public class CameraControlsAndroid
{
    private readonly float cameraSpeed = 0.015f;
    float[] cameraZoomBounds = new float[2];
    float[] cameraXBounds = new float[2];
    float[] cameraYBounds = new float[2];
    Vector2Int mapSize;

    public CameraControlsAndroid()
    {
        mapSize = GameValuesController.instance.GetMapDimensions();
        cameraZoomBounds = new float[] { 8.4375f, mapSize.x * 2f + 2f };
    }

    public void Pan(Vector2 touchDeltaPosition)
    {
        CalculatePanBounds();

        Camera.main.transform.Translate(-touchDeltaPosition.x * cameraSpeed, -touchDeltaPosition.y * cameraSpeed, 0);

        ApplyBounds();
    }

    private void ApplyBounds()
    {
        Camera.main.transform.position = new Vector3(
             Mathf.Clamp(Camera.main.transform.position.x, cameraXBounds[0], cameraXBounds[1]),
             Mathf.Clamp(Camera.main.transform.position.y, cameraYBounds[0], cameraYBounds[1]), 
             Camera.main.transform.position.z);
    }

    private void CalculatePanBounds()
    {
        float horizontalBound = (mapSize.x * 2f + 2f - Camera.main.orthographicSize) / 2;
        cameraXBounds[0] = -horizontalBound;
        cameraXBounds[1] = horizontalBound;

        if (Camera.main.orthographicSize < mapSize.y + 2)
        {
            float verticalBound = (mapSize.y - Camera.main.orthographicSize + 2);
            cameraYBounds[0] = -verticalBound;
            cameraYBounds[1] = verticalBound;
        }
        else
        {
            cameraYBounds[0] = 0f;
            cameraYBounds[1] = 0f;
        }
    }

    public void Zoom(float difference)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - difference * cameraSpeed, cameraZoomBounds[0], cameraZoomBounds[1]);
    }
}