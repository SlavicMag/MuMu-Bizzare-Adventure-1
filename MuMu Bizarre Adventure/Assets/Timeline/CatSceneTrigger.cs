using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CatSceneTrigger : MonoBehaviour
{
    public ControlTrigger controltrigger;
    public enum ControlTrigger { Trigger, IfNetEnemy };

    public PlayableDirector timelineDirector;
    public GameObject timelineManager;
    public Animator PlayerAnim;
    public int i;
    public List<GameObject> enemiesInTrigger = new List<GameObject>();
    public GameObject[] spawners;

    private void Update()
    {
        if (i <= 0)
        {
            gameObject.SetActive(false);
        }
        if (controltrigger == ControlTrigger.IfNetEnemy)
        {
            bool allInactive = true;

            foreach (GameObject spawner in spawners)
            {
                if (spawner.activeSelf)
                {
                    allInactive = false;
                    break;
                }
            }

            if (allInactive)
            {
                if (enemiesInTrigger.Count <= 0)
                {
                    if (i > 0)
                    {
                        PlayerAnim.StopPlayback();
                        timelineManager.SetActive(true);
                        timelineDirector.Play();
                        i--;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (controltrigger == ControlTrigger.Trigger)
        {
            if (other.CompareTag("Player") && i > 0)
            {
                PlayerAnim.StopPlayback();
                timelineManager.SetActive(true);
                timelineDirector.Play();
                i--;
            }
        }

        if (controltrigger == ControlTrigger.IfNetEnemy)
        {
            if (other.CompareTag("Enemy"))
            {
                if(!enemiesInTrigger.Contains(other.gameObject))
                {
                    enemiesInTrigger.Add(other.gameObject);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (controltrigger == ControlTrigger.IfNetEnemy)
        {
            if (other.CompareTag("Enemy"))
            {
                if (enemiesInTrigger.Contains(other.gameObject))
                {
                    enemiesInTrigger.Remove(other.gameObject);
                }
            }
        }
    }
}
