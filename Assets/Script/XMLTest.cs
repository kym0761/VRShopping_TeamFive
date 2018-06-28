using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net;//webrequest
using System.IO;//streamreader
using System.Xml; //xmldoc
using System.Security.Cryptography.X509Certificates;//security
using System.Net.Security; //security
using UnityEngine.UI;
//using UnityEngine.Networking;
public class XMLTest : MonoBehaviour {

    class Item
    {
        string Category;
        string Name;
        string Price;
        string ImageURL;
        string URL;

        public Item(string category, string name, string price, string imageurl, string url)
        {
            Category = category;
            Name = name;
            Price = price;
            ImageURL = imageurl;
            URL = url;
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
    }; //아이템 정보를 관리하게 편하게 만들기 위한 클래스 선언함.

    string api = "https://openapi.11st.co.kr/openapi/OpenApiService.tmall?"; // 자세한 내용은 api참고
    string key = "key=15ad03e97e8776eb603b9fb8f33a4d43&apiCode=ProductSearch";
    string apiCode = "&apiCode=ProductSearch";
    string page = "&pageSize=";
    string pageSize = "20"; // 한번에 나올 아이템의 갯수 1~200개까지만 가능함
    string keyword = "&keyword=";
    public string toFind = "bag"; // 검색할 내용

    public GameObject cube; // 작동이 잘 되나 확인하기 위한 큐브 물체. 조금씩 움직짐
    Item[] Items; // Item Array 
    public RawImage ImageInstance; // 이미지 출력이 작동되는지 확인하기 위한 임시 변수
    // Use this for initialization
    public GameObject ItemInstance; // 아이템 instantiate
    public Transform ItemInsTransform; // 아이템 놓을 위치

    GameObject[] Instantiated; // 동적으로 배정된 아이템을 저장할 공간 -> 다시 아이템을 배정하고 싶을때 기존의 것을 삭제
    bool isPlayerNear = false;

    public GameObject UIforBtn;
    GameObject ui;
    public Transform UIpos;

    void Start()
    {

    }

