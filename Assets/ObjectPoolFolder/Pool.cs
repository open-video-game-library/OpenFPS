using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    Transform pool; //�I�u�W�F�N�g��ۑ������I�u�W�F�N�g��transform
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
            //�I�u�W�F����A�N�e�B�u�Ȃ�g����
            if (!t.gameObject.activeSelf)
            {
                t.transform.SetPositionAndRotation(pos, qua);
                t.gameObject.SetActive(true);//�ʒu�Ɖ�]��ݒ��A�A�N�e�B�u�ɂ���
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
            GameObject a = Instantiate(obj, pos, qua, pool);//�����Ɠ�����pool��e�ɐݒ�
            RePlayObjectCollecter.RePlayObjectCollection(a.GetComponent<RePlayObject>());
            arrayPool.Add(a);
            return a;
        }
    }
}
