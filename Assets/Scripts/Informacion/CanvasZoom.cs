using UnityEngine;
using UnityEngine.UI;

public class CanvasZoom : MonoBehaviour
{
    public Canvas canvas;
    public float zoomSpeed = 0.1f; // Velocidad del zoom
    public float maxZoom = 2.0f; // Máximo nivel de zoom
    public float minZoom = 0.5f; // Mínimo nivel de zoom

    private Vector2 lastTouchPos1;
    private Vector2 lastTouchPos2;
    private bool isZooming = false;

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (!isZooming)
            {
                lastTouchPos1 = touch1.position;
                lastTouchPos2 = touch2.position;
                isZooming = true;
            }
            else
            {
                // Calcula la distancia actual entre los toques
                float currentDistance = Vector2.Distance(touch1.position, touch2.position);
                float lastDistance = Vector2.Distance(lastTouchPos1, lastTouchPos2);

                // Calcula la diferencia en la distancia
                float distanceDelta = currentDistance - lastDistance;

                // Aplica el zoom
                Vector3 scale = canvas.transform.localScale;
                scale += Vector3.one * distanceDelta * zoomSpeed;
                scale.x = Mathf.Clamp(scale.x, minZoom, maxZoom);
                scale.y = Mathf.Clamp(scale.y, minZoom, maxZoom);
                canvas.transform.localScale = scale;

                // Actualiza las posiciones para el siguiente frame
                lastTouchPos1 = touch1.position;
                lastTouchPos2 = touch2.position;
            }
        }
        else
        {
            isZooming = false;
        }
    }
}
