using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SetUI : MonoBehaviour {

    public class Item
    {
        string Category;
        string Name;
        string Price;
        string ImageURL;
        string URL;
        Texture2D Img;

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
    };
    public GameObject content;

    public RectTransform ItemPrefab;

    Item thisItem;
    Button Btn;
    public Transform itemZenPos;
    public GameObject ItemIns;
    GameObject item;


    /*아이템을 클릭했을 때 나오는 UI 초기화를 위해 사용하는 스크립트 파일.*/


    // Use this for initialization
    void Start () {

       GameObject[] products = GameObject.FindGameObjectsWithTag("Product");

        for (int i = 0; i < products.Length; i++)
        {
            ItemInsScript product = products[i].GetComponent<ItemInsScript>();
            Item productData = new Item(product.GetCategory(), product.GetName(), product.GetPrice(),
                product.GetImageURL(), product.GetURL(), product.GetImage());
            AddScrollView(productData);
        }


        Btn = transform.GetChild(0).GetChild(0).GetComponent<Button>();
        Btn.onClick.AddListener(TaskOnClick);

        item = Instantiate(ItemIns, itemZenPos.position, Quaternion.identity);
        item.GetComponent<ItemInsScript>().SetItemValue(thisItem.GetCategory(),thisItem.GetName(),thisItem.GetPrice(),thisItem.GetImageURL(),thisItem.GetURL());
        item.tag = "WantToSelect";
        item.GetComponent<Rigidbody>().useGravity = false;
        item.AddComponent<SphereCollider>();
        item.GetComponent<SphereCollider>().radius = 0.5f;
        

    }

    public void SetThis(Item _item)
    {
        thisItem = _item;
        SetName(thisItem.GetName());
        SetImage(thisItem.GetImage());
        if (item != null)
        {
            item.GetComponent<ItemInsScript>().SetItemValue(thisItem.GetCategory(), thisItem.GetName(), thisItem.GetPrice(), thisItem.GetImageURL(), thisItem.GetURL());
        }

    }
	
	// Update is called once per frame
	void Update () {
        

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            Debug.Log("OK");
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("bye");
            transform.GetChild(0).GetComponent<Rigidbody>().useGravity = true;
            transform.GetChild(1).GetComponent<Rigidbody>().useGravity = true;
            transform.GetChild(2).GetComponent<Rigidbody>().useGravity = true;
            transform.GetChild(3).GetComponent<Rigidbody>().useGravity = true;
            Destroy(item,10.0f);
            Destroy(gameObject,2.0f);
        }

    }
    void TaskOnClick()
    {
        Application.OpenURL(thisItem.GetURL());

    }

    public void MakeItem(string category, string name, string price, string imageurl, string url, Texture2D img)
    {
        thisItem = new Item(category, name, price, imageurl, url, img);
    }
    public void SetName(string name)//UI에서 이름 부분 셋팅
    {
        UnityEngine.UI.Text ProductText = transform.GetChild(3).GetChild(0).GetComponent<UnityEngine.UI.Text>(); //UI의 nameCanvas.productNameTxt위치

        ProductText.text = name;
    }
    public void SetImage(Texture2D image)
    {
        Image Img = transform.GetChild(1).GetChild(0).GetComponent<Image>(); // UI의 ImageCanvas.productImage 위치.

        Sprite wantToChange = Sprite.Create(image, new Rect(0.0f, 0.0f, image.width,image.height), new Vector2(0.5f, 0.5f), 100.0f);
        Img.sprite = wantToChange;

    }

    public void AddScrollView(Item item)
    {
        RectTransform content = transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>();

        GameObject productPrefab = Instantiate(ItemPrefab.gameObject);
        productPrefab.transform.SetParent(content,false);

        productPrefab.GetComponent<ProductPrefabClick>().InitProductPrefab(item);
        

        

    }


}
