using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform cam;
    //public SerialReceiver serialreceiver;
    public PlayerController playercontroller;
    public Transform[] childObject;
    public Transform[] childObject2;
    public Transform kinemaMaker;

    public GameObject a;
    // Start is called before the first frame update
    int i = 0;

    void Start()
    {
        
        sagasu(this.transform);
        a.GetComponent<Rigidbody>().isKinematic = false;
        a.GetComponent<Collider>().isTrigger = false;
    }

    void sagasu(Transform a)
    {

        foreach (Transform childObject2 in a)
        {
            if (childObject2.gameObject.GetComponent<Rigidbody>() != null)
            {
                childObject2.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
            if (childObject2.gameObject.GetComponent<Collider>() != null)
            {
                childObject2.gameObject.GetComponent<Collider>().isTrigger = true;
            }
            if (childObject2.childCount == 0)
            {


            }
            else
            {
                    sagasu(childObject2);
            }

        }
        

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.transform.eulerAngles.y);
        //var aim = this.Player.transform.position - this.transform.position;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if(other.gameObject.tag == "bullet")
        {
            Destroy(other.gameObject);
            other.transform.eulerAngles += new Vector3(0,180f,0);//Ç±ÇÍÇ≈ãtì]
            //serialreceiver.shoted();

            /*
            this.transform.eulerAngles -=
                new Vector3(0, this.transform.eulerAngles.y - other.transform.eulerAngles.y, 0);

            Debug.Log(this.transform.eulerAngles.y - other.transform.eulerAngles.y); //Ç±ÇÍÇ™ëäëŒìIÇ»ç¿ïW


        */

            //var aim = other.transform.eulerAngles.y - this.transform.eulerAngles.y;
            //var aim2 = cam.transform.eulerAngles.y;
            /*
            var aim = this.transform.position  -other.transform.position;
            var look = Quaternion.LookRotation(aim);
            Debug.Log(look.z);
            //var rot = new Quaternion(look.x, look.y, look.z);
            */

            //var a = this.transform.eulerAngles.y - other.transform.eulerAngles.y;
            //Debug.Log(Mathf.Floor(a));
        }
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.parent.gameObject.tag != "Player")
        {

            playercontroller.GroundTap(true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.parent.gameObject.tag != "Player")
        {

            playercontroller.GroundTap(true);
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.parent.gameObject.tag != "Player")
        {

            playercontroller.GroundTap(false);
        }
    }
    */


}
