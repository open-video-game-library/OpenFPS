using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class NavMeshSurfaceBaker : MonoBehaviour
{
    NavMeshSurface _surface;
    // Start is called before the first frame update
    void Start()
    {
        _surface = GetComponent<NavMeshSurface>();
        _surface.BuildNavMesh();
        //
    }
    IEnumerator TimeUpdate()
    {
        while (true)
        {
            _surface.BuildNavMesh();

            //yield return new WaitForSeconds(10.0f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void Rebulid()
    {
        _surface.BuildNavMesh();
        //StartCoroutine(TimeUpdate());
    }
}
