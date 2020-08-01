using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class EventBehaviour : ScriptableObject
{
    //put our logic behind the events
    public void TestEvent()
    {
        Debug.Log("Test event success");
        //any logic here
        //can reference the Reference script here
    }
}
