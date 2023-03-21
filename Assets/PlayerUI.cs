using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] PlayerController pc;
    [SerializeField] AudioClip audioclip;
    [SerializeField] Canvas canvas;

    [SerializeField] Image damagedEffect_Image;
    [SerializeField] Text hp_Text;
    [SerializeField] Slider hp_Slider;

    int maxhp;
    int hp;
    int prehp;

    // Start is called before the first frame update
    void Start()
    {
        if(canvas != null)
        {
            damagedEffect_Image.color = new Color(1, 1, 1, 0);
            maxhp = PlayerController.hp;
            hp_Text.text = PlayerController.hp.ToString();
            hp_Slider.value = (float)hp / (float)maxhp;
        }
        prehp = 0;
    }

    void UIUpdate()
    {
        if (canvas != null)
        {
            damagedEffect_Image.color = new Color(1, 1, 1, 255f);
            hp = PlayerController.hp;
            hp_Text.text = PlayerController.hp.ToString();
            hp_Slider.value = (float)hp / (float)maxhp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hp != prehp)
        {
            UIUpdate();
        }

        if (canvas != null)
        {
            damagedEffect_Image.color -= new Color(0, 0, 0, Time.deltaTime);
            hp = PlayerController.hp;
            hp_Text.text = PlayerController.hp.ToString();
            hp_Slider.value = (float)hp / (float)maxhp;
        }
        prehp = hp;
    }
}
