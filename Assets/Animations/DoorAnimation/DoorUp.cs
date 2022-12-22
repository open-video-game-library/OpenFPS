using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUp : MonoBehaviour
{
    bool once;
    float count;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(once == true && count < 1f)
        {
            this.transform.position += this.transform.up * Time.deltaTime * 5f;
            count += Time.deltaTime;
        }
    }

    public void GetFlag()
    {
        once = true;
    }
}
