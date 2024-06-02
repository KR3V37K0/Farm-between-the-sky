using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMainSC : MonoBehaviour
{
    [Header("--- Движение / Movement ---")]
    public float speed_Move;
    private Rigidbody rb;

    [Header("--- Камера / Camera ---")]
    public GameObject Camera;
    public Vector3 camera_Ofset;
    public Transform camera_Point;
    public float sence_H, sence_V,sence_Zoom;

    public Image image_Point;
    public Sprite[] array_image_Point;

    [Header("--- Держать / Grab ---")]
    public bool isGrab = false;
    public GameObject objGrab;




    void Start()
    {
        rb = GetComponent<Rigidbody>();
        image_Point.sprite=array_image_Point[0];
    }
    private void Update()
    {
        Controll_Camera();
    }
    void FixedUpdate()
    {
        Controll_Movement();
        if (isGrab) Grab();
        else detect_Point();

    }
    private void Controll_Movement()
    {
        //Debug.Log(camera_Point.transform.forward);
        if ((Input.GetAxis("Vertical") != 0) || (Input.GetAxis("Horizontal") != 0))
        {
            transform.localEulerAngles = new Vector3(0,camera_Point.localEulerAngles.y,0);
        }
        rb.MovePosition(rb.position+transform.forward*speed_Move*1.2f*Input.GetAxis("Vertical")*Time.deltaTime);     //здесь коэффицент отличия движения вперед и вбок
        rb.MovePosition(rb.position + transform.right * speed_Move * Input.GetAxis("Horizontal") * Time.deltaTime);

    }
    private void Controll_Camera()
    {
        camera_Point.transform.localEulerAngles += new Vector3(-1*Input.GetAxis("Mouse Y")*sence_V, Input.GetAxis("Mouse X")*sence_H, 0); //вращение

        camera_Point.position = rb.position;
        Camera.transform.position = camera_Point.position;                      //применение
        Camera.transform.localPosition = camera_Ofset;

        camera_Ofset.z += Input.GetAxis("Mouse ScrollWheel")*sence_Zoom;
        //camera_Ofset.x += Input.GetAxis("Mouse ScrollWheel")*-0.6f*sence_Zoom;            // зум
        camera_Ofset.z = Mathf.Clamp(camera_Ofset.z, -6.3f,-2.51f);
        //camera_Ofset.x = Mathf.Clamp(camera_Ofset.x, 0.66f, 2.94f);
    }

    private void detect_Point()
    {
        Ray ray = Camera.GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        float maxDistance = 7f+(-1f*camera_Ofset.z);

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.collider.gameObject.tag == "Grabable")
            {
                image_Point.sprite = array_image_Point[1];
                if (Input.GetMouseButton(0))
                {
                    objGrab=hit.collider.gameObject;
                    isGrab=true;
                }
            }
            else
            {
                image_Point.sprite = array_image_Point[0];
            }
        }
        else
        {
            image_Point.sprite = array_image_Point[0];
        }

        // Для визуализации луча в редакторе
        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
    }
    private void Grab()
    {


        if (!Input.GetMouseButton(0))  //отпустить объект
        {
            isGrab = false;
            objGrab = null;
        }
    }
}
