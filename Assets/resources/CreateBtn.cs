using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreateBtn : MonoBehaviour {

    public GameObject Manager;

    public Button btn;

    /*GenerateManager의 UI에서 생성 버튼의 기능을 넣어주는 스크립트 파일*/
    // Use this for initialization
    void Start()
    {
        Manager = GameObject.Find("ItemGenerateManager");
        btn = gameObject.GetComponent<Button>();

        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TaskOnClick()
    {
        Manager.GetComponent<XMLTest>().Create();
    }
}
