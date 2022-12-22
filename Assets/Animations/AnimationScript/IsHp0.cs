using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHp0 : MonoBehaviour
{
    // Start is called before the first frame update
    public void Remove()
    {
        this.transform.position -= new Vector3(0,100f,0);
    }
}
