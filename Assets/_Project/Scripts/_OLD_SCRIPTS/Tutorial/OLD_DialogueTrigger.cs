using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLD_DialogueTrigger : MonoBehaviour
{
    public OLD_Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<OLD_DialogueManager>().StartDialogue(dialogue);
    }
}