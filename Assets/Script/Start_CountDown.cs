using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Start_CountDown : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Text text;
    [SerializeField] Rigidbody rb;

    public float countDown_Value;
    public Wave wave;
    float value;
    // Start is called before the first frame update
    void Start()
    {
        if (CursorController.is999 == 0)
        {
            canvas.enabled = false;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        
        value = countDown_Value;
    }

    private void Update()
    {
        value -= Time.deltaTime;
        text.text = Mathf.Floor(value + 1f).ToString();
        if (value <= 0 && canvas.enabled == true)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            wave.WaveUpdate();
            canvas.enabled = false;
        }
        else if (value > 0 && canvas.enabled == true)
        {

        }
    }
}
