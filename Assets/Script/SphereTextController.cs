using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTextController : MonoBehaviour
{
    public Camera cam;
    public GameObject target;

    private RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cam == null || target == null || rect == null)
        {
            return;
        }

        rect.position = cam.WorldToScreenPoint(target.transform.position) + new Vector3(0.0f, 10.0f, 0.0f);
    }
}
