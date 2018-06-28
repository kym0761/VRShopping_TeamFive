using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelect : MonoBehaviour {

    public GameObject UI_Main;
    GameObject UI;
    GameObject toHold;
    public bool canHold = true;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))//마우스 좌클릭 할 시
        {
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "Product")
            {

                if (UI == null)// UI를 처음 만드는 것이면 이것.
                {
                    /*감지된 아이템의 정보를 빼옴*/
                    string name = hitInfo.transform.GetComponent<ItemInsScript>().GetName();
                    string category = hitInfo.transform.GetComponent<ItemInsScript>().GetCategory();
                    string URL = hitInfo.transform.GetComponent<ItemInsScript>().GetURL();
                    string imageURL = hitInfo.transform.GetComponent<ItemInsScript>().GetImageURL();
                    string price = hitInfo.transform.GetComponent<ItemInsScript>().GetPrice();
                    Texture2D image = hitInfo.transform.GetComponent<ItemInsScript>().GetImage();

                    /*그 아이템의 정보로 UI를 생성함.*/
                    UI = Instantiate(UI_Main, gameObject.transform.position + new Vector3(0.0f, 0.0f, 2.5f), gameObject.transform.rotation);
                    UI.GetComponent<SetUI>().MakeItem(category, name, price, imageURL, URL, image);
                    UI.GetComponent<SetUI>().SetImage(image);
                    UI.GetComponent<SetUI>().SetName(name);

                }
                else // 현재 있는 UI를 없애버리고 새로운 UI가 뜨도록 만듬.
                {
                    Destroy(UI);
                    string name = hitInfo.transform.GetComponent<ItemInsScript>().GetName();
                    string category = hitInfo.transform.GetComponent<ItemInsScript>().GetCategory();
                    string URL = hitInfo.transform.GetComponent<ItemInsScript>().GetURL();
                    string imageURL = hitInfo.transform.GetComponent<ItemInsScript>().GetImageURL();
                    string price = hitInfo.transform.GetComponent<ItemInsScript>().GetPrice();
                    Texture2D image = hitInfo.transform.GetComponent<ItemInsScript>().GetImage();

                    UI = Instantiate(UI_Main, gameObject.transform.position + new Vector3(0.0f, 0.0f, 2.5f), gameObject.transform.rotation);
                    UI.GetComponent<SetUI>().MakeItem(category, name, price, imageURL,URL, image);

                    UI.GetComponent<SetUI>().SetImage(image);
                    UI.GetComponent<SetUI>().SetName(name);

                }
            }
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "WantToSelect" && canHold ==true)
            {
                toHold = hitInfo.transform.gameObject;
                GameObject Camera = GameObject.Find("Main Camera");
                toHold.transform.SetParent(Camera.transform);

                canHold = false;

            }
        }
        if (toHold == null)
        {
            canHold = true;
        }

	}
}
