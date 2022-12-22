using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public int MaxHP;
    public int HP;
    public GameObject hpSlider;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        GameObject a = (GameObject)Instantiate(hpSlider, this.transform.position, Quaternion.identity, GameObject.Find("WorldCanvas").transform);
        slider = a.GetComponent<Slider>();
        slider.maxValue = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (slider == null)
        {
            return;
        }
        slider.value = HP;
        slider.transform.position = new Vector3(this.transform.position.x,
            this.transform.position.y + 2f,
            this.transform.position.z);
        if (HP < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
