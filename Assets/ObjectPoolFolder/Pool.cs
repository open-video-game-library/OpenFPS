using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    Transform pool; //オブジェクトを保存する空オブジェクトのtransform
    [SerializeField] RePlayObjectCollecter RePlayObjectCollecter;
    public bool childrenOnly;
    int countchildnumber = 0;


    void Start()
    {
        pool = this.transform;
    }

    public GameObject GetObject(GameObject obj, Vector3 pos, Quaternion qua)
    {
        foreach (Transform t in pool)
        {
            //オブジェが非アクティブなら使い回し
            if (!t.gameObject.activeSelf)
            {
                t.SetPositionAndRotation(pos, qua);
                t.gameObject.SetActive(true);//位置と回転を設定後、アクティブにする
                return t.gameObject;
            }
        }
        if (childrenOnly == true)
        {
            GameObject a = pool.GetChild(countchildnumber%pool.childCount).gameObject;
            a.transform.SetPositionAndRotation(pos, qua);
            countchildnumber++;
            return a;
        }
        else
        {
            GameObject a = Instantiate(obj, pos, qua, pool);//生成と同時にpoolを親に設定
            RePlayObjectCollecter.RePlayObjectCollection(a.GetComponent<RePlayObject>());
            return a;
        }
    }
}
