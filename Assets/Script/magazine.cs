using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magazine : MonoBehaviour
{
    public ShotPoint shotpoint;
    // Start is called before the first frame update
    void Start()
    {
        shotpoint = this.transform.parent.gameObject.GetComponent<ShotPoint>();
        /*
        this.transform.DOLocalMove(
                   new Vector3(0f, -0.217f, -0.56f), // I—¹Žž‚ÌRotation
                   shotpoint.reloadime                // ‰‰oŽžŠÔ
               ).OnComplete(() =>
               {
                   shotpoint.leavebullets = ShotPoint.fullbullets;
                   shotpoint.reloading = false;
               });
               */
    }

    // Update is called once per frame
    void Update()
    {

        if(shotpoint.reloading == false)
        {
            Destroy(this.gameObject);
        }
    }
}
