using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadtoDespone : MonoBehaviour
{
    //this object's MeshRender is delete when you load a scene
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
