using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Text;

public class DialogueManager : MonoBehaviour
{
    //make instance of DialogueManager
    public static DialogueManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Fix this" + gameObject.name);
        }
        else
        {
            instance = this;
        }
    }

    //ref to textbox
    public GameObject dialogueBox;

    public Text dialogueName; //name of character
    public Text dialogueText; //text
    public Image dialoguePortrait; //portrait of character
    public float delay = 0.001f;

    //options stuff
    private bool isDialogueOption;
    public GameObject dialogueOptionUI;
    public bool inDialogue;
    public GameObject[] optionButtons; //ref to buttons
    private int optionsAmount; //how many buttons to pop up
    public Text questionText;

    //queue = FIFO(ifrst in first out) collection
    public Queue<DialogueBase.Info> dialogueInfo = new Queue<DialogueBase.Info>();

    private bool isCurrentlyTyping;
    private string completeText;

    //function that takes in information from DialogueBase and put it into our queue
    public void EnqueueDialogue(DialogueBase db)
    {
        if (inDialogue) return;
        inDialogue = true;

        dialogueBox.SetActive(true);
        dialogueInfo.Clear(); //clear everything inside that queue/collection

        //is incoming dialogue a dialogue option
        if(db is DialogueOptions)
        {
            isDialogueOption = true;
            DialogueOptions dialogueOptions = db as DialogueOptions;
            optionsAmount = dialogueOptions.optionsInfo.Length;
            questionText.text = dialogueOptions.questionText;

            for (int i = 0; i < optionButtons.Length; i++)
            {
                optionButtons[i].SetActive(false);
            }
            for (int i = 0; i < optionsAmount; i++)
            {
                //get ref to options buttons and turn them on
                optionButtons[i].SetActive(true);
                optionButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = dialogueOptions.optionsInfo[i].buttonName;
                UnityEventHandler myEventHandler = optionButtons[i].GetComponent<UnityEventHandler>();
                myEventHandler.eventHandler = dialogueOptions.optionsInfo[i].myEvent;
                //check if there is a next dialogue 
                if(dialogueOptions.optionsInfo[i].nextDialogue != null)
                {
                    myEventHandler.myDialogue = dialogueOptions.optionsInfo[i].nextDialogue;
                }
                else
                {
                    myEventHandler.myDialogue = null;
                }
            }
        }
        else
        {
            isDialogueOption = false;
        }

        foreach(DialogueBase.Info info in db.dialogueInfo)
        {
            dialogueInfo.Enqueue(info);
        }

        DequeueDialogue();
    }

    //gets information in order of what was enqueued
    public void DequeueDialogue()
    {
        if (isCurrentlyTyping == true)
        {
            CompleteText();
            StopAllCoroutines(); //stop typing
            isCurrentlyTyping = false;
            return;
        }

        if (dialogueInfo.Count == 0)
        {
            EndOfDialogue();
            return;
        }

        DialogueBase.Info info = dialogueInfo.Dequeue();
        completeText = info.myText;

        dialogueName.text = info.character.myName;
        dialogueText.text = info.myText;
        dialoguePortrait.sprite = info.character.myPortrait;

        dialogueText.text = ""; //clear dialogue text
        StartCoroutine(TypeText(info));
    }

    //have the text typed out
    IEnumerator TypeText(DialogueBase.Info info)
    {
        isCurrentlyTyping = true;
        //add in each character one by one
        foreach(char c in info.myText.ToCharArray())
        {
            yield return new WaitForSeconds(delay); //the delay
            dialogueText.text += c;
            yield return null;
        }
        isCurrentlyTyping = false;
    }

    private void CompleteText()
    {
        dialogueText.text = completeText;
    }

    public void EndOfDialogue()
    {
        dialogueBox.SetActive(false);
        inDialogue = false;
        OptionsLogic();
    }

    private void OptionsLogic()
    {
        if (isDialogueOption == true)
        {
            dialogueOptionUI.SetActive(true);
      
        }

    }

    public void CloseOptions()
    {
        dialogueOptionUI.SetActive(false);
    }

}
