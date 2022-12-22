using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{

    MeshRenderer[] ragRendererbodies;
    // Start is called before the first frame update
    void Start()
    {
        ragRendererbodies = GetComponentsInChildren<MeshRenderer>();
        Update_ColorChange();
    }

    public void Update_ColorChange()
    {
        if (Wave.enemyShape != 2) //敵の色変え用(中々シュール)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
