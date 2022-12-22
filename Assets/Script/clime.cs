using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clime : MonoBehaviour
{
    public bool climb;
    public PlayerController PlayerController;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            PlayerController.Climb(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            PlayerController.Climb(false);
        }

    }
}
