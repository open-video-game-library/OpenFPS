using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFlag : MonoBehaviour
{
    Wave wave;
    bool once;
    // Start is called before the first frame update
    [SerializeField] DoorUp doorUp;
    public bool dontWaveUpdate;
    float count;
    void Start()
    {
        wave = GameObject.Find("Wave_Maker").GetComponent<Wave>();
    }

    // Update is called once per frame
    void Update()
    {
        if(once == true && count <= 1f)
        {
            this.transform.position -= this.transform.up * Time.deltaTime * 5f;
            count += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && once == false)
        {
            if(dontWaveUpdate == false)
            {
                wave.WaveUpdate();
            }
            doorUp.GetFlag();
            once = true;
        }
    }
}
