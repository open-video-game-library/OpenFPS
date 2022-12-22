using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeParticle : MonoBehaviour
{
    public List<ParticleSystem> particleSystems;
    public List<float> rememberTime;
    // Start is called before the first frame update
    void Start()
    {
        List<ParticleSystem> particleSystems = new List<ParticleSystem>();
        List<float> rememberTime = new List<float>();
    }

    // Update is called once per frame
    public void RecordParticle(ParticleSystem particleSystem)
    {
        particleSystems.Add(particleSystem);
        rememberTime.Add(RePlayObjectCollecter.world_time);
    }

    public void RePlay(int rem)
    {
        if (particleSystems[rem] != null)
        {
            particleSystems[rem].Play();
        }
    }
}
