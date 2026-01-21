using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public Text dialogueText;
    public Text nameText;
    public Image icon;

    public Animator boxAnim;

    public Queue<DialogueLine> sentences;

    private void Start()
    {
        sentences = new Queue<DialogueLine>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        boxAnim.SetBool("boxOpen", true);

        sentences.Clear();

        foreach(DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            sentences.Enqueue(dialogueLine);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        DialogueLine currentLine = sentences.Dequeue();

        icon.sprite = currentLine.charapter.icon;
        nameText.text = currentLine.charapter.name;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueText.text = "";
        foreach (char letter in dialogueLine.sentences.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        boxAnim.SetBool("boxOpen", false);

    }
}
