using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyColor : MonoBehaviour
{
    public static float EnemyColor_R;
    public static float EnemyColor_G;
    public static float EnemyColor_B;
    public static float EnemyColor_A;
    [SerializeField] Slider EnemyColor_R_Slider;
    [SerializeField] Slider EnemyColor_G_Slider;
    [SerializeField] Slider EnemyColor_B_Slider;
    [SerializeField] Slider EnemyColor_A_Slider;

    public Wave wave;
    // Start is called before the first frame update
    void Start()
    {
        EnemyColor_R_Slider.value = EnemyColor_R;
        EnemyColor_G_Slider.value = EnemyColor_G;
        EnemyColor_B_Slider.value = EnemyColor_B;
        EnemyColor_A_Slider.value = EnemyColor_A;
        ValueUpdate();
    }

    public void ValueUpdate()
    {
        SliderValueTextChange();
        foreach (GameObject a in wave.EnemyBox)
        {
            MeshRenderer [] ragRendererbodies = a.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in ragRendererbodies)
            {
                foreach (Material renderer2 in renderer.materials)
                {
                    renderer2.color = new Color(EnemyColor_R / 255f, EnemyColor_G / 255f, EnemyColor_B / 255f, EnemyColor_A / 255f);
                }
            }

            SkinnedMeshRenderer [] ragRendererbodies2 = a.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer renderer in ragRendererbodies2)
            {
                foreach (Material renderer2 in renderer.materials)
                {
                    renderer2.color = new Color(EnemyColor_R / 255f, EnemyColor_G / 255f, EnemyColor_B / 255f, EnemyColor_A / 255f);
                }
            }
        }
    }

    void SliderValueTextChange()
    {
        EnemyColor_R_Slider.GetComponentInChildren<Text>().text = EnemyColor_R_Slider.value.ToString();
        EnemyColor_G_Slider.GetComponentInChildren<Text>().text = EnemyColor_G_Slider.value.ToString();
        EnemyColor_B_Slider.GetComponentInChildren<Text>().text = EnemyColor_B_Slider.value.ToString();
        EnemyColor_A_Slider.GetComponentInChildren<Text>().text = EnemyColor_A_Slider.value.ToString();
        EnemyColor_R = EnemyColor_R_Slider.value;
        EnemyColor_G = EnemyColor_G_Slider.value;
        EnemyColor_B = EnemyColor_B_Slider.value;
        EnemyColor_A = EnemyColor_A_Slider.value;
    }

    // Update is called once per frame
}
