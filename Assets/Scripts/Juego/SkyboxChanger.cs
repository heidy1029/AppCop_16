using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Light directionalLight; // Directional light to simulate the sun/moon
    public Material skyboxMaterial; // Skybox material

    [Range(0, 24)] public float timeOfDay = 12f; // Value representing the time of day (0 to 24)
    public float dayDuration = 120f; // Duration of a full day cycle in seconds

    private float timeMultiplier; // Multiplier to control the speed of the day cycle

    // Colors for the light at different times of the day
    public Color morningLightColor = new Color(1.0f, 0.95f, 0.8f);
    public Color afternoonLightColor = new Color(1.0f, 0.9f, 0.7f);
    public Color nightLightColor = new Color(0.2f, 0.2f, 0.35f);

    // Intensities of the light
    public float morningLightIntensity = 1.0f;
    public float afternoonLightIntensity = 1.2f;
    public float nightLightIntensity = 0.2f;

    // Skybox exposure for different times of the day
    public float morningExposure = 1.0f;
    public float afternoonExposure = 0.7f;
    public float nightExposure = 0.3f;

    // Transition speed for smooth changes
    public float transitionSpeed = 0.5f;

    private void Start()
    {
        // Calculate the time multiplier
        timeMultiplier = 24f / dayDuration;
    }

    private void Update()
    {
        // Advance the time of day
        timeOfDay += Time.deltaTime * timeMultiplier;
        if (timeOfDay >= 24f) timeOfDay = 0f;

        UpdateLightingAndSkybox();
    }

    // Update the lighting and skybox settings based on time of day
    private void UpdateLightingAndSkybox()
    {
        // Variables to store target values for interpolation
        Color targetLightColor = nightLightColor;
        float targetLightIntensity = nightLightIntensity;
        float targetExposure = nightExposure;

        // Morning (6 AM to 12 PM)
        if (timeOfDay >= 6f && timeOfDay < 12f)
        {
            targetLightColor = morningLightColor;
            targetLightIntensity = morningLightIntensity;
            targetExposure = morningExposure;
        }
        // Afternoon (12 PM to 6 PM)
        else if (timeOfDay >= 12f && timeOfDay < 18f)
        {
            targetLightColor = afternoonLightColor;
            targetLightIntensity = afternoonLightIntensity;
            targetExposure = afternoonExposure;
        }

        // Smoothly interpolate the light color, intensity, and skybox exposure
        directionalLight.color = Color.Lerp(directionalLight.color, targetLightColor, transitionSpeed * Time.deltaTime);
        directionalLight.intensity = Mathf.Lerp(directionalLight.intensity, targetLightIntensity, transitionSpeed * Time.deltaTime);
        float currentExposure = skyboxMaterial.GetFloat("_Exposure");
        skyboxMaterial.SetFloat("_Exposure", Mathf.Lerp(currentExposure, targetExposure, transitionSpeed * Time.deltaTime));

        // Rotate the sun/moon based on the time of day
        float sunRotation = (timeOfDay / 24f) * 360f - 90f;
        directionalLight.transform.rotation = Quaternion.Euler(sunRotation, 170f, 0f);
    }
}
