using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DownloadButton : MonoBehaviour
{
    DataManager dataManager;
    public GameObject donwloadbutton;
    void Start()
    {
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
    }

    public void wakeUp()
    {

    }

    // �{�^������������

    public void DownLoad_Data()
    {
        dataManager.getData();
    }
}
