using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSettingController : MonoBehaviour
{
    // Start is called before the first frame update
    CursorController CursorController;

    void Start()
    {
        CursorController = GetComponent<CursorController>();
    }
    public void GoShootingRange()
    {
        SceneManager.LoadScene("AllInOneScene");
        //SceneManager.LoadScene("gameScene");
    }



    public void GoPlayGround()
    {
        SceneManager.LoadScene("AllInOneScene");
        //SceneManager.LoadScene("gameScene");
    }
}
