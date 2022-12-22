using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFieldSponePointChanger : MonoBehaviour
{
    [SerializeField] Transform playerController;
    // Start is called before the first frame update
    void Start()
    {
        if (CursorController.is999 == 1)
        {
            playerController.position = transform.position;
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
