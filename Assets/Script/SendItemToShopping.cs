using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendItemToShopping : MonoBehaviour {

    public GameObject setRateUI;
    // Use this for initialization
    GameObject UI;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /*카트에 물건이 닿았을때 그 물건을 넣어주는 기능*/
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("there is something..");
        if (other.tag == "WantToSelect")
        {
            /*감지한 아이템*/
            ItemInsScript hit = other.gameObject.GetComponent<ItemInsScript>();

            /*ShoppingManager와 연결하고 아이템값을 보내준다.*/
            GameObject manager = GameObject.Find("ShoppingManager");
            SaveShopping.Item item = new SaveShopping.Item(hit.GetCategory(), hit.GetName(), hit.GetPrice(), hit.GetImageURL(), hit.GetURL(), hit.GetImage());
            manager.GetComponent<SaveShopping>().AddItem(item);

            /*별점 매겨주는 UI생성*/
            GameObject playerCamera = GameObject.Find("Main Camera");
            UI = Instantiate(setRateUI, playerCamera.transform.position + playerCamera.transform.forward * 5.0f + new Vector3(0,3,0), Quaternion.identity);
            UI.transform.GetChild(0).GetComponent<SliderValueChanged>();

            /*감지했던 아이템 삭제*/
            Destroy(hit.gameObject);
        }
    }
}
