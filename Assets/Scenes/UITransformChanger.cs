using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITransformChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("UIMount", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UIMount()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector3(160f, 0f, 0);
    }

    
}
