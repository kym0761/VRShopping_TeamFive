using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveShopping : MonoBehaviour {

    [System.Serializable]
    public class Item
    {
        string Category;
        string Name;
        string Price;
        string ImageURL;
        string URL;
        Texture2D Img;
        float starValue = 0.0f;
        public Item(string category, string name, string price, string imageurl, string url, Texture2D img)
        {
            Category = category;
            Name = name;
            Price = price;
            ImageURL = imageurl;
            URL = url;
            Img = img;
        }
        public string GetName()
        {
            return Name;
        }
        public string GetPrice()
        {
            return Price;
        }
        public string GetCategory()
        {
            return Category;
        }
        public string GetImageURL()
        {
            return ImageURL;
        }
        public string GetURL()
        {
            return URL;
        }
        public Texture2D GetImage()
        {
            return Img;
        }
        public float GetStarValue()
        {
            return starValue;
        }
        public void SetStarValue(float input)
        {
            starValue = input;
        }
    };
    
    public List<Item> ShoppingItem;
    public GameObject ShoppingUI_Manager;
    public Camera playerCamera;
    GameObject ui;

    // Use this for initialization
    void Start () {
        ShoppingItem = new List<Item>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.M))//M버튼을 누르면 장바구니 내역을 볼 수 있음.
        {
            if (ui != null)
            {
                Destroy(ui);
            }
            GameObject UI = Instantiate(ShoppingUI_Manager, playerCamera.transform.position + playerCamera.transform.forward * 5.0f, playerCamera.transform.rotation);
            
            UI.GetComponent<SetShoppingUI>().ReceiveItem(ShoppingItem);        
        }
	}

    public void AddItem(Item Selected) //장바구니에 아이템을 넣어주는 기능.
    {
        ShoppingItem.Add(Selected);
        GetComponent<XMLSaveLoad>().SaveXML();// xml 데이터 업데이트
    }

    public void MakeUI()//장바구니 내역을 보여주는 UI를 만들때 사용하는 기능.
    {
        GameObject player = GameObject.Find("Player");

        ui = Instantiate(ShoppingUI_Manager, player.transform.position + player.transform.forward *5.0f,playerCamera.transform.rotation);
        ui.transform.SetParent(transform);
    }

    public void DeleteItem(Item item)//장바구니의 아이템을 삭제해주는 기능.
    {
        for (int i = 0; i < ShoppingItem.Count; i++)
        {
            if (ShoppingItem[i].GetName() == item.GetName())
            {
                ShoppingItem.RemoveAt(i);
                break;
            }
        }
        GetComponent<XMLSaveLoad>().SaveXML(); // xml 데이터 업데이트
    }

    public void Rate(float score)
    {
        Item current = ShoppingItem[ShoppingItem.Count - 1];
        current.SetStarValue(score);
        GetComponent<XMLSaveLoad>().SaveXML();// xml 데이터 업데이트
    }

    public List<Item> ReturnItemList()
    {
        return ShoppingItem;
    }
}
