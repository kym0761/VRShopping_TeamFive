using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateRate : MonoBehaviour {


    float? average;
    public List<SaveShopping.Item> ShoppingItem;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void Calcurate(string category)
    {
        float sum=0;
        int count = 0;
        for (int i = 0; i < ShoppingItem.Count; i++)
        {
            if (ShoppingItem[i].GetCategory() == category)
            {
                sum += ShoppingItem[i].GetStarValue();
                count++;
            }

        }
        if (count != 0)
        {
            average = sum / count;
        }
    }
}
