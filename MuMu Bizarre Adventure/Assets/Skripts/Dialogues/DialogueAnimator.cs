using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Playables;

public class DialogueAnimator : MonoBehaviour
{
    public DialogueManager dm;
    public DialogueTrigger[] dT;
    public PlayableDirector timelineDirector;
    public GameObject timelineManager;
    public bool dialogFalse = false;
    public bool scene = false;
    public DialogueManager trigger;
    public Animator boxanim;
    public Quests quests;
    public int nomer;
    public Animator NPSanim;
    public int numberAnim;
    public bool AnimPosleDialog;
    public string nameAnim;

    private void Start()
    {
        dm = FindObjectOfType<DialogueManager>();
        trigger = FindObjectOfType<DialogueManager>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (dialogFalse == false)
            {
                dT[nomer].TriggerDialogue();
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            dm.EndDialogue();
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (scene)
            {
                if (trigger.sentences.Count == 0 && boxanim.GetBool("boxOpen") == false)
                {
                    timelineManager.SetActive(true);
                    timelineDirector.Play();
                }
            }
            if (AnimPosleDialog)
            {
                if (nomer == numberAnim)
                {
                    if (trigger.sentences.Count == 0 && boxanim.GetBool("boxOpen") == false)
                    {
                        NPSanim.Play(nameAnim);
                    }
                }
            }
        }
    }
}
