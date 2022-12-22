using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagCatch : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateStart()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.name == "Awall")
        {
            Destroy(other.gameObject);
        }
        if(other.transform.tag == "Enemy")
        {
            PlayerController.hp = 0;
        }
    }
}
