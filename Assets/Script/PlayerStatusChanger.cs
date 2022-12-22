using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusChanger : MonoBehaviour
{
    [SerializeField] InputField PlayerContoller_Hp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public PlayerStatusChanger()
    {
        //PlayerController.hp = int.Parse(PlayerContoller_Hp.text);
    }
}
