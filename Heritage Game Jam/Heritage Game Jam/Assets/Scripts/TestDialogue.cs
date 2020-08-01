using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogue : MonoBehaviour
{
    //ref to DialogueBase script
    public DialogueBase dialogue;

    public void TriggerDialogue()
    {
        DialogueManager.instance.EnqueueDialogue(dialogue);
    }

    private void Update()
    {
        //feel free to change
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            TriggerDialogue();
        }
    }
}
