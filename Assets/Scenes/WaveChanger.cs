using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveChanger : MonoBehaviour
{
    [SerializeField] InputField phase_inputfield;
    [SerializeField] InputField enemyMany_inputfield;
    [SerializeField] InputField leftTime_inputfield;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "AllInOneScene") //if this scene is config
        {
            phase_inputfield.text = Wave.Wave_number.ToString();
            enemyMany_inputfield.text = Wave.SponeEnemyMany.ToString();
            leftTime_inputfield.text = Wave.LeftTime.ToString();
        }
    }

    // Start is called before the first frame update
    public void StageValueChange()
    {
        Wave.Wave_number = int.Parse(phase_inputfield.text);
        Wave.SponeEnemyMany = int.Parse(enemyMany_inputfield.text);
        Wave.LeftTime = int.Parse(leftTime_inputfield.text);

    }
}