    // Update is called once per frame
    void Update() {

        /*현재 모든 기능은 T 버튼을 누르면 작동이 되도록 만들어져 있다. 만일 T버튼을 눌러도 아무일이 없으면
         유니티 에디터에서 ItemGenerateManager의 Tofind의 값을 영어로 바꾸고 하면 됨
         그게 아니면 유니티 에디터 오류이므로 유니티 다시 키면 된다.
         */
        if (isPlayerNear == true)
        {
            if (Input.GetKeyDown(KeyCode.T) && Instantiated == null) //처음 기능을 사용할 때.
            {
                DoThis();
            }
            else if (Input.GetKeyDown(KeyCode.T) && Instantiated != null)//두번째부터는 여기
            {
                for (int i = 0; i < Instantiated.Length; i++)
                {
                    Destroy(Instantiated[i]);// 기존에 있던 인스턴스들을 전부 지운다.
                }
                Instantiated.Initialize();

                DoThis();
            }
        }
    }
    public void Create() // 생성 버튼을 위해서 만들어놓은 이벤트 함수
    {
        if (isPlayerNear == true)
        {
            if (Instantiated == null) //처음 기능을 사용할 때.
            {
                DoThis();
            }
            else if (Instantiated != null)//두번째부터는 여기
            {
                for (int i = 0; i < Instantiated.Length; i++)
                {
                    Destroy(Instantiated[i]);// 기존에 있던 인스턴스들을 전부 지운다.
                }
                Instantiated.Initialize();

                DoThis();
            }
        }
    }
    void DoThis()
    {
        ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback; //certification for unity must needed to read OPEN API

        string api_toFind = api + key + apiCode + page + pageSize + keyword + UnityEngine.WWW.EscapeURL(toFind, System.Text.Encoding.GetEncoding("euc-kr")); // euc-kr

        //Debug.Log(api_toFind);//디버깅용

        WebRequest wrGetURL = WebRequest.Create(api_toFind);

        using (StreamReader reader = new StreamReader(wrGetURL.GetResponse().GetResponseStream(), System.Text.Encoding.GetEncoding("euc-kr"), true))
        {
            string xml = string.Empty;
            Debug.Log(reader.CurrentEncoding);
            xml = reader.ReadToEnd();
            //Debug.Log(name+"의 xml : " + xml);

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(xml);
            XmlElement root = doc.DocumentElement;

            XmlNodeList nodesForPrice = root.GetElementsByTagName("ProductPrice");//item price
            XmlNodeList nodesForName = root.GetElementsByTagName("ProductName");//item name
            XmlNodeList nodesForImage = root.GetElementsByTagName("ProductImage100");//item image
            XmlNodeList nodesForUrl = root.GetElementsByTagName("DetailPageUrl");//item detailURL


            Items = new Item[nodesForName.Count]; // 아이템 배열을 읽은 갯수만큼 만듬.
            for (int i = 0; i < nodesForName.Count; i++)
            {
                System.Text.Encoding euckr = System.Text.Encoding.GetEncoding(51949); //euc-kr = 51949 encoding  System.Text.Encoding.GetEncoding(51949)
                byte[] byteForName = euckr.GetBytes(nodesForName.Item(i).InnerText);
                string nameEncoded = euckr.GetString(byteForName); // encoded by euc-kr

                Items.SetValue(new Item(toFind, nameEncoded, nodesForPrice.Item(i).InnerText,
                    nodesForImage.Item(i).InnerText, nodesForUrl.Item(i).InnerText), i);//items에 해당 값을 차례대로 넣어준다.
            }

        }


        Instantiated = new GameObject[Items.Length];//가상공간에 생성하고자하는 물체의 수만큼, 배열을 생성한다.
        if (ItemInsTransform == null)//배열위치를 따로 안 정했으면, manager위치 기반으로하면 되긴한데, 사실 필요없을지도.
            ItemInsTransform = GetComponent<Transform>();

        for (int i = 0; i < Items.Length; i++)/*items 갯수만큼, 가상 공간에 물체를 배열하고, 물체의 데이터를 전달.*/
        {

            GameObject NEW = Instantiate(ItemInstance, ItemInsTransform.position + new Vector3(0, 0, 1.0f) * i, ItemInsTransform.rotation);/*아이템 생성*/
            Instantiated[i] = NEW;/*아이템을 instanticated 배열에 저장*/
            NEW.GetComponent<ItemInsScript>().SetItemValue(Items[i].GetCategory(), Items[i].GetName(),
                Items[i].GetPrice(), Items[i].GetImageURL(), Items[i].GetURL());/*아이템의 데이터를 전달*/

            NEW.transform.parent = transform; //부모를 이 manager로 설정. 부모 설정을 통해 각 manager가 독립적으로 아이템 품목을 설정 가능하고 그에 맞게 아이템을 배정할 수 있음.
            NEW.gameObject.tag = "Product"; //tag 설정
            NEW.AddComponent<BoxCollider>();
            NEW.GetComponent<BoxCollider>().size = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }


    /*이 코드는, 유니티내에서 URL 사용을 위해 인증을 위한 코드이므로, 분석할 필요가 없음.*/
    public bool MyRemoteCertificateValidationCallback(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        bool isOk = true;
        // If there are errors in the certificate chain, look at each error to determine the cause.
        if (sslPolicyErrors != SslPolicyErrors.None)
        {
            for (int i = 0; i < chain.ChainStatus.Length; i++)
            {
                if (chain.ChainStatus[i].Status != X509ChainStatusFlags.RevocationStatusUnknown)
                {
                    chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                    chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                    chain.ChainPolicy.UrlRetrievalTimeout = new System.TimeSpan(0, 1, 0);
                    chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                    bool chainIsValid = chain.Build((X509Certificate2)certificate);
                    if (!chainIsValid)
                    {
                        isOk = false;
                    }
                }
            }
        }
        return isOk;
    }

    public string GetImageURL(int i)
    {
        if (i < Items.Length)
        {
            if (Items != null)
            {
                return Items[i].GetImageURL();
            }
        }
        else if (i < 0)
        {
            Debug.Log("0 미만의 값임.");
        }
        else
        {
            Debug.Log("아이템의 갯수를 넘어선 요청을 함");
        }

        return "";
    }
    public string GetURL(int i)
    {
        if (i < Items.Length)
        {
            if (Items != null)
            {
                return Items[i].GetURL();
            }
        }
        else if (i < 0)
        {
            Debug.Log("0 미만의 값임.");
        }
        else
        {
            Debug.Log("아이템의 갯수를 넘어선 요청을 함");
        }

        return "";
        
    }
    public string GetName(int i)
    {
        if (i < Items.Length)
        {
            if (Items != null)
            {
                return Items[i].GetName();
            }
        }
        else if (i < 0)
        {
            Debug.Log("0 미만의 값임.");
        }
        else
        {
            Debug.Log("아이템의 갯수를 넘어선 요청을 함");
        }

        return "";
    }
    public string GetCategory(int i)
    {
        if (i < Items.Length)
        {
            if (Items != null)
            {
                return Items[i].GetCategory();
            }
        }
        else if (i < 0)
        {
            Debug.Log("0 미만의 값임.");
        }
        else
        {
            Debug.Log("아이템의 갯수를 넘어선 요청을 함");
        }

        return "";

    }
    public string GetPrice(int i)
    {
        if (i < Items.Length)
        {
            if (Items != null)
            {
        return Items[i].GetPrice();
            }
        }
        else if (i < 0)
        {
            Debug.Log("0 미만의 값임.");
        }
        else
        {
            Debug.Log("아이템의 갯수를 넘어선 요청을 함");
        }

        return "";
    }

    public int GetItemLength()
    {
        if (Items != null)
            return Items.Length;
        else
            return -1;
    }

    void OnTriggerEnter(Collider other)
    {
        /*들어오면UI 생성*/
        if (other.tag == "Player")
        {
            isPlayerNear = true;
            ui = Instantiate(UIforBtn, UIpos.position, Quaternion.identity);
            ui.transform.SetParent(transform);

        }
        
    }
    void OnTriggerExit(Collider other)
    {
        /*빠져나가면 UI삭제*/
        if (other.tag == "Player")
        {
            isPlayerNear = false;
            Destroy(ui);
        }
    }

}
