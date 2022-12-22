using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpWeapon : MonoBehaviour
{
    Ray ray;
    [SerializeField] Transform mainCamera;

    [SerializeField] Text text;

    [SerializeField] Shot shot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(mainCamera.position, mainCamera.forward*1.5f);
        Vector3 origin = ray.origin; // 既存のRayの始点を取得
        Vector3 direction = ray.direction; // 既存のRayの方向ベクトルを取得

        RaycastHit hit;
        int layerMask = ~(1 << 7);
        if (Physics.Raycast(ray, out hit, 10.0f, layerMask))
        {
            if (hit.collider.transform.root.gameObject.tag == "weapon")
            {
                text.text = "Press " + "[E]" + "to Pick Up" + hit.collider.transform.root.transform.name;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PickUpWeapon_DoChange(hit.collider.transform.root.gameObject);
                }
            }
            else
            {
                text.text = "";
            }
        }
        else
        {
            text.text = "";
        }

        
    }

    public void PickUpWeapon_DoChange(GameObject gun)
    {
        GunChangeValueUpdater[] GunChangeValueUpdaters = mainCamera.root.GetComponentsInChildren<GunChangeValueUpdater>();
        foreach (GunChangeValueUpdater a in GunChangeValueUpdaters)
        {
            a.GetComponent<GunChangeValueUpdater>().DropDownWeapon();
        }
        shot.Latest_GunChangeValueUpdater = gun.GetComponent<GunChangeValueUpdater>();
        gun.transform.parent = shot.transform;
        MeshRenderer[] meshrenderers = gun.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer a in meshrenderers)
        {
            a.gameObject.layer = 7;
        }
        BoxCollider[] gun_colliders = gun.GetComponentsInChildren<BoxCollider>(); ;
        foreach (BoxCollider a in gun_colliders)
        {
            a.GetComponent<BoxCollider>().enabled = false;
            if (a.GetComponent<Rigidbody>() == true)
            {
                a.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        shot.PickUpGun();
    }


    private void LateUpdateWeaponStatus()
    {
        
    }
}
