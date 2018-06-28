using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBriefUI : MonoBehaviour {

    public UnityEngine.UI.Text nameText;
    public Image ProductImage;
    public UnityEngine.UI.Text priceText;
    GameObject Camera;


    /*물체를 보고 있으면 나오는 UI의 내용을 초기화해주는 스크립트 파일.*/
    // Use this for initialization
    void Start () {
        Camera = GameObject.Find("Main Camera");

    }
	
	// Update is called once per frame
	void Update () {
        
        transform.LookAt(Camera.transform);

    }
    public void SetName(string name)
    {
        nameText.text = name;
    }
    public void SetImage(Texture2D Image)
    {
        ProductImage.sprite = Sprite.Create(Image, new Rect(0.0f, 0.0f, Image.width, Image.height), new Vector2(0.5f, 0.5f), 100.0f);
    }
    public void SetPrice(string price)
    {
        priceText.text = "가격 : " + price + "원";
    }
}
