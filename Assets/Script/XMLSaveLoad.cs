using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class XMLSaveLoad : MonoBehaviour {

    public List<SaveShopping.Item> shoppingItem;
    // Use this for initialization
    IEnumerator Start () {
        
        yield return StartCoroutine(LoadXML());
        
        GetComponent<SaveShopping>().ShoppingItem = shoppingItem;
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void SaveXML()
    {
        shoppingItem = GetComponent<SaveShopping>().ReturnItemList();

        XmlDocument saveXml = new XmlDocument();
        saveXml.AppendChild(saveXml.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        XmlNode root = saveXml.CreateNode(XmlNodeType.Element, "ItemInfo", string.Empty);
        saveXml.AppendChild(root);

        for (int i = 0; i < shoppingItem.Count; i++)
        {
            XmlNode item = saveXml.CreateNode(XmlNodeType.Element, "Item", string.Empty);

            root.AppendChild(item);

            XmlElement category = saveXml.CreateElement("Category");
            category.InnerText = shoppingItem[i].GetCategory();//category 값
            item.AppendChild(category);

            XmlElement name = saveXml.CreateElement("Name");
            name.InnerText = shoppingItem[i].GetName(); // name값
            item.AppendChild(name);

            XmlElement price = saveXml.CreateElement("Price");
            price.InnerText = shoppingItem[i].GetPrice(); //price값
            item.AppendChild(price);

            XmlElement imageURL = saveXml.CreateElement("ImageURL");
            imageURL.InnerText = shoppingItem[i].GetImageURL(); // imageURL값
            item.AppendChild(imageURL);

            XmlElement URL = saveXml.CreateElement("URL");
            URL.InnerText = shoppingItem[i].GetURL(); // URL값
            item.AppendChild(URL);

            XmlElement starValue = saveXml.CreateElement("StarValue");
            starValue.InnerText = shoppingItem[i].GetStarValue().ToString(); // starValue값
            item.AppendChild(starValue);
        }

        XmlElement Length = saveXml.CreateElement("Length");
        Length.InnerText = shoppingItem.Count.ToString();
        root.AppendChild(Length);

        saveXml.Save(Application.dataPath + "/Items.xml");
    }

    IEnumerator LoadXML()
    {
        shoppingItem = new List<SaveShopping.Item>();

        XmlDocument LoadXml = new XmlDocument();
        
        LoadXml.Load(Application.dataPath+"/Items.xml");
        if (LoadXml == null)
        {
            yield return null;
        }
        XmlNodeList categories = LoadXml.GetElementsByTagName("Category");
        XmlNodeList names = LoadXml.GetElementsByTagName("Name");
        XmlNodeList prices = LoadXml.GetElementsByTagName("Price");
        XmlNodeList imageURLs = LoadXml.GetElementsByTagName("ImageURL");
        XmlNodeList URLs = LoadXml.GetElementsByTagName("URL");
        XmlNodeList starValues = LoadXml.GetElementsByTagName("StarValue");

        
        int length = System.Convert.ToInt32(LoadXml.GetElementsByTagName("Length").Item(0).InnerText);
        for (int i = 0; i < length; i++)
        {
            string category =  categories.Item(i).InnerText;
            string name = names.Item(i).InnerText;
            string price = prices.Item(i).InnerText;
            string imageURL = imageURLs.Item(i).InnerText;
            string URL = URLs.Item(i).InnerText;
            float starValue = System.Convert.ToSingle(starValues.Item(i).InnerText);

            Texture2D texture = null;
            yield return StartCoroutine(LoadImg(imageURL, value => texture = value));
           

            SaveShopping.Item item = new SaveShopping.Item(category, name, price, imageURL, URL, texture);
            item.SetStarValue(starValue);

            shoppingItem.Add(item);
        }

    }
    public IEnumerator LoadImg(string ImageURL, System.Action<Texture2D> result )
    {
        WWW w = new WWW(ImageURL);
        yield return w;
        Debug.Log("이미지 URL : " + ImageURL);

        //temp = w.texture;
        /*이미지를 바운받을때까지 대기하고, 이미지를 다 받으면, 해당 이미지의 2Dtexture를 추출함*/
        result(w.texture);
    }

    public List<SaveShopping.Item> ReturnItemList()
    {
        return shoppingItem;
    }

}
