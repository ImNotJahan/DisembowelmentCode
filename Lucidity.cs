using System;
using UnityEngine;

public class Lucidity : MonoBehaviour
{
    [SerializeField] private Material screenMaterial;

    [SerializeField] private float minVisualSnow = 0.02f;
    [SerializeField] private float maxVisualSnow = 1.00f;

    [SerializeField] private float minVignette = 1.00f;
    [SerializeField] private float maxVignette = 0.05f;

    [SerializeField] private float lucidityRate = 0.05f;
    [SerializeField] private float intensityExponent = 2f;

    [SerializeField] private Death death;
    
    private const float lucidityBump = 0.05f;

    private static float lucidity = 1f;

    private bool losingLucidity = false;

    private float timeAlive = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() 
    {
        lucidity = 1f;

        UpdateMaterial();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

        if (!losingLucidity) return;
        
        lucidity -= lucidityRate * Time.deltaTime;

        if (lucidity <= 0) Die();

        timeAlive += Time.deltaTime;

        UpdateMaterial();
    }

    private void Die()
    {
        losingLucidity = false;
        lucidity       = 1;

        UpdateMaterial();

        death.Show(timeAlive);
    }

    private void UpdateMaterial()
    {
        screenMaterial.SetFloat ("_Intensity", Mathf.Lerp(maxVisualSnow, minVisualSnow, Mathf.Pow(lucidity, intensityExponent)));
        screenMaterial.SetVector("_Edge",      new(0, Mathf.Lerp(maxVignette, minVignette, lucidity), 0, 0));
    }

    public static void BumpLucidity()
    {
        lucidity = Math.Min(1, lucidity + lucidityBump);
    }

    public static void MaxLucidity() => lucidity = 1f;

    public void StartLosingLucidity() 
    {
        losingLucidity = true;

        // also reset rotation so player is looking at girl
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
}
