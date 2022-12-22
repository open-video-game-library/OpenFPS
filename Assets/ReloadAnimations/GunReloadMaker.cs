using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunReloadMaker : MonoBehaviour
{
    public Shot shot;
    Animator animator;
    bool isreloading;
    public AudioClip audioclip;
    AudioSource audiosource;
    Animator RootAnimator;
    // Start is called before the first frame update
    void Start()
    {
        //shotPoint = this.transform.GetComponentInChildren<ShotPoint>();
        animator = GetComponent<Animator>();
        animator.SetLayerWeight(1, 0f);
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.root.GetComponent<Animator>() == true)
        {
            animator.SetFloat("Forward",
                this.transform.root.GetComponent<Animator>().GetFloat("Forward"));
        }
    }

    void ReloadComplete()
    {
        shot.reload();
        audiosource.PlayOneShot(audioclip);
    }
}
