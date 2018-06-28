using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueChanged : MonoBehaviour {

    public Slider slider;
    public Image[] stars;
    public Material[] materials;
    public Text Text;
    public Button btn;
    float starValue;
    // Use this for initialization
    void Start () {

        /*슬라이더, 버튼에 이벤트 추가*/
        slider.onValueChanged.AddListener(delegate { Task(); });
        btn.onClick.AddListener(BtnClick);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Task()
    {
        /*slider의 값이 변하면 그 값을 받고 0.2로 나눈다 그럼 0~5개의 별 생성 가능함.*/
        float value = slider.value;

        starValue = value / 0.2f;


        /*점수에 따라서 별 갯수, 별의 생김새 표현.*/
        for (int i = 0; i < stars.Length; i++)
        {
            if (starValue < i + 0.4f) // 0번째별 별값 < 0+0.4
            {
                stars[i].material = materials[0]; // empty star
            }
            else if (i + 0.4f <= starValue && starValue < i + 1.0f)
            {
                stars[i].material = materials[1]; // half star
            }
            else
            {
                stars[i].material = materials[2]; // full star
            }
        }
        Text.text = "점수 : " + starValue.ToString("N1");

    }
    void BtnClick()
    {
        /*점수를 확정하면 그 점수를 ShoppingManager에게 보내줌.*/
        GameObject Manager = GameObject.Find("ShoppingManager");
        Manager.GetComponent<SaveShopping>().Rate(starValue);
        Destroy(gameObject);
    }
}
