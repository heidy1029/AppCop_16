using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material skyboxDay;
    public Material skyboxEvening;
    public Material skyboxNight;
    public float changeInterval = 10f; // Cambia de cielo cada 10 segundos (puedes ajustarlo)

    private float timer = 0f;
    public Light directionalLight;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer < changeInterval)
        {
            RenderSettings.skybox = skyboxDay;
            directionalLight.color = Color.white;
        }
        else if (timer < changeInterval * 2)
        {
            RenderSettings.skybox = skyboxEvening;
            directionalLight.color = new Color(1f, 0.5f, 0.2f); // Luz cálida para el atardecer
        }
        else if (timer < changeInterval * 3)
        {
            RenderSettings.skybox = skyboxNight;
            directionalLight.color = Color.blue;
        }
        else
        {
            timer = 0f;
        }
    }
}
