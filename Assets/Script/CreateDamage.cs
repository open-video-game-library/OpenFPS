using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateDamage : MonoBehaviour
{
    Text text;
    Transform Player;
    public Camera cam;
    public bool deleteItself;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("PlayerCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(Player.transform.position);
    }
}
