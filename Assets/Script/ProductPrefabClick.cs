using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ProductPrefabClick : MonoBehaviour, IPointerClickHandler
{
    /*ScrollView안에 생성된 item 리스트를 클릭하면 일어나는 일을 설정하기 위해서 만듬.
     이 코드는 productPrefab과 같이 사용함.
         */
    public string Category;
    public string Name;
    public string Price;
    public string ImageURL;
    public string URL;
    public Texture2D Image;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)//마우스 좌클릭하면, 해당하는 이벤트를 할 수 있음.
        {
            Debug.Log(this.name + " are Clicked!");

            //! 아이디어, 아이템을 클릭 한 후 UI가 보이게되면, 해당 tag를 가진 ItemManager의 모든 child값을 받아서, 그 걸 통해 해당 prefab을 content에 child로 넣어준다.
            // thisItem.transform.parent = transform; 이런식으로 content에 script를 만들면됨.


                 Debug.Log("my father : "+transform.root.gameObject.name);

            SetUI.Item temp = new SetUI.Item(Category, Name, Price, ImageURL, URL, Image);
            transform.root.gameObject.GetComponent<SetUI>().SetThis(temp);


        }
    }
    public void InitProductPrefab(SetUI.Item item)
    {
        Category = item.GetCategory();
        Name = item.GetName();
        Price = item.GetPrice();
        ImageURL = item.GetImageURL();
        URL = item.GetURL();
        Image = item.GetImage();

        UnityEngine.UI.Text productName = transform.GetChild(0).GetComponent<UnityEngine.UI.Text>();
        productName.text = Name;
        UnityEngine.UI.Text productPrice = transform.GetChild(1).GetComponent<UnityEngine.UI.Text>();
        productPrice.text ="가격 : "+ Price+ " 원";

        UnityEngine.UI.Image img = transform.GetChild(2).GetComponent<Image>();
        img.sprite = Sprite.Create(Image, new Rect(0.0f, 0.0f, Image.width, Image.height), new Vector2(0.5f, 0.5f), 100.0f);

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
}
