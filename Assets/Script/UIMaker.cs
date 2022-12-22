using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMaker : MonoBehaviour
{
    bool on;
    public Shot shot;
    public GameObject _shot;
    public ShotPoint shotPoint;
    public GameObject _shotPoint;
    public PlayerController playerController;
    public GameObject _playerController;

    public float ADSTime = 0.5f; //構えるまでの時間
    public float FireBuck = 0.1f;//リコイルの時間
    public float ADSSpeed = 0.5f; //ADS移動時の倍率
    public int siya = 5; //リコイル時の視野変更


    // Start is called before the first frame update
    void Start()
    {
        shot = _shot.GetComponent<Shot>();
        shotPoint = _shotPoint.GetComponent<ShotPoint>();
        playerController = _playerController.GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //shot.ADSTime = ADSTime;
        shotPoint.FireBuck = FireBuck;
        shotPoint.siya = siya;
        //PlayerController.ADSSpeed = ADSSpeed;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (on == false)//UI見える化
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;

                on = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                on = false;
            }
        }

    }
}
