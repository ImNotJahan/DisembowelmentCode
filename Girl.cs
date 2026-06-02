using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Girl : MonoBehaviour
{
    [SerializeField] private List<Texture2D> textures;
    [SerializeField] private Emotions        emotion = Emotions.Smile;
    [SerializeField] private Material        girlMaterial;

    [SerializeField] private EmittedText     textModel;
    [SerializeField] private List<string>    possibleWords;

    private List<string> recentlyUsedWords = new();

    [Header("Text Emission")]
    [SerializeField] private float emitInterval = 1.5f;
    [SerializeField] private float emitSpeed    = 1.5f;
    [SerializeField] private float spreadAngle  = 60f;  // degrees off-up spread

    void Start() => UpdateEmotion();

    public void StartEmittingText()
    {
        StartCoroutine(EmitLoop());
    }

    private IEnumerator EmitLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(emitInterval);
            SpawnWord();
        }
    }

    private void SpawnWord()
    {
        if (textModel == null || possibleWords == null || possibleWords.Count == 0) return;

        string word;

        // Pick a random word
        do word = possibleWords[Random.Range(0, possibleWords.Count)];
        while (recentlyUsedWords.Contains(word));

        recentlyUsedWords.Add(word);

        if (recentlyUsedWords.Count > 10) recentlyUsedWords.RemoveAt(0); 

        // Instantiate first so the full rotation chain (Girl × text local) is resolved,
        // then read the instance's own axes — transform.up is visual up on screen,
        // transform.forward is into/out of the screen face
        EmittedText instance = Instantiate(textModel, transform.position - Vector3.forward * 0.1f, textModel.transform.rotation, transform);
        instance.SetText(word);
        instance.transform.localRotation = textModel.transform.rotation;

        Vector3 baseDir = Random.value > 0.5f ? instance.transform.up : -instance.transform.up;
        Vector3 dir     = Quaternion.AngleAxis(Random.Range(-spreadAngle, spreadAngle), instance.transform.forward) * baseDir;

        instance.Launch(dir * emitSpeed);
    }

    private void UpdateEmotion()
    {
        girlMaterial.mainTexture = textures[(int) emotion];
    }

    public void SetEmotion(Emotions to)
    {
        emotion = to;
        UpdateEmotion();
    }

    public enum Emotions
    {
        Neutral,
        Joy,
        Anger,
        Sad,
        Fun,
        Smile,
        Panic,
        Surprise,
        Shy,
        ShySmile,
        ShySmile2,
        CrossEye,
        DarkSmile,
        DarkNeutral,
        Blank,
        Sharp,
        Smug,
        Sulky,
        Unamused,
        What
    }
}
