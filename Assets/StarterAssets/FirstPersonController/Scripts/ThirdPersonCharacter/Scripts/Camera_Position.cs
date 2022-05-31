using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Position : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject Camera1;
    public GameObject Camera2;
    int Counter = 0;
    [SerializeField] private float infront = -0.25f;

    public GameObject Light;
    public GameObject mainCameraLight;

    public GameObject Player;
    public bool dashable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        //this.transform.position = Player.transform.position + new Vector3(0, 1.349f - 0.549f, 0);

    }
    /*
    void movecon()
    {
        Transform trans = transform;
        transform.position = trans.position;
        trans.position += trans.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical") * mainSPEED;
        trans.position += trans.TransformDirection(Vector3.right) * Input.GetAxis("Horizontal") * mainSPEED;
    }
    */

    void cameracon()
    {
        float x_Rotation = Input.GetAxis("Mouse X");
        float y_Rotation = Input.GetAxis("Mouse Y");
        x_Rotation = x_Rotation * x_sensi;
        y_Rotation = y_Rotation * y_sensi;
        camera.transform.eulerAngles = new Vector3(camera.transform.eulerAngles.x,
            camera.transform.eulerAngles.y, 0f);
        camera.transform.Rotate(0, x_Rotation, 0);
        camera.transform.Rotate(-y_Rotation, 0, 0);
    }

    public float mainSPEED;
    public float x_sensi;
    public float y_sensi;
    public new GameObject camera;
    public int usingcamera;

    // Update is called once per frame

    void Update()
    {

        dashable = true;

        Camera1.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        Camera2.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        if (Input.GetKeyDown(KeyCode.E))
        {
            usingcamera++;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            usingcamera--;
        }
        
        if (Input.GetMouseButton(1))
        {
            mainCameraLight.SetActive(true);
            Light.SetActive(false);
            dashable = false;

            mainCamera.transform.position = new Vector3(this.transform.position.x + this.transform.root.transform.forward.x * infront, this.transform.position.y, this.transform.position.z + this.transform.root.transform.forward.z * infront);
            mainCamera.transform.eulerAngles = this.transform.eulerAngles;
            Camera1.SetActive(false);
            Camera2.SetActive(false);
            if (Counter % 2 == 0)
            {
                if (Input.GetMouseButtonDown(0))
                {

                    Transform transform1 = mainCamera.transform;

                    Vector3 pos = new Vector3(this.transform.root.transform.position.x + this.transform.root.transform.forward.x * infront,
                    this.transform.root.transform.position.y + 1.5f,
                    this.transform.root.transform.position.z + this.transform.root.transform.forward.z * infront);
                    Vector3 worldAngle3 = transform1.eulerAngles;
                    Vector3 worldAngle4 = transform1.eulerAngles;

                    Camera1.transform.position = pos;
                    Camera1.transform.eulerAngles = mainCamera.transform.eulerAngles;
                    //Camera1.SetActive(false);
                    //Camera2.SetActive(true);
                    //Instantiate(Camera1, )

                    mainCamera.transform.position = transform1.position;

                    Counter++;
                }
            }

            else if (Counter % 2 == 1)
            {
                if (Input.GetMouseButtonDown(0))
                {

                    Transform transform2 = mainCamera.transform;

                    Vector3 pos = new Vector3(this.transform.root.transform.position.x + this.transform.root.transform.forward.x * infront,
                this.transform.root.transform.position.y + 1.5f,
                this.transform.root.transform.position.z + this.transform.root.transform.forward.z * infront);
                    Vector3 worldAngle3 = transform2.eulerAngles;
                    Vector3 worldAngle4 = transform2.eulerAngles;

                    Camera2.transform.position = pos;
                    Camera2.transform.eulerAngles = mainCamera.transform.eulerAngles;
                    //Camera2.SetActive(false);
                    //Camera1.SetActive(true);
                    mainCamera.transform.position = transform2.position;
                    //Instantiate(Camera1, )

                    Counter++;
                }
            }
        }
        else
        {
            Camera1.SetActive(true);
            Camera2.SetActive(true);
            Light.SetActive(true);
            mainCameraLight.SetActive(false);
            var scroll = Input.mouseScrollDelta.y;

            //movecon();
            cameracon();

            //mainCamera.transform.LookAt(Player.transform.position);
            /*
            this.transform.eulerAngles = new Vector3(0f,
                this.transform.eulerAngles.y,
                0f);
                */
            /*if (Vector3.Distance(Player.transform.position, Camera1.transform.position) <
            Vector3.Distance(Player.transform.position, Camera2.transform.position))*/
            if(usingcamera % 2 == 0)
            {
                Camera1.GetComponent<Rigidbody>().AddForce(new Vector3(0f, scroll * 30f, 0f), ForceMode.Impulse);
                //Camera2.SetActive(true);
                //Camera1.SetActive(false);
                mainCamera.transform.position = Camera1.transform.position;
            }
            else
            {
                Camera2.GetComponent<Rigidbody>().AddForce(new Vector3(0f, scroll * 30f, 0f), ForceMode.Impulse);
                //Camera2.SetActive(false);
                //Camera1.SetActive(true);
                mainCamera.transform.position = Camera2.transform.position;
            }

        }
    }
}
    
