using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectKapelBlood : MonoBehaviour
{
    public GameObject[] hblood;
    private List<ParticleCollisionEvent> m_CollisionEvents = new List<ParticleCollisionEvent>();
    private ParticleSystem m_ParticleSystem;

    void Start()
    {
        m_ParticleSystem = GetComponent<ParticleSystem>();
    }


    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = m_ParticleSystem.GetCollisionEvents(other, m_CollisionEvents);

        int i = 0;

        while (i < numCollisionEvents)
        {
            Vector3 pos = m_CollisionEvents[i].intersection - m_CollisionEvents[i].normal * Random.Range(0.3f, 0.7f);
            Instantiate(hblood[Random.Range(0, 4)], pos, Quaternion.identity);
            i++;
        }
    }
}
