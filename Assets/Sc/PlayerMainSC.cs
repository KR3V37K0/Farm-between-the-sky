using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainSC : MonoBehaviour
{
    [Header("--- Движение / Movement ---")]
    public float speed_Move;
    [Header("--- Камера / Camera ---")]
    public GameObject Camera;
    public Vector3 camera_Ofset;
    public Transform camera_Point;
    public float sence_H, sence_V;

    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Controll_Camera();
    }
    void FixedUpdate()
    {
        Controll_Movement();
    }
    private void Controll_Movement()
    {
        //Debug.Log(camera_Point.transform.forward);
        if ((Input.GetAxis("Vertical") != 0) || (Input.GetAxis("Horizontal") != 0))
        {
            transform.localEulerAngles = new Vector3(0,camera_Point.localEulerAngles.y,0);
        }
        rb.MovePosition(rb.position+transform.forward*speed_Move*1.2f*Input.GetAxis("Vertical")*Time.deltaTime);     //здесь коэффицент отличия движения вперед и вбок
        rb.MovePosition(rb.position + Vector3.right * speed_Move * Input.GetAxis("Horizontal") * Time.deltaTime);

    }
    private void Controll_Camera()
    {
        camera_Point.transform.localEulerAngles += new Vector3(-1*Input.GetAxis("Mouse Y")*sence_V, Input.GetAxis("Mouse X")*sence_H, 0);

        camera_Point.position = rb.position;
        Camera.transform.position = camera_Point.position;
        Camera.transform.localPosition = camera_Ofset;

        //camera_Point
    }
}
