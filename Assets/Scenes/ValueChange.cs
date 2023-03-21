using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueChange : MonoBehaviour
{

    // Start is called before the first frame update
    void LateStart()
    {
        Invoke("SliderUpdate", 0.25f);
    }

    public void SliderUpdate()
    {
        this.GetComponent<Text>().text = this.GetComponentInParent<Slider>().value.ToString("N0");
    }

    // Update is called once per frame

    public void ValueChanged()
    {
        if (this.GetComponentInParent<Slider>() == true)
        {
            this.GetComponent<Text>().text = this.GetComponentInParent<Slider>().value.ToString("N0");
        }
    }
}
