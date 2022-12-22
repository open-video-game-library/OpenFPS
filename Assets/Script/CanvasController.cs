using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [Header("in_config_Scene")]
    public Canvas[] Appear_when_start_canvases;
    public Canvas[] Delete_when_start_canvases;

    [Header("in_game_Scene")]
    public Canvas[] _Appear_when_start_canvases;
    public Canvas[] _Delete_when_start_canvases;
    public Canvas[] _Appear_when_finish_canvases;
    public Canvas[] _Delete_when_finish_canvases;
    // Start is called before the first frame update
    void Start()
    {
        if(CursorController.is999 == 0) //When load Config
        {
            foreach (Canvas a in Appear_when_start_canvases)
            {
                a.enabled = true;
            }
            foreach (Canvas a in Delete_when_start_canvases)
            {
                a.enabled = false;
            }
        }
        else
        {
            foreach (Canvas a in _Appear_when_start_canvases)
            {
                a.enabled = true;
            }
            foreach (Canvas a in _Delete_when_start_canvases)
            {
                a.enabled = false;
            }
        }
    }

    // Update is called once per frame
    public void WhenPlayerDie(bool boolean) //when true player is die
    {
        foreach (Canvas a in _Appear_when_finish_canvases)
        {
            a.enabled = boolean;
        }
        foreach (Canvas a in _Delete_when_finish_canvases)
        {
            a.enabled = !boolean;
        }
    }
}
