using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public TextMeshProUGUI lines;
    public string[] words;
    public float textSpeed;
    private int index;


    // Start is called before the first frame update
    void Start()
    {
        lines.text = string.Empty;
        StartCutscene();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (lines.text == words[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                lines.text = words[index];
            }
        }
    }

    private void StartCutscene()
    {
        index = 0;
    }

    private IEnumerator TypeLine()
    {
        foreach (char c in words[index].ToCharArray())
        {
            lines.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void NextLine()
    {
        if (index < words.Length - 1)
        {
            index++;
            lines.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
