using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetShoppingUI : MonoBehaviour
{


    /*shopping UI Manager에 붙을 예정.*/
    List<SaveShopping.Item> ShoppingItem;
    public RectTransform shoppingPrefab;
    public RectTransform content;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ReceiveItem(List<SaveShopping.Item> item)//아이템을 카트를 통해서 넣어주기 위해 만든 함수.
    {
        ShoppingItem = item;
        for (int i = 0; i < ShoppingItem.Count; i++)
        {
            AddScrollView(ShoppingItem[i]);
        }
    }

    public void AddScrollView(SaveShopping.Item item)/*장바구니 안에 들어간 아이템을 이용해서 스크롤 뷰를 만들때 사용하기 위한 함수.*/
    {
        GameObject productPrefab = Instantiate(shoppingPrefab.gameObject);
        productPrefab.transform.SetParent(content, false);
        for (int i = 0; i < ShoppingItem.Count; i++)
        {
            productPrefab.GetComponent<ShoppingPrefab>().InitProductPrefab(item);
        }
    }

    /*빠져나가면 UI삭제해주는 기능*/
    void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Player")
        //    Debug.Log("OK");
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("bye");
            GetComponent<Rigidbody>().useGravity = true;
            Destroy(gameObject, 2.0f);
        }
    }
}
