using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextDialogue : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Dialogue dialogueNext;
    public Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        StopAllCoroutines();
        sentences = new Queue<string>();

        StartCoroutine(AnimationWait());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting conversation with " + dialogue.nameChar);

        nameText.text = dialogue.nameChar;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        Debug.Log(sentence);
    }

    IEnumerator AnimationWait()
    {
        yield return new WaitForSeconds(2);

        FindObjectOfType<DialogueManager>().StartDialogue(dialogueNext);
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        FindObjectOfType<DialogueManager>().EndDialogue();

        Debug.Log("End of conversation.");
    }
}
