using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    [SerializeField] private TMP_Text         text;
    [SerializeField] private List<GameObject> credits;

    public void Show(float timeSurvived)
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowSequence(timeSurvived));
    }

    private static readonly WaitForSeconds WaitOneSecond     = new(1f);
    private static readonly WaitForSeconds WaitTypeInterval  = new(0.1f);
    private static readonly WaitForSeconds WaitBeforeCredits = new(0.2f);

    private IEnumerator ShowSequence(float timeSurvived)
    {
        string newText = "Pain experienced: " + (int)(timeSurvived * 17);

        yield return WaitOneSecond;

        text.text = "";

        while (text.text.Length < newText.Length)
        {
            yield return WaitTypeInterval;
            text.text += newText[text.text.Length];
        }

        yield return WaitBeforeCredits;

        foreach (GameObject credit in credits) credit.SetActive(true);
    }

    public void Restart() => SceneManager.LoadScene(0);
}
