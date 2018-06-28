using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour {


    public float mouseSensitivity = 300.0f;
    public float clampAngle = 80.0f;
    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis
    public GameObject player;

    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        //카메라를 마우스 방향에 맞춰서 회전.
        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;
        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;

        //카메라에 맞춰서 플레이어도 회전, 단 캐릭터는 위 아래로 방향을 바꿀순 없음. 옆으로만 방향바꾸기 가능.
        Quaternion PlayerRotation = Quaternion.Euler(0,rotY,0);
        player.transform.rotation = PlayerRotation;


    }



}
