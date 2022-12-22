using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour
{
    public PlayerController playercontroller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {

            playercontroller.GroundTap(true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {

            playercontroller.GroundTap(true);
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {

            playercontroller.GroundTap(false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag != "Player")
        {

            playercontroller.GroundTap(true);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag != "Player")
        {

            playercontroller.GroundTap(true);
        }

    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag != "Player")
        {

            playercontroller.GroundTap(false);
        }
    }
}
