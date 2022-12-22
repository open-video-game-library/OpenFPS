using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInformation : MonoBehaviour
{
    public Text sens;
    public Text Aim_sens;
    // Start is called before the first frame update
    void Start()
    {
        sens.text = PlayerController.Sens.ToString();
        Aim_sens.text = PlayerController.Aim_Sens.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
