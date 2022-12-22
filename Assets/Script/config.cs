using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class config : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Config()
    {
        Debug.Log("aaa");
    }

    public void GoShootingRange()
    {
        SceneManager.LoadScene("yakigassen_config");
        Debug.Log("aaa2");
    }

    public void OnClick()
    {
        SceneManager.LoadScene("yakigassen_config");
        Debug.Log("aaa3");
    }
}
