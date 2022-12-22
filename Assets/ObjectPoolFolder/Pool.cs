using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    Transform pool; //�I�u�W�F�N�g��ۑ������I�u�W�F�N�g��transform
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
            //�I�u�W�F����A�N�e�B�u�Ȃ�g����
            if (!t.gameObject.activeSelf)
            {
                t.SetPositionAndRotation(pos, qua);
                t.gameObject.SetActive(true);//�ʒu�Ɖ�]��ݒ��A�A�N�e�B�u�ɂ���
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
            GameObject a = Instantiate(obj, pos, qua, pool);//�����Ɠ�����pool��e�ɐݒ�
            RePlayObjectCollecter.RePlayObjectCollection(a.GetComponent<RePlayObject>());
            return a;
        }
    }
}
