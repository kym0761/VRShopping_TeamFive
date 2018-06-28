using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float speed = 5.0f;
    private Transform position;

    /******************************WASD를 누르면 캐릭터가 그 방향에 맞게 움직인다. 더 깊게 알 필요 없음.******************************/


    // Use this for initialization
    void Start () {
        position = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //캐릭터가 움직일 vector를 만들고, 캐릭터의 포지션에 movement*speed값을 더하면 캐릭터가 그만큼 움직인다.
        Vector3 movement = GetComponent<Transform>().forward * moveVertical * Time.deltaTime + GetComponent<Transform>().right * moveHorizontal * Time.deltaTime;

        position.transform.position = position.transform.position + movement * speed;

    }
}
