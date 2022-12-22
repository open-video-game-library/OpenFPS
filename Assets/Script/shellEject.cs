using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shellEject : MonoBehaviour
{
    public GameObject shell;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShellOut()
    {
        GameObject a = Instantiate(shell, transform.position, Quaternion.identity);
        a.GetComponent<Rigidbody>().AddForce(transform.right * 2f, ForceMode.Impulse);
        Destroy(a, 1.0f);
    }
}
