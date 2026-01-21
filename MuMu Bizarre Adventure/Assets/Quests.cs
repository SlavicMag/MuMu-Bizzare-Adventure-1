using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests : MonoBehaviour
{
    public int questNumber;
    public int[] items;
    public GameObject[] clouds;
    public GameObject[] barrier;
    public GameObject NPS;
    public Animator boxAnim;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player" )
        {
            if (other.gameObject.GetComponent<Pickup>() != null)
            {
               if (questNumber < items.Length)
                {
                    if (other.gameObject.GetComponent<Pickup>().id == items[questNumber])
                    {
                        questNumber++;
                        NPS.GetComponent<DialogueAnimator>().nomer++;
                        Destroy(other.gameObject);
                        CheckQuest();

                    }
                }
            }
        }
       

    }

    public void CheckQuest()
    {
        for(int i = 0; i < clouds.Length; i++)
        {
            if(i == questNumber)
            {
                clouds[i].SetActive(true);
                clouds[i].GetComponent<Animator>().SetTrigger("isTrigger");
                break;
            }
            else
            {
                clouds[i].SetActive(false);
            }
        }
        if(questNumber == 1)
        {
            barrier[0].SetActive(false);
            boxAnim.SetBool("boxOpen", false);
            NPS.GetComponent<DialogueAnimator>().dT[NPS.GetComponent<DialogueAnimator>().nomer].TriggerDialogue();
        }
        if (questNumber == 2)
        {
            barrier[1].SetActive(false);
            boxAnim.SetBool("boxOpen", false);
        }

    }

    public void NEActive()
    {
        NPS.SetActive(false);
    }

    public void BarierFalse()
    {
        barrier[1].SetActive(false);
    }
}

