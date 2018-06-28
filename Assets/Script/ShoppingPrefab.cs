using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingPrefab : MonoBehaviour {

    public string Category;
    public string Name;
    public string Price;
    public string ImageURL;
    public string URL;
    public Texture2D Image;
    public float StarValue;

    /*장바구니 내역안의 아이템들을 초기화하는 함수.*/

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    Button Btn;
    public void InitProductPrefab(SaveShopping.Item item)
    {
        Category = item.GetCategory();
        Name = item.GetName();
        Price = item.GetPrice();
        ImageURL = item.GetImageURL();
        URL = item.GetURL();
        Image = item.GetImage();
        StarValue = item.GetStarValue();

        UnityEngine.UI.Text productName = transform.GetChild(0).GetComponent<UnityEngine.UI.Text>();
        productName.text = Name;
        UnityEngine.UI.Text productPrice = transform.GetChild(1).GetComponent<UnityEngine.UI.Text>();
        productPrice.text = "가격 : " + Price + " 원";

        UnityEngine.UI.Image img = transform.GetChild(2).GetComponent<Image>();
        img.sprite = Sprite.Create(Image, new Rect(0.0f, 0.0f, Image.width, Image.height), new Vector2(0.5f, 0.5f), 100.0f);

        Btn = transform.GetChild(3).GetComponent<Button>();
        Btn.onClick.AddListener(TaskOnClick);

    }
    void TaskOnClick() //장바구니 내역에 있는 아이템의 삭제 버튼을 누르면, 그 삭제기능을 발동시킴.
    {
        GameObject shoppingManager = GameObject.Find("ShoppingManager");
        SaveShopping.Item wantToDelete = new SaveShopping.Item(Category,Name,Price,ImageURL,URL,Image);
        shoppingManager.GetComponent<SaveShopping>().DeleteItem(wantToDelete);

        Destroy(gameObject);

    }

    public string GetCategory()
    {
        return Category;
    }
    public string GetName()
    {
        return Name;
    }
    public string GetImageURL()
    {
        return ImageURL;
    }
    public string GetPrice()
    {
        return Price;
    }
    public string GetURL()
    {
        return URL;
    }
    public Texture2D GetImage()
    {
        return Image;
    }
    public float GetStarValue()
    {
        return StarValue;
    }
}
