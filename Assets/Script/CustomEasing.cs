using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEasing : MonoBehaviour
{
    /*
    // Start is called before the first frame update
    [SerializeField]
    private AnimationCurve _customEasing;
    public Transform slider;
    void Start()
    {
        DOTween.Init();    // �� �R���Ȃ��ƌ����Ȃ�
        DOTween.defaultEaseType = Ease.Linear;
        transform.DOLocalMoveX(2f, 3f).SetEase(_customEasing);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            slider.transform.DOScale(
                new Vector3(0, 1, 2), // �X�P�[���l
                1f                    // ���o����
                ).OnComplete(() =>
                {
                    Debug.Log("on");
                });
        }
        else
        {
            slider.transform.DOScale(
                new Vector3(30, 1, 2), // �X�P�[���l
                1f                    // ���o����
                );
        }
    }
    */
}
