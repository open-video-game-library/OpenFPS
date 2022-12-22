using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    [SerializeField]
    private GameObject createStageManager;

    [SerializeField]
    private float playerMoveSpeed = 3.0f;

    [SerializeField]
    private float playerApplySpeed = 0.2f;

    [SerializeField]
    private float enemyMoveSpeed = 3.0f;

    [SerializeField]
    private float enemyApplySpeed = 0.2f;

    [SerializeField]
    private GameObject startEffect;

    [SerializeField]
    private GameObject enemyEffect;

    [SerializeField]
    private GameObject hostageEffect;

    [SerializeField]
    private GameObject cautionUI;
    public void SetActiveCautionUI(bool flag) { cautionUI.SetActive(flag); }

    private Text cautionUIText;

    [SerializeField]
    private GameObject dangerUI;
    public void SetActiveDangerUI(bool flag) { dangerUI.SetActive(flag); }

    private Text dangerUIText;

    [SerializeField]
    private GameObject keyUI;

    [SerializeField]
    private GameObject purposeUI;

    [SerializeField]
    private Image speedUpGaugeUI;
    public Image GetSpeedUpGaugeUI() { return speedUpGaugeUI; }

    [SerializeField]
    private GameObject stageFoundation;

    [SerializeField]
    private GameObject itemBox;

    [SerializeField]
    private GameObject shotItem;

    [SerializeField]
    private GameObject decoyItem;

    private GameObject mainCamera;

    // シーン上にあるステージ
    public static List<Stage> stageList;

    // シーン上にある壁
    public static List<Wall> wallList;

    // シーン上にあるマップ（ステージ・壁含む）
    public static List<List<Map>> mapList;

    public static List<List<int>> stageIDList;

    public static List<Enemy> enemyList;

    private Vector2 forward;

    private Stage TargetStage;

    private Color TargetColor;

    private float rad = 0;

    public int ROOM_NUM;

    public int ENEMY_NUM;

    public static Vector3 startPos;
    public static Vector3 endPos;
    public static Vector3 stageCenterPos;

    // 人質が救出できる距離にプレイヤーがいるかどうか
    private bool hostageFlag;

    private bool moveCameraFlag;

    public static List<GameObject> trailList;


    public bool GetHostageFlag()
    {
        return hostageFlag;
    }


    


    

    

    
    

    private IEnumerator InitilizeEffect(Vector3 startPos, GameObject effect)
    {
        var obj = Instantiate(effect, startPos, effect.transform.rotation);

        yield return new WaitForSeconds(3f);

        obj.SetActive(false);
    }


    private void InitilizeStartEffect(Vector3 startPos, GameObject effect)
    {
        var obj = Instantiate(effect, startPos, effect.transform.rotation);
    }

    public void RemoveEnemy(Enemy e)
    {
        enemyList.Remove(e);
    }

    
    private IEnumerator MoveCamera()
    {
        yield return new WaitForSeconds(1f);

        moveCameraFlag = true;
    }

    public GameObject InstantiateShotItem(Vector3 pos)
    {
        GameObject obj = Instantiate(shotItem, pos, shotItem.transform.rotation);
        obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        return obj;
    }

    public GameObject InstantiateDecoyItem(Vector3 pos)
    {
        GameObject obj = Instantiate(decoyItem, pos, decoyItem.transform.rotation);
        obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        return obj;
    }

    private void Awake()
    {
        enemyList = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        stageList = new List<Stage>();
        wallList = new List<Wall>();

        mapList = new List<List<Map>>();
        mapList.Add(new List<Map>());

        stageIDList = new List<List<int>>();
        for (int i = 0; i < ROOM_NUM + 1; i++)
        {
            stageIDList.Add(new List<int>());
        }

        if(SceneManager.GetActiveScene().name != "Select")
        {
            // ステージを自動生成
            CreateStage cs = createStageManager.GetComponent<CreateStage>();
            cs.Create(new Vector3(-1, 0, 1), ROOM_NUM);
        }
        else
        {
            stageCenterPos = Vector3.zero;
            //startPos = new Vector3(-1, -104, -3);
        }

        

        //stageFoundation.transform.position = new Vector3(stageCenterPos.x, -158f, stageCenterPos.z);

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
