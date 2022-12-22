using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseButton : MonoBehaviour
{
    bool onoff;
    float anchoredX;
    // Start is called before the first frame update
    void Start()
    {
        anchoredX = this.transform.parent.GetComponent<RectTransform>().anchoredPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenClose()
    {
        if(onoff == false)
        {
            this.transform.parent.GetComponent<RectTransform>().anchoredPosition = new Vector3(-anchoredX, 0f, 0);
            onoff = true;
        }
        else
        {
            this.transform.parent.GetComponent<RectTransform>().anchoredPosition = new Vector3(anchoredX, 0f, 0);
            onoff = false;
        }
    }
}
