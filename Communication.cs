using System;
using TMPro;
using UnityEngine;

public class Communication : MonoBehaviour
{
    [TextArea(10, 100)]
    [SerializeField] private string script = "";

    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private TMP_Text optionText;
    [SerializeField] private Girl     girl;
    [SerializeField] private Knife    knife;

    private string[] scriptLines;

    private int scriptPosition = 0;

    void Start()
    {
        scriptLines = script.Split('\n');

        ProgressScript();
    }

    public void ProgressScript()
    {
        while (scriptLines.Length > scriptPosition)
        {
            string line = scriptLines[scriptPosition];

            string text      = line[1..];
            char   operation = line[0  ];

            switch (operation)
            {
                case '?':
                    optionText.text = text;
                    break;
                
                case '>':
                    dialogText.text = text;
                    break;
                
                case '@':
                    girl.SetEmotion((Girl.Emotions) Enum.Parse(typeof(Girl.Emotions), text));
                    break;
            }

            scriptPosition++;

            if (operation == '?') return;
        }
        
        gameObject.SetActive(false);

        knife.MarkCanStab();
    }
}
