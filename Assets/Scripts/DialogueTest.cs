using System;
using System.Collections.Generic;
using UnityEngine;



public enum DialogueNames
{
    Greet,
    Ask,
    Buy,
    Sell
}



[Serializable]
public struct DialogueBranch
{
    public DialogueNames Name;
    public string[] Lines;
}
[CreateAssetMenu(fileName = "DialogueTest", menuName = "Scriptable Objects/DialogueTest")]
public class DialogueTest : ScriptableObject
{
    public List<DialogueBranch> Dialogues = new List<DialogueBranch>();
}
