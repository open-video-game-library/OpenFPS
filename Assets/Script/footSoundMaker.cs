using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footSoundMaker : MonoBehaviour
{
    public AudioClip audioclip1;
    public AudioClip audioclip2;
    public AudioClip audioclip3;
    public AudioClip audioclip4;
    AudioSource audiosource;
    // Start is called before the first frame update
    void Start()
    {
        audiosource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void footsteps()
    {
        int a = Random.Range(1, 5);
        if(a == 1)
        {
            audiosource.PlayOneShot(audioclip1);
        }
        else if (a == 2)
        {
            audiosource.PlayOneShot(audioclip2);
        }
        else if (a == 3)
        {
            audiosource.PlayOneShot(audioclip3);
        }
        else if (a >= 4)
        {
            audiosource.PlayOneShot(audioclip4);
        }

        
    }
}
