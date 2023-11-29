using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // If using standard UI Text
using TMPro; // If using TextMeshPro

public class TextBubble : MonoBehaviour
{
    //public GameObject textBubblePanel; // Assign in Inspector
    //public TextMesh dialogueText; // Assign in Inspector if using standard UI Text
    //public TextMesh dialogueText; // Assign in Inspector if using TextMeshPro

    private Queue<string> sentences; // Queue to hold sentences.
    private float timeAlive = 5.0f;
    private float aliveTimer;
    private bool wasEnabled;
    private bool enable;
    private bool fade;
    private float fadeTime = 2.5f;
    private float alpha;

    public GameObject panel;
    private Text txt;
    private Image background;

    void Start()
    {
        //sentences = new Queue<string>();
        //textBubblePanel.SetActive(false); // Hide at start

        aliveTimer = 0f;
        wasEnabled = false;
        enable = false;
        fade = false;
        alpha = 1f;

        txt = GetComponentInChildren<Text>();
        background = GetComponentInChildren<Image>();
    }

    private void OnEnable()
    {
        enable = true;
    }

    private void Update()
    {
        aliveTimer += Time.deltaTime;
        if (enable)
        {
            print(aliveTimer);
            if (!wasEnabled)
            {
                aliveTimer = 0f;
            }

            wasEnabled = true;

            if (aliveTimer >= timeAlive)
            {
                enable = false;
                fade = true;
            }
        }
        else if (fade)
        {
            wasEnabled = false;
            alpha = 1 - Mathf.Clamp01((aliveTimer - timeAlive) / fadeTime);
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, alpha);
            background.color = new Color(background.color.r, background.color.g, background.color.b,
                alpha);
            if (alpha < 0.001)
            {
                fade = false;
                gameObject.SetActive(false);
            }
        }
    }

    //public void StartDialogue(string[] dialogueLines)
    //{
    //    sentences.Clear();

    //    foreach (var sentence in dialogueLines)
    //    {
    //        sentences.Enqueue(sentence);
    //    }

    //    DisplayNextSentence();
    //}

    //public void DisplayNextSentence()
    //{
    //    if (sentences.Count == 0)
    //    {
    //        EndDialogue();
    //        return;
    //    }

    //    string sentence = sentences.Dequeue();
    //    dialogueText.text = sentence;
    //    textBubblePanel.SetActive(true);
    //}

    //void EndDialogue()
    //{
    //    textBubblePanel.SetActive(false);
    //    // Handle the end of the dialogue, like enabling player movement again
    //}

    //// Call this method when player interacts with a character/NPC
    //public void Interact()
    //{
    //    if (textBubblePanel.activeInHierarchy)
    //    {
    //        DisplayNextSentence();
    //    }
    //    else
    //    {
    //        // Start the dialogue
    //        StartDialogue(new string[] { "Hello!", "How are you?", "Welcome to the world of Pokémon!" });
    //    }
    //}
}
