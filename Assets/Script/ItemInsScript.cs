using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInsScript : MonoBehaviour {

    GameObject ItemGenerateManager;
    // Use this for initialization
    public string Category;
    public string Name;
    public string Price;
    public string ImageURL;
    public string URL;
    public Texture2D Image;
    public GameObject[] Meshes;
    GameObject mesh;
    //static int itemNumber = 0;
    void Start () {

        //SetItemValue();

    }
    /*내용 초기화.*/
    public void SetItemValue(string category, string name, string price , string imageURL, string _URL)
    {

        Category = category;
        SetMesh();
        Name = name;
        Price = price;
        ImageURL = imageURL;
        URL = _URL;
        if (ImageURL != null)
            StartCoroutine(LoadImg(ImageURL));
    }
	// Update is called once per frame
	void Update () {
		
	}
    /*이미지 다운로드*/
    public IEnumerator LoadImg(string ImageURL)
    {
        WWW w = new WWW(ImageURL);
        yield return w;
        //Debug.Log("이미지 URL : "+ImageURL);
        Image = w.texture;
        /*이미지를 바운받을때까지 대기하고, 이미지를 다 받으면, 해당 이미지의 2Dtexture를 추출함*/
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
    void SetMesh() // 아이템 카테고리에 맞춰서 mesh를 결정해줌
    {
        switch (Category)
        {
            case "가방":
                mesh = Instantiate(Meshes[0], transform.position, transform.rotation);
                mesh.transform.SetParent(transform);
                break;
            case "부츠":
                mesh = Instantiate(Meshes[1], transform.position, transform.rotation);
                mesh.transform.SetParent(transform);
                break;
            case "드레스":
                mesh = Instantiate(Meshes[3], transform.position, transform.rotation);
                mesh.transform.SetParent(transform);
                break;
            case "안경":
                mesh = Instantiate(Meshes[4], transform.position, transform.rotation);
                mesh.transform.SetParent(transform);
                break;
            case "장갑":
                mesh = Instantiate(Meshes[5], transform.position, transform.rotation);
                mesh.transform.SetParent(transform);
                break;
            case "모자":
                mesh = Instantiate(Meshes[6], transform.position, transform.rotation);
                mesh.transform.SetParent(transform);
                break;
            case "자켓":
                mesh = Instantiate(Meshes[7], transform.position, transform.rotation);
                mesh.transform.SetParent(transform);
                break;
            case "바지":
                mesh = Instantiate(Meshes[8], transform.position, transform.rotation);
                mesh.transform.SetParent(transform);
                break;
            case "샌들":
                mesh = Instantiate(Meshes[9], transform.position, transform.rotation);
                mesh.transform.SetParent(transform);
                break;
            case "신발":
                mesh = Instantiate(Meshes[10], transform.position, transform.rotation);
                mesh.transform.SetParent(transform);
                break;
            case "슬리퍼":
                mesh = Instantiate(Meshes[11], transform.position, transform.rotation);
                mesh.transform.SetParent(transform);
                break;
            case "양말":
                mesh = Instantiate(Meshes[12], transform.position, transform.rotation);
                mesh.transform.SetParent(transform);
                break;
            case "정장":
                mesh = Instantiate(Meshes[13], transform.position, transform.rotation);
                mesh.transform.SetParent(transform);
                break;
            case "셔츠":
                mesh = Instantiate(Meshes[14], transform.position, transform.rotation);
                mesh.transform.SetParent(transform);
                break;
            case "넥타이":
                mesh = Instantiate(Meshes[15], transform.position, transform.rotation);
                mesh.transform.SetParent(transform);
                break;
            default:
                mesh = Instantiate(Meshes[2], transform.position, transform.rotation);
                mesh.transform.SetParent(transform);
                break;

        }
    }

}
