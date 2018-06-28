using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ShoppingPrefabClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject itemIns;
    public GameObject rateUI;
    public Transform itemZenPos;
    static GameObject item;
    static GameObject UI;
    Button btn;
	// Use this for initialization
	void Start () {
        btn = transform.root.GetChild(0).GetChild(1).GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)//스크롤 뷰 안에 있는 장바구니 아이템을 클릭하면 발생하는 이벤트.
    {
        if (eventData.button == PointerEventData.InputButton.Left)//마우스 좌클릭하면, 해당하는 이벤트를 할 수 있음.
        {

            /*shopping Manager와 연결*/
            ShoppingPrefab sp = GetComponent<ShoppingPrefab>();
            /*아이템 및 UI 생성*/
            itemZenPos = transform.root.GetChild(1);
            if (item == null)
            {
                item = Instantiate(itemIns, itemZenPos.position, Quaternion.identity);
                item.GetComponent<ItemInsScript>().SetItemValue(sp.GetCategory(), sp.GetName(), sp.GetPrice(), sp.GetImageURL(), sp.GetURL());
                item.tag = "InShopping";
                item.GetComponent<Rigidbody>().useGravity = false;
                item.AddComponent<SphereCollider>();
                item.GetComponent<SphereCollider>().radius = 0.5f;

                UI = Instantiate(rateUI, itemZenPos.position, itemZenPos.rotation);
                UI.GetComponent<SetRateUI>().SetItem(GetComponent<ShoppingPrefab>().GetStarValue());
                UI.GetComponent<SetRateUI>().categoryTxt.text = sp.GetCategory();
                btn.onClick.AddListener(delegate { TaskOnClick(sp.GetURL()); });
            }
            else//UI와 아이템이 존재할떄 그것을 삭제해주고 다시 기능을 사용함.
            {
                btn.onClick.RemoveAllListeners();
                Destroy(item);
                Destroy(UI);

                item = Instantiate(itemIns, itemZenPos.position, Quaternion.identity);
                item.GetComponent<ItemInsScript>().SetItemValue(sp.GetCategory(), sp.GetName(), sp.GetPrice(), sp.GetImageURL(), sp.GetURL());
                item.tag = "InShopping";
                item.GetComponent<Rigidbody>().useGravity = false;
                item.AddComponent<SphereCollider>();
                item.GetComponent<SphereCollider>().radius = 0.5f;

                UI = Instantiate(rateUI, itemZenPos.position, itemZenPos.rotation);
                UI.GetComponent<SetRateUI>().SetItem(gameObject.GetComponent<ShoppingPrefab>().GetStarValue());
                UI.GetComponent<SetRateUI>().categoryTxt.text = sp.GetCategory();
                btn.onClick.AddListener(delegate { TaskOnClick(sp.GetURL()); });
            }

        }
    }
    void OnDestroy() //이 UI는 장바구니에서 해당 아이템이 사라졌을때 같이 사라짐. 
    {
        Destroy(item);
        Destroy(UI);
    }
    void TaskOnClick(string URL) //URL 좌표 오픈
    {
        if (URL != null)
        {
            Application.OpenURL(URL);
        }
    }
}
