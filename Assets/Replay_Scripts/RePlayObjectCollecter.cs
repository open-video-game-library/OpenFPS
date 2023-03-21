using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class RePlayObjectCollecter : MonoBehaviour
{
    public int recordRate;
    public static float world_time;

    float end_time = 999f;
    public RePlayObject[] RePlayObjects;
    //public AnimatorRecorder[] animatorRecorder;

    int rePlayObjectCount;
    [SerializeField] Text text;
    [SerializeField] Text RePlayButtonUI;
    // Start is called before the first frame update

    [SerializeField] Text debugtext;

    [SerializeField] Slider slider;
    private void Awake()
    {
        RePlayObjects = new RePlayObject[500];
        //animatorRecorder = new AnimatorRecorder[100];
    }

    void Start()
    {
        Time.timeScale = 1;
        world_time = 0f;
        Invoke("Record", 1f / recordRate);
    }

    // Update is called once per frame

    private void LateUpdate()
    {
        Debug.Log("once_Count" + once_Count);
        world_time += Time.deltaTime;
        if (once_Count != 0)
        {
            debugtext.text = "";
            debugtext.text += "WorldTime : " + RePlayObjectCollecter.world_time + "\n";
            for (int i = 0; i < rePlayObjectCount; i++)
            {
                RePlayObjects[i].IsPlay(world_time);

                if(RePlayObjects[i].GetComponentInChildren<InvokeSound>() == true)
                {
                    for(int j =0; j < RePlayObjects[i].GetComponentInChildren<InvokeSound>().rememberTime.Count; j++)
                    {
                        float remTime = RePlayObjects[i].GetComponentInChildren<InvokeSound>().rememberTime[j];
                        if (remTime < world_time && world_time < remTime + Time.deltaTime)
                        {
                            RePlayObjects[i].GetComponentInChildren<InvokeSound>().RePlay(j);
                        }
                    }
                }
                if (RePlayObjects[i].GetComponentInChildren<InvokeParticle>() == true)
                {
                    for (int j = 0; j < RePlayObjects[i].GetComponentInChildren<InvokeParticle>().rememberTime.Count; j++)
                    {
                        float remTime = RePlayObjects[i].GetComponentInChildren<InvokeParticle>().rememberTime[j];
                        if (remTime < world_time && world_time < remTime + Time.deltaTime)
                        {
                            RePlayObjects[i].GetComponentInChildren<InvokeParticle>().RePlay(j);
                        }
                    }
                }

                if (RePlayObjects[i].AnimatorRecorder == true)
                {

                    debugtext.text += RePlayObjects[i].name + ":" + RePlayObjects[i].AnimatorRecorder.playBackTime + "/"+ RePlayObjects[i].AnimatorRecorder.startRecording_time + "\n";
                }
                
            }
        }
        else
        {
            Record();
        }
        
        if(world_time > end_time)
        {
            Debug.Log(world_time + "ISSTOP" + end_time);
            Time.timeScale = 0;
        }

        text.text = world_time.ToString();
        slider.value = world_time;
    }


    public void RePlayObjectCollection(RePlayObject a)
    {
        RePlayObjects[rePlayObjectCount] = a;
        rePlayObjectCount++;
    }

    void Record()
    {
        if (once_Count == 0)
        {
            for (int i = 0; i < rePlayObjectCount; i++)
            {
                Debug.Log("RePlayObjects[i]" +i + ":"+ RePlayObjects[i]);
                if(RePlayObjects[i] == true)
                {
                    RePlayObjects[i].RecordTransform(world_time);
                }
            }
            //Invoke("Record", 1f / recordRate);
        }
        else if(world_time > end_time && once_Count %2 == 0)
        {
            StartRePlay();
        }
    }

    bool once;
    public int once_Count;

    public void StartRePlay()
    {
        if (once_Count % 2 == 1)
        {
            if(once_Count == 1)
            {
                world_time = 0f;
            }
            Time.timeScale = 1f;
            for (int i = 0; i < rePlayObjectCount; i++)
            {
                    if (RePlayObjects[i].AnimatorRecorder != null)
                {
                    RePlayObjects[i].AnimatorRecorder.PlayBack();
                }
            }
        }
        else if (once_Count % 2 == 0)
        {
            Time.timeScale = 0;
            for (int i = 0; i < rePlayObjectCount; i++)
            {
                if (RePlayObjects[i].AnimatorRecorder != null)
                {
                    RePlayObjects[i].AnimatorRecorder.StopPlayBack();
                }
            }
        }

        if (once_Count == 0)
        {
            end_time = world_time;
            once_Count++;
            for (int i = 0; i < rePlayObjectCount; i++)
            {
                //
                RePlayObjects[i].transform.parent = null;
                //

                if (RePlayObjects[i].enabled == false)
                {
                    RePlayObjects[i].gameObject.SetActive(true);
                }

                if (RePlayObjects[i].AnimatorRecorder != null)
                {
                    RePlayObjects[i].transform.parent = null;
                    if (RePlayObjects[i].GetComponent<NavMeshAgent>() == true)
                    {
                        RePlayObjects[i].GetComponent<AICharacterControl>().enabled = false;
                        RePlayObjects[i].GetComponent<ThirdPersonCharacter>().enabled = false;
                        RePlayObjects[i].GetComponent<NavMeshAgent>().enabled = false;
                    }
                    
                }
                if (RePlayObjects[i].AnimatorRecorder != null)
                {
                    RePlayObjects[i].AnimatorRecorder.StopRecord();
                }
            }
        }
    }

    public void PlayBackStart()
    {
        if(once_Count != 0)
        {
            Time.timeScale = 1f;
            world_time = 0f;
            for (int i = 0; i < rePlayObjectCount; i++)
            {
                if (RePlayObjects[i].AnimatorRecorder != null)
                {
                    RePlayObjects[i].AnimatorRecorder.PlayBack();
                }
            }
        }
    }

    int count;

    public void StopAnimation()
    {
        world_time = slider.value;
        if (Time.timeScale != 0)
        {
            if (once_Count > 0)
            {
                Time.timeScale = 0f;
            }
            for (int i = 0; i < rePlayObjectCount; i++)
            {
                if (RePlayObjects[i].AnimatorRecorder != null)
                {
                    RePlayObjects[i].AnimatorRecorder.StopPlayBack();
                }
            }
            RePlayButtonUI.text = "Start";
        }
        else
        {
            Time.timeScale = 1f;
            for (int i = 0; i < rePlayObjectCount; i++)
            {
                if (RePlayObjects[i].AnimatorRecorder != null)
                {
                    RePlayObjects[i].AnimatorRecorder.PlayBack();
                }
            }
            RePlayButtonUI.text = "Stop";
        }
        count++;
    }

    public void SliderSetUp()
    {
        slider.maxValue = end_time;
    }

    [SerializeField] GameObject [] AppearItems;

    [SerializeField] GameObject[] DisappearItems;
    public void WatchTheRecording()
    {
        for (int i = 0; i < rePlayObjectCount; i++)
        {
            if (RePlayObjects[i].AnimatorRecorder != null)
            {
                RePlayObjects[i].AnimatorRecorder.StopPlayBack();
            }
        }
        if (once_Count == 0)
        {
            end_time = world_time;
            slider.maxValue = end_time;
            once_Count++;
            for (int i = 0; i < rePlayObjectCount; i++)
            {
                //
                RePlayObjects[i].transform.parent = null;
                //

                if (RePlayObjects[i].enabled == false)
                {
                    RePlayObjects[i].gameObject.SetActive(true);
                }

                if (RePlayObjects[i].AnimatorRecorder != null)
                {
                    RePlayObjects[i].transform.parent = null;
                    if (RePlayObjects[i].GetComponent<NavMeshAgent>() == true)
                    {
                        RePlayObjects[i].GetComponent<AICharacterControl>().enabled = false;
                        RePlayObjects[i].GetComponent<ThirdPersonCharacter>().enabled = false;
                        RePlayObjects[i].GetComponent<NavMeshAgent>().enabled = false;
                    }

                }
                if (RePlayObjects[i].AnimatorRecorder != null)
                {
                    RePlayObjects[i].AnimatorRecorder.StopRecord();
                }
            }
        }
        world_time = 0f;
        Time.timeScale = 1f;
        for (int i = 0; i < rePlayObjectCount; i++)
        {
            if (RePlayObjects[i].AnimatorRecorder != null)
            {
                RePlayObjects[i].AnimatorRecorder.PlayBack();
            }
        }

        foreach (GameObject a in AppearItems)
        {
            a.SetActive(false);
        }

        foreach (GameObject a in DisappearItems)
        {
            a.SetActive(true);
        }
    }

    public void SliderValueChange()
    {
        world_time = slider.value;
    }

    public void FinishWatchRecord()
    {
        foreach (GameObject a in AppearItems)
        {
            a.SetActive(true);
        }

        foreach (GameObject a in DisappearItems)
        {
            a.SetActive(false);
        }
    }
}
