using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class DialogueLine
{
    public DialogueCharapter charapter;
    [TextArea(3, 10)]
    public string sentences;
}

[System.Serializable]
public class DialogueCharapter
{
    public Sprite icon;
    public string name;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}
