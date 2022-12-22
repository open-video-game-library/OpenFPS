using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeAgentMesh : MonoBehaviour
{
    [SerializeField] AgentBaseHeightCorrector AgentBaseHeightCorrector;
    // Start is called before the first frame update
    void LateStart()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        AgentBaseHeightCorrector.enabled = true;
    }
}
