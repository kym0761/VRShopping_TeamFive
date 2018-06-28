using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetPredictRateUI : MonoBehaviour {

    // SaveShopping.Item item;
    float starValue;
    public Image[] stars;
    public Material[] materials;
    public Text text;
    public Text CategoryTxt;
    string category;
    
    /*예측한 점수를 보여주기 위한 UI를 초기화할 때 사용하는 스크립트 파일.*/
    
    // Use this for initialization
    void Start()
    {
        Destroy(gameObject,3.5f);

    }

    // Update is called once per frame
    void Update()
    {

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
    public void SetCategory(string input)
    {
        category = input;
        CategoryTxt.text = category;
    }
    public void SetStarValue()
    {
        CustomerTEST ShoppingManagers = GameObject.Find("ShoppingManager").GetComponent<CustomerTEST>();
        int index = ShoppingManagers.FindProductIndex(category);

        float newStarValue = ShoppingManagers.Estimate(10, index) / 0.2f;
        //Debug.Log("newstarvalue : " +newStarValue);
        SetItem(newStarValue);

    }
}
