using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public GameObject Bullet;
    public Transform Player;
    public int shotInterbal;
    public float shotSpeed;
    public string send;
    int t = 0;
    // Start is called before the first frame update
    void Start()
    {
        send = "90";
        //Player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        t++;

        
        //this.transform.localRotation = look;

        
        /*
        float serial = this.Player.transform.eulerAngles.y - a.y;
        Debug.Log(serial);
        if (Mathf.Abs(serial) < 90)
        {
            serial += serial + 90;
            send = Mathf.Floor(serial).ToString();
        }

        if (t % shotInterbal == 0) {
            GameObject bullet = (GameObject)Instantiate(Bullet, transform.position, this.transform.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(transform.forward * shotSpeed);
            Destroy(bullet, 3.0f);
        }
        */
    }
}
