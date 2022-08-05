using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;  
    float speed = 10.0f;

    [SerializeField] private Camera mainCam;
    public Camera currentCam;
    public List<Camera> cameras;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
        currentCam = mainCam;
    }
    
    public void GetAllCam()
    {
        cameras = new List<Camera>();
        cameras.AddRange(GameObject.FindObjectsOfType<Camera>());
        foreach (var VARIABLE in cameras)
        {
            VARIABLE.enabled = VARIABLE == mainCam;
        }
    }

    void SwitchCam()
    {
        if (currentCam != mainCam)
        {
            currentCam.enabled = false;
            mainCam.enabled = true;
            currentCam = mainCam;
        }
        else
        {
            Camera cam = ChooseRandomCamDiffMain();
            if (cam != null)
            {
                mainCam.enabled = false;
                currentCam = cam;
                cam.enabled = true;
            }
        }
    }
    
    Camera ChooseRandomCamDiffMain()
    {
        if (cameras.Count == 1)
        {
            return null;
        }
        else
        {
            Camera cam;
            do
            {
                cam = cameras[Random.Range(0, cameras.Count)];
            } while (cam == mainCam);

            return cam;
        }
    }
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            SwitchCam();
        }
        if (Input.GetMouseButton (1) && currentCam == mainCam) {
            if (Input.GetAxis ("Mouse X") > 0) {
                currentCam.gameObject.transform.position -= new Vector3 (Input.GetAxisRaw ("Mouse X") * Time.deltaTime * speed, 
                    0.0f, Input.GetAxisRaw ("Mouse Y") * Time.deltaTime * speed);
            }
 
            else if (Input.GetAxis ("Mouse X") < 0) {
                currentCam.gameObject.transform.position -= new Vector3 (Input.GetAxisRaw ("Mouse X") * Time.deltaTime * speed, 
                    0.0f, Input.GetAxisRaw ("Mouse Y") * Time.deltaTime * speed);
            }
        }
 
    }
}
