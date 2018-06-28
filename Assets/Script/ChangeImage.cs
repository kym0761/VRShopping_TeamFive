using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeImage : MonoBehaviour {

    /*이 코드와 Canvas라고 되어있는 prefab도 사용하지 않음.*/


    public Texture2D Img;
    public string URL;
    GameObject Manager;
    //static int ImageNumber = 0;
    float time;
	// Use this for initialization
	void Start () {
        /* // 여러개의 이미지를 바꿀때 필요했는데 지금 필요없을듯
        Manager = GameObject.Find(transform.parent.name);
        URL=Manager.GetComponent<XMLTest>().GetImageURL(ImageNumber);
        if (ImageNumber < Manager.GetComponent<XMLTest>().GetItemLength())
        {
            ImageNumber++;
        }
        else
        {
            ImageNumber = 0;
        }
            StartCoroutine(LoadImg(URL));
        */
        if(URL != null)
        StartCoroutine(LoadImg(URL));

	}
    public IEnumerator LoadImg(string ImageURL)
    {
        
        WWW w = new WWW(ImageURL);
        yield return w;
        Debug.Log(ImageURL);
        Img = w.texture;
        /*이미지를 바운받을때까지 대기하고, 이미지를 다 받으면, 해당 이미지의 2Dtexture를 추출함*/

        //추출한 이미지로 Sprite를 만들고 그 이미지를 붙여놓는다.
        Sprite wantToChange =Sprite.Create(Img, new Rect(0.0f, 0.0f, Img.width, Img.height), new Vector2(0.5f, 0.5f), 100.0f);
        
        GetComponent<Image>().sprite = wantToChange;



    }
    // Update is called once per frame
    void Update () {
        /*
        time += Time.deltaTime;
        if (time > 5.0f)
        {
            Destroy(gameObject); // 사진이 지워지는 것이 문제가 없는지 확인하기 위해 실험.
        }
        */
	}
    /*
    void OnGUI()
    {
        GUILayout.Label(Img);
    }
    */
}
