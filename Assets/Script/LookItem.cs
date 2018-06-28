using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookItem : MonoBehaviour {

    public Transform playerTransform;
    // public Transform cameraTransform;
    public float time = 0.0f;
    float makeTime = 1.0f;
    public float height = 2.0f;
    bool looking;
    public GameObject brief_UI;
    GameObject UI;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hitInfo = new RaycastHit();
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "Product")
        {
            /*아이템을 감지하고 있으면 시간을 증가시키고 그 시간이 어느정도 됐다면 UI 생성함.*/
            if (time < makeTime)
                time += Time.deltaTime;
            if (time >= makeTime && UI == null)
            {
                /*UI생성 그리고 UI를 유저를 바라보게 만듬.*/
                UI = Instantiate(brief_UI, hitInfo.transform.GetComponent<ItemInsScript>().transform.position + new Vector3(0.0f, height, 0.0f), Quaternion.identity);
                UI.transform.LookAt(gameObject.transform);
                /*UI의 값을 변경. 자세한것은 SetBriefUI 참고.*/
                UI.GetComponent<SetBriefUI>().SetImage(hitInfo.transform.GetComponent<ItemInsScript>().GetImage());
                UI.GetComponent<SetBriefUI>().SetPrice(hitInfo.transform.GetComponent<ItemInsScript>().GetPrice());
                UI.GetComponent<SetBriefUI>().SetName(hitInfo.transform.GetComponent<ItemInsScript>().GetName());

            }
        }
        else
        {
            /*다른 곳으로 시야를 돌리면, 시간의 값을 0 으로 만들어주고, UI가 있다면 삭제해줌.*/
            if (!(time < 0))
                time = 0;
            if (time < makeTime && UI != null)
            {
                Destroy(UI, 1.0f);
                UI = null;
            }
        }
    }
}
