using UnityEngine;
using System.Collections;

public class LightIntensitySlider: MonoBehaviour
{

    public float intensity = 1f;

    [SerializeField] public Rect LightSlider;

    public Light[] MainLight;

    private float[] LightIntensity = { 0f, 0f, 0f, 0f };

    void Start()
    {

        for (int i = 0; i < MainLight.Length; i++)
        {
            LightIntensity[i] = MainLight[i].intensity;
        }
    }

    void Update()
    {

        for (int i = 0; i < MainLight.Length; i++)
        {

            MainLight[i].intensity = LightIntensity[i] * intensity;
        }

    }

    public void OnGUI()
    {
        intensity = GUI.HorizontalSlider(LightSlider, intensity, 0f, 2f);
    }
}