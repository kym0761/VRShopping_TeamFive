using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BtnClick : MonoBehaviour {

    public GameObject Manager;

    public Button btn;

    public GameObject PredictUI;
    static GameObject UI;
	// Use this for initialization
	void Start () {
        /*버튼 클릭시 ItemGenerateManager가 토하는 물체의 Category를 변경시켜준다.*/
        Manager = GameObject.Find("ItemGenerateManager");
        btn = gameObject.GetComponent<Button>();
        /*버튼의 이벤트 추가해주고, 예측용UI를 뭐를 쓸건지 할당함.*/
        PredictUI = (GameObject)Resources.Load("Prefab/PredictRateUI", typeof(GameObject));
        btn.onClick.AddListener(TaskOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void TaskOnClick()
    {
        /*버튼 클릭시에, 해당카테고리 추천점수를 예측해서 표현해준다.*/
        string txt = transform.GetChild(0).GetComponent<Text>().text;

        Manager.GetComponent<XMLTest>().toFind = txt;

        if (UI != null)
        {
            Destroy(UI);
        }
        UI = Instantiate(PredictUI,transform.root.position + new Vector3(5,0,0) ,Quaternion.identity);
        UI.GetComponent<SetPredictRateUI>().SetCategory(txt);
        UI.GetComponent<SetPredictRateUI>().SetStarValue();
    }
 
}
