using UnityEngine;

[RequireComponent(typeof(Outlinable))]
[RequireComponent(typeof(AudioSource))]
public class Pills : MonoBehaviour
{
    private bool usable = true;
    
    private void OnMouseDown()
    {
        if (!usable) return;

        GetComponent<Outlinable>() .OnMouseExit();
        GetComponent<Outlinable>() .enabled = false;
        GetComponent<AudioSource>().Play();

        Lucidity.MaxLucidity();

        usable = false;
    }
}
