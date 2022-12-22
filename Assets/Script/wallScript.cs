using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallScript : MonoBehaviour
{
    private MeshRenderer[] childBox;
    // Start is called before the first frame update
    void Start()
    {
        childBox = GetComponents<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Vector3 a;
        foreach (ContactPoint point in collision.contacts)
        {
            a = point.point;
        }

        if (collision.transform.tag == "Bullets")
        {
            for(int i = 0; i< childBox.Length; i++)
            {
            }
        }
    }

}

