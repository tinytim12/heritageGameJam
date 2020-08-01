using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Dialogue", menuName = "Dialogue")]
public class DialogueBase : ScriptableObject
{
    [System.Serializable]
    public class Info
    {
        public CharacterProfile character;
        public string myName;
        public Sprite portrait;
        [TextArea(2, 5)]
        public string myText;
    }

    [Header("Insert dialogue information below")]
    public Info[] dialogueInfo;
}
