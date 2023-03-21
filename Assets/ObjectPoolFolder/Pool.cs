using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    Transform pool; //オブジェクトを保存する空オブジェクトのtransform
    [SerializeField] RePlayObjectCollecter RePlayObjectCollecter;
    public bool childrenOnly;
    int countchildnumber = 1;

    public List<GameObject> arrayPool;

    void Start()
    {
        pool = this.transform;
        foreach(Transform child in this.transform)
        {
            arrayPool.Add(child.gameObject);
        }
    }

    public GameObject GetObject(GameObject obj, Vector3 pos, Quaternion qua)
    {
        foreach (GameObject t in arrayPool)
        {
            //オブジェが非アクティブなら使い回し
            if (!t.gameObject.activeSelf)
            {
                t.transform.SetPositionAndRotation(pos, qua);
                t.gameObject.SetActive(true);//位置と回転を設定後、アクティブにする
                return t.gameObject;
            }
        }
        if (childrenOnly == true)
        {
            //GameObject a = pool.GetChild(countchildnumber%pool.childCount).gameObject;
            GameObject a = (GameObject)arrayPool[countchildnumber % arrayPool.Count];
            Debug.Log("countchildnumber" + countchildnumber);
            a.transform.SetPositionAndRotation(pos, qua);
            countchildnumber++;
            return a;
        }
        else
        {
            GameObject a = Instantiate(obj, pos, qua, pool);//生成と同時にpoolを親に設定
            RePlayObjectCollecter.RePlayObjectCollection(a.GetComponent<RePlayObject>());
            arrayPool.Add(a);
            return a;
        }
    }
}
