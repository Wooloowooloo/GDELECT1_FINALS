using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OLD_DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator animNuki, animDB, nameAnim, dAnim;

    public OLD_Dialogue dialogueVar;
    public OLD_Tutorial tutorial;

    public Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();

        animNuki.SetBool("IsOpen", true);
        animDB.SetBool("IsHere", true);
        nameAnim.SetBool("IsEnd", true);
        dAnim.SetBool("HasEnd", true);

        StartCoroutine(AnimationWait());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue (OLD_Dialogue dialogue)
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

    public void DisplayNextSentence ()
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

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    IEnumerator AnimationWait ()
    {
        yield return new WaitForSeconds(2);

        FindObjectOfType<OLD_DialogueManager>().StartDialogue(dialogueVar);
    }    

    public void EndDialogue ()
    {
        FindObjectOfType<OLD_Tutorial>().MoveOn();

        animNuki.SetBool("IsOpen", false);
        animDB.SetBool("IsHere", false);
        nameAnim.SetBool("IsEnd", false);
        dAnim.SetBool("HasEnd", false);

        Debug.Log("End of conversation.");
    }
}