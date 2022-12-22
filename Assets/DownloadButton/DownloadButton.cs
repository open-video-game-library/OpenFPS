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

    // ƒ{ƒ^ƒ“‚ð‰Ÿ‚µ‚½‚ç

    public void DownLoad_Data()
    {
        dataManager.getData();
    }
}
