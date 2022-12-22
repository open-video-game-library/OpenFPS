using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class AgentBaseHeightCorrector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        _nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        CorrectBaseHeight();
    }

    private void CorrectBaseHeight()
    {
        NavMeshHit navhit;
        if (NavMesh.SamplePosition(transform.position, out navhit, 10f, NavMesh.AllAreas))
        {
            Ray r = new Ray(navhit.position, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit, 10f, LayerMask.GetMask("Level")))
            {
                _nav.baseOffset = -hit.distance;
            }
        }
    }

    NavMeshAgent _nav;
}
