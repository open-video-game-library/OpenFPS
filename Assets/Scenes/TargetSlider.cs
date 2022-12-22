using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetSlider : MonoBehaviour
{
    [SerializeField] Wave wave;
    [SerializeField] Transform Player;
    int maxwavenumber;
    Camera camera;
    Vector3 prePlayer;
    Animator MainCamera;

    int count;
    int once;
    [SerializeField] Text countText;
}
