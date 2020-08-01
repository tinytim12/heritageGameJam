using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UnityEventHandler : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent eventHandler;
    public DialogueBase myDialogue;

    //this is what happens when you click on this button
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        eventHandler.Invoke();
        DialogueManager.instance.CloseOptions(); //call closeoptions function whenever we click on a button

        //trigger dialogue when button is clicked
        if(myDialogue != null)
        {
            DialogueManager.instance.EnqueueDialogue(myDialogue);
        }
    }
}
