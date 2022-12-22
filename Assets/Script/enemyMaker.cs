using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMaker : MonoBehaviour
{
    static public int killcount = 0;
    int prekillcount = -1;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Enemy = Instantiate(enemy, this.transform.position, Quaternion.identity);
        Enemy.transform.position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.eulerAngles += new Vector3(0, 45, 0);
        if (prekillcount != killcount) {
            GameObject Enemy = Instantiate(enemy, this.transform.position, Quaternion.identity);
            Enemy.transform.position = this.transform.position;
        }
        prekillcount = killcount;
    }
}
