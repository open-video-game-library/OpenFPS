using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1) && Time.timeScale == 0)
        {
            Time.timeScale = 1;
            canvas.enabled = false;
        }


        if (Input.GetKeyDown(KeyCode.F1))
        {
            Time.timeScale = 0;
            canvas.enabled = true;
        }
    }
}
