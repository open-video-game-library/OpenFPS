using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadScript : MonoBehaviour
{
    Enemy Enemy;
    Animator animator;
    public ShotPoint[] gunsearcher;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        gunsearcher = GetComponentsInChildren<ShotPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PickUp() //Reload完了アニメーションです
    {
        gunsearcher = GetComponentsInChildren<ShotPoint>();
        Debug.Log("reloading");
        for (int i =0; i<gunsearcher.Length; i++)
        {
            
            if (gunsearcher[i].isActiveAndEnabled == true)
            {
                gunsearcher[i].Reloadcomplete();
            }
        }
    }

    void SetUpComplete() //Complete setting up to shoot
    {
        
    }

}
