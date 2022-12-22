using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Comfig : MonoBehaviour
{
    Text text;
    public Slider SensSlider;
    public Slider AimSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PlayerController.Sens +=0.1f;
            SensSlider.value = PlayerController.Sens;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PlayerController.Sens -=0.1f;
            SensSlider.value = PlayerController.Sens;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PlayerController.Aim_Sens += 0.1f;
            AimSlider.value = PlayerController.Aim_Sens;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PlayerController.Aim_Sens -= 0.1f;
            AimSlider.value = PlayerController.Aim_Sens;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("PlayGround");
    }

    private void OnTriggerStay(Collider other)
    {
        SceneManager.LoadScene("PlayGround");
    }

    private void OnTriggerExit(Collider other)
    {
        SceneManager.LoadScene("PlayGround");
    }

}
