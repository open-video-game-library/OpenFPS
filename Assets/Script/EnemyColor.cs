using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyColor : MonoBehaviour
{
    public static float EnemyColor_R = 100f;
    public static float EnemyColor_G = 110f;
    public static float EnemyColor_B = 150f;
    public static float EnemyColor_A = 255f;
    [SerializeField] Slider EnemyColor_R_Slider;
    [SerializeField] Slider EnemyColor_G_Slider;
    [SerializeField] Slider EnemyColor_B_Slider;
    [SerializeField] Slider EnemyColor_A_Slider;

    [SerializeField] Text EnemyColor_R_Text;
    [SerializeField] Text EnemyColor_G_Text;
    [SerializeField] Text EnemyColor_B_Text;
    [SerializeField] Text EnemyColor_A_Text;

    public Wave wave;
    // Start is called before the first frame update
    void Start()
    {
        EnemyColor_R_Slider.value = EnemyColor_R;
        EnemyColor_G_Slider.value = EnemyColor_G;
        EnemyColor_B_Slider.value = EnemyColor_B;
        EnemyColor_A_Slider.value = EnemyColor_A;
        ValueUpdate();
        SliderValueTextChange();
        //Invoke("ColorChange", 0.5f);
    }

    public void ColorChange()
    {
        EnemyColor_R = EnemyColor_R_Slider.value;
        EnemyColor_G = EnemyColor_G_Slider.value;
        EnemyColor_B = EnemyColor_B_Slider.value;
        EnemyColor_A = EnemyColor_A_Slider.value;
    }

    private void Update()
    {
        EnemyColor_R = EnemyColor_R_Slider.value;
        EnemyColor_G = EnemyColor_G_Slider.value;
        EnemyColor_B = EnemyColor_B_Slider.value;
        EnemyColor_A = EnemyColor_A_Slider.value;
    }

    public void ValueUpdate()
    {
        //SliderValueTextChange();
        foreach (GameObject a in wave.EnemyBox)
        {
            MeshRenderer [] ragRendererbodies = a.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in ragRendererbodies)
            {
                foreach (Material renderer2 in renderer.materials)
                {
                    renderer2.color = new Color(EnemyColor_R / 255f, EnemyColor_G / 255f, EnemyColor_B / 255f, EnemyColor_A / 255f);
                    Debug.Log("EnemyColor" + renderer2.color);
                }
            }

            SkinnedMeshRenderer [] ragRendererbodies2 = a.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer renderer in ragRendererbodies2)
            {
                foreach (Material renderer2 in renderer.materials)
                {
                    renderer2.color = new Color(EnemyColor_R / 255f, EnemyColor_G / 255f, EnemyColor_B / 255f, EnemyColor_A / 255f);
                    Debug.Log(renderer2.color);
                }
            }
        }
        
    }

    void SliderValueTextChange()
    {
        EnemyColor_R_Text.text = EnemyColor_R.ToString("N0");
        EnemyColor_G_Text.text = EnemyColor_G.ToString("N0");
        EnemyColor_B_Text.text = EnemyColor_B.ToString("N0");
        EnemyColor_A_Text.text = EnemyColor_A.ToString("N0");
    }

    // Update is called once per frame
}
