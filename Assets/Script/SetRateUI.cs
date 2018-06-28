using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetRateUI : MonoBehaviour {

    // SaveShopping.Item item;
    float starValue;
    public Image[] stars;
    public Material[] materials;
    public Text text;
    public Text categoryTxt;
    /*평점 UI를 초기화해주기위해 만든 스크립트 파일.*/

    // Use this for initialization
    void Start () {
        

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetItem(float starvalue)
    {
        starValue = starvalue;

        for (int i = 0; i < stars.Length; i++)
        {
            if (starValue < i + 0.4f)
            {
                stars[i].material = materials[0];
            }
            else if (i + 0.4f <= starValue && starValue < i + 1.0f)
            {
                stars[i].material = materials[1];
            }
            else
            {
                stars[i].material = materials[2];
            }
        }

        text.text = "점수 : " + starValue.ToString("N1");
    }

}
