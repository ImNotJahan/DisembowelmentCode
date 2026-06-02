using TMPro;
using UnityEngine;

public class EmittedText : MonoBehaviour
{
    private const float MAX_LIFETIME = 5f;

    [SerializeField] private TMP_Text text;
    [SerializeField] private Color    grayedColor = new(0.45f, 0.45f, 0.45f, 1f);

    [SerializeField] private float cursorFlashRate = 0.5f;

    private Vector3 velocity;
    private string  word;
    private int     progress      = 0;
    private float   timeAlive     = 0;
    private float   cursorTimer   = 0;
    private bool    cursorVisible = false;

    public void SetText(string to)
    {
        word = to;
        RefreshDisplay();
    }

    public void Launch(Vector3 velocity) => this.velocity = velocity;

    void Update()
    {
        timeAlive += Time.deltaTime;

        if (timeAlive >= MAX_LIFETIME) Destroy(gameObject);

        transform.position += velocity * Time.deltaTime;

        cursorTimer += Time.deltaTime;
        if (cursorTimer >= cursorFlashRate)
        {
            cursorTimer  -= cursorFlashRate;
            cursorVisible = !cursorVisible;
            RefreshDisplay();
        }

        HandleTyping();
    }

    private void HandleTyping()
    {
        if (string.IsNullOrEmpty(word) || progress >= word.Length) return;

        foreach (char typed in Input.inputString)
        {
            if (progress >= word.Length) break;

            if (char.ToLower(typed) == char.ToLower(word[progress]))
            {
                progress++;
                RefreshDisplay();

                if (progress >= word.Length)
                {
                    CompleteWord();
                    return;
                }
            }
        }
    }

    private void CompleteWord()
    {
        Destroy(gameObject);

        Lucidity.BumpLucidity();
    }

    private void RefreshDisplay()
    {
        if (string.IsNullOrEmpty(word)) return;

        string grayed  = progress > 0 ? $"<color=#808080>{word[..progress]}</color>" : "";

        if (progress >= word.Length)
        {
            text.text = grayed;
            return;
        }

        string  cursorChar = cursorVisible ? word[progress].ToString() : "<color=#536878>" + word[progress] + "</color>";
        string  rest       = progress + 1 < word.Length ? word[(progress + 1)..] : "";
        text.text          = grayed + cursorChar + rest;
    }
}
