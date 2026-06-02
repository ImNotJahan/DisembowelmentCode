using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Outlinable))]
[RequireComponent(typeof(MeshRenderer))]
public class Knife : MonoBehaviour
{
    [SerializeField] private Material    bloodyMaterial;
    [SerializeField] private GameObject  blur;
    [SerializeField] private GameObject  blink;
    [SerializeField] private AudioSource sound;
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioClip   dyingMusic;

    [SerializeField] private Girl     girl;
    [SerializeField] private Lucidity lucidity;

    private static readonly WaitForSeconds WaitAfterStab = new(5f);

    private bool canStab = false;

    void Start()
    {
        GetComponent<Outlinable>().enabled = false;
    }

    public void MarkCanStab() 
    {
        canStab = true;

        GetComponent<Outlinable>().enabled = true;
    }

    private void OnMouseDown()
    {
        if (!canStab) return;
        StartCoroutine(StabSequence());
    }

    private IEnumerator StabSequence()
    {
        GetComponent<Outlinable>().enabled = false;

        GetComponent<MeshRenderer>().SetMaterials(new() { bloodyMaterial });

        sound.Play();

        canStab = false;

        blur .SetActive(true);
        blink.SetActive(true);

        music.mute = true;

        yield return WaitAfterStab;

        music.mute = false;
        music.clip = dyingMusic;

        music.Play();

        blink.SetActive(false);

        girl    .StartEmittingText  ();
        lucidity.StartLosingLucidity();
    }
}
