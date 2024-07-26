using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

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
    [SerializeField] float Zoom_max, Zoom_min;
    

    public Image image_Point;
    public Sprite[] array_image_Point;

    [Header("--- Держать / Grab ---")]
    public bool isGrab = false;
    public GameObject objGrab;
    [Header("тест")]
    public float smoothSpeed = 0.125f;
    [SerializeField][Range(-90, 90)] float minXAngle, maxXAngle;

    private Vector3 velocity = Vector3.zero;  //wtf?
    Quaternion targetRotation;
    float targetZoom, currentZoom;
    private float zoomVelocity = 0.0f;

    private void OnValidate()
    {
        // Проверка и корректировка значений
        if (minXAngle > maxXAngle)
        {
            minXAngle = maxXAngle;
        }
    }
    void Start()
    {
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        image_Point.sprite=array_image_Point[0];
        targetRotation = camera_Point.transform.localRotation;
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
            transform.localEulerAngles = new Vector3(0, camera_Point.localEulerAngles.y, 0);
        }
        rb.MovePosition(rb.position + transform.forward * speed_Move * 1.2f * Input.GetAxis("Vertical") * Time.deltaTime);     //здесь коэффицент отличия движения вперед и вбок
        rb.MovePosition(rb.position + transform.right * speed_Move * Input.GetAxis("Horizontal") * Time.deltaTime);

    }
    private void Controll_Camera()
    {

        float rotationSmoothTime = 0.3f;
        if (Input.GetMouseButton(1))
        {
            image_Point.gameObject.SetActive(false);
            // Обновление целевого вращения на основе ввода мыши
            float mouseX = Input.GetAxis("Mouse X") * sence_H;
            float mouseY = -Input.GetAxis("Mouse Y") * sence_V;

            Quaternion rotationDelta = Quaternion.Euler(mouseY, mouseX, 0);
            targetRotation *= rotationDelta;

        }
        else if (Input.GetMouseButtonUp(1))
        {
            image_Point.gameObject.SetActive(true);
        }

        // Плавное вращение camera_Point к целевому вращению
        targetRotation.z = 0;
        camera_Point.transform.localRotation = Quaternion.Slerp(camera_Point.transform.localRotation, targetRotation, Time.deltaTime / rotationSmoothTime);

        Vector3 eulerAngles = camera_Point.transform.localEulerAngles;

        // Преобразование углов в диапазон (-180, 180)
        if (eulerAngles.x > 180)
            eulerAngles.x -= 360;

        // Ограничение углов X в диапазоне от minXAngle до maxXAngle
        eulerAngles.x = Mathf.Clamp(eulerAngles.x, minXAngle, maxXAngle);

        // Приведение углов к диапазону (0, 360)
        if (eulerAngles.x < 0)
            eulerAngles.x += 360;

        eulerAngles.z = 0;

        camera_Point.transform.localEulerAngles = eulerAngles;


        // Обновление позиции camera_Point
        camera_Point.position = rb.position;

        // Применение положения камеры
        targetZoom += Input.GetAxis("Mouse ScrollWheel") * sence_Zoom;
        targetZoom = Mathf.Clamp(targetZoom, Zoom_min, Zoom_max);

        // Плавное изменение зума
        currentZoom = Mathf.SmoothDamp(currentZoom, targetZoom, ref zoomVelocity, rotationSmoothTime);
        camera_Ofset.z = currentZoom;

        // Применение положения камеры
        Camera.transform.localPosition = camera_Ofset;
    }
    private void NEUROControll_Camera()
    {
        
        float rotationSmoothTime = 0.3f;
        if (Input.GetMouseButton(1))
        {
            image_Point.gameObject.SetActive(false);
            // Обновление целевого вращения на основе ввода мыши
            float mouseX = Input.GetAxis("Mouse X") * sence_H;
            float mouseY = -Input.GetAxis("Mouse Y") * sence_V;

            Quaternion rotationDelta = Quaternion.Euler(mouseY, mouseX, 0);
            targetRotation *= rotationDelta;
            
        }
        else if(Input.GetMouseButtonUp(1))
        {
            image_Point.gameObject.SetActive(true);
        }

        // Плавное вращение camera_Point к целевому вращению
        targetRotation.z = 0;
        camera_Point.transform.localRotation = Quaternion.Slerp(camera_Point.transform.localRotation, targetRotation, Time.deltaTime / rotationSmoothTime);

        Vector3 eulerAngles = camera_Point.transform.localEulerAngles;

        // Преобразование углов в диапазон (-180, 180)
        if (eulerAngles.x > 180)
            eulerAngles.x -= 360;

        // Ограничение углов X в диапазоне от minXAngle до maxXAngle
        eulerAngles.x = Mathf.Clamp(eulerAngles.x, minXAngle, maxXAngle);

        // Приведение углов к диапазону (0, 360)
        if (eulerAngles.x < 0)
            eulerAngles.x += 360;

        eulerAngles.z = 0;

        camera_Point.transform.localEulerAngles = eulerAngles;


        // Обновление позиции camera_Point
        camera_Point.position = rb.position;

        // Применение положения камеры
        targetZoom += Input.GetAxis("Mouse ScrollWheel") * sence_Zoom;
        targetZoom = Mathf.Clamp(targetZoom, Zoom_min, Zoom_max);

        // Плавное изменение зума
        currentZoom = Mathf.SmoothDamp(currentZoom, targetZoom, ref zoomVelocity, rotationSmoothTime);
        camera_Ofset.z = currentZoom;

        // Применение положения камеры
        Camera.transform.localPosition = camera_Ofset;
    }
    private void detect_Point()
    {
        //Vector2 localCursorPosition;
        // RectTransformUtility.ScreenPointToLocalPointInRectangle(image_Point.rectTransform as RectTransform, Input.mousePosition, Camera.GetComponent<Camera>(), out localCursorPosition);
        image_Point.rectTransform.anchoredPosition = new Vector3(Input.mousePosition.x- (Screen.width / 2), Input.mousePosition.y - (Screen.height / 2),0);
        //image_Point.rectTransform.anchoredPosition.x -= (Screen.width / 2);
        //image_Point.transform.position = Input.mousePosition;
        Ray ray = Camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        float maxDistance = 7f + (-1f * camera_Ofset.z);

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            Vector3 hitPoint = hit.point;
            float distanceToObject = Vector3.Distance(hitPoint, Camera.transform.position);
            float img_size = maxDistance/ distanceToObject /4f;
            image_Point.transform.localScale = new Vector3(img_size, img_size, 1f);
            if (hit.collider.gameObject.tag == "Grabable")
            {
                image_Point.sprite = array_image_Point[1];
                if (Input.GetMouseButton(0))
                {
                    objGrab = hit.collider.gameObject;
                    isGrab = true;
                }
            }
            else if (hit.collider.gameObject.tag == "Player")
            {
                image_Point.gameObject.SetActive(false);
            }
            else
            {
                image_Point.gameObject.SetActive(true);
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
    private void OLDControll_Movement()
    {
        //Debug.Log(camera_Point.transform.forward);
        if ((Input.GetAxis("Vertical") != 0) || (Input.GetAxis("Horizontal") != 0))
        {
            transform.localEulerAngles = new Vector3(0,camera_Point.localEulerAngles.y,0);
        }
        rb.MovePosition(rb.position+transform.forward*speed_Move*1.2f*Input.GetAxis("Vertical")*Time.deltaTime);     //здесь коэффицент отличия движения вперед и вбок
        rb.MovePosition(rb.position + transform.right * speed_Move * Input.GetAxis("Horizontal") * Time.deltaTime);

    }
    private void OLDControll_Camera()
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
    private void OLDdetect_Point()
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
    private void OLDGrab()
    {


        if (!Input.GetMouseButton(0))  //отпустить объект
        {
            isGrab = false;
            objGrab = null;
        }
    }
}
