using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerTEST : MonoBehaviour {

    float lr = 0.01f; // hyperparameter 0.01이 무난함.
    int epoch = 100; // 몇번 학습할 것인가. 이정도가 적절?
    int factor = 10; // 이것도 hyperparameter Latent factor로 불리는거

    int user = 10;
    int product = 20;
    float?[,] p;
    float?[,] q;
    float?[,] r;


    /*사용자 값과 물품 값을 입력하는게 힘드므로, 임의로 지정한다.*/
    string[] productCategory =
    {
        "치킨", "가방", "신발", "셔츠", "바지",
        "양말", "장갑", "의자", "드레스","자켓",
        "부츠", "모자", "안경", "정장", "샌들",
        "넥타이","슬리퍼","감자","햄버거","피자"
    };

    // Use this for initialization
    void Start() {

        p = new float?[user+1, factor];
        q = new float?[product, factor];
        r = new float?[user+1, product];

        
        for (int a = 0; a < user+1; a++)
        {
            for (int b = 0; b < factor; b++)
            {
                p[a, b] = Random.Range(0.0f, 0.1f);
            }
        }

        for (int a = 0; a < product; a++)
        {
            for (int b = 0; b < factor; b++)
            {
                q[a, b] = Random.Range(0.0f, 0.1f);
            }
        }

        /*임의로 지정된 물품 및 사용자 값.*/
        r = new float?[,]{
            { 0.9f,null,null,0.5f,null,0.5f,null,0.2f,null,null,0.9f,null,0.7f,0.6f,null,null,null,0.1f,null,0.4f },
            { null,null,0.2f,0.5f,0.4f,null,null,null,0.8f,null,0.9f,null,null,null,null,null,null,0.4f,null,null },
            { null,0.3f,null,null,0.6f,null,0.5f,null,null,null,null,null,0.3f,null,null,0.1f,null,null,0.5f,null },
            { 0.9f,null,0.1f,null,0.9f,0.5f,null,null,0.5f,null,null,0.8f,null,0.6f,null,null,null,0.7f,null,0.6f },
            { 0.7f,0.4f,null,0.9f,null,null,0.8f,null,null,0.1f,null,0.9f,null,0.8f,null,0.4f,0.6f,0.7f,null,null },
            { null,0.5f,0.8f,0.5f,null,0.9f,0.4f,0.7f,0.8f,0.3f,0.6f,null,null,0.8f,null,null,null,null,null,0.9f },
            { null,null,null,0.8f,null,null,0.3f,null,null,0.4f,null,null,0.7f,0.5f,0.7f,null,0.4f,0.6f,null,0.3f },
            { 0.8f,null,null,null,0.6f,null,null,0.9f,null,null,0.7f,null,null,null,null,null,null,null,0.8f,null },
            { null,0.5f,null,null,null,0.5f,null,null,null,0.8f,null,null,null,null,0.5f,null,0.9f,null,0.2f,0.3f },
            { 0.7f,null,0.8f,null,0.8f,null,0.5f,null,0.3f,null,null,0.5f,null,null,0.7f,0.7f,0.6f,null,0.6f,null },
            { null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null }//사용자 값 텅텅 빔.
        };


    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.L)) // learn 작동
        {
            Learn();
        }

    }

   public float Estimate(int u, int i) //유저한테 추천해줄때 쓰는 아이템
    {
        if (u < user && i < product)// u와 i가 기존에 있던 유저와 제품이면
        {

            return DotProduct(p, q, u, i);
        }
        else //새로운 유저나 새로운 아이템이면? -> 패턴이 없으므로, 기존의 값을 더해서 평균을 낸다.
        {
            if (!(u < user))
            {
                float sum = 0.0f;
                int len = 0;
                for (int a = 0; a < user; a++)
                {

                    if (r[a, i] != null)
                    {
                        sum += (float)r[a, i];
                        len++;
                    }

                }//아직 문제가 있는 코드
                return sum / (float)len; // null이 아닌 모든 값의 합의 평균
            }
            else
            {
                float sum = 0.0f;
                int len = 0;
                for (int a = 0; a < product; a++)
                {

                    if (r[u, a] != null)
                    {
                        sum += (float)r[u, a];
                        len++;
                    }

                }

                return sum / (float)len; // null이 아닌 모든 값의 합의 평균
            }
             
        }
    }

    float DotProduct(float?[,] p, float?[,] q, int j, int k) // 내적을 위한 함수.
    {
        float sum = 0;

        for (int i = 0; i < factor; i++)
        {
            if (p[j, i] != null && q[k, i] != null)
                sum += (float)(p[j, i] * q[k, i]);
        }

        return sum;
    }

    void Learn ()//학습함수.
    {

        for (int i = 0; i < epoch; i++)
        {
            for (int j = 0; j < user; j++)
            {
                for (int k = 0; k < product; k++)
                {
                    if (r[j, k] != null)
                    {
                        float err = (float)r[j, k] - DotProduct(p, q, j, k);

                        //p[j]와 q[k] 를 SGD 를 이용해서 업데이트
                        for (int a = 0; a < factor; a++)
                        {
                            float? temp = p[j, a];
                            p[j, a] += lr * err * q[k, a];
                            q[k, a] += lr * err * temp;
                        }
                        //주의 new q[k] = lr *err * old p[j] 임

                    }
                }
            }
        }
    }

    public int FindProductIndex(string category) // 카테고리에 해당하는 위치가 어디인지 알기 위해 만든 함수.
    {
        int index = -1; // -1이 나오면 해당 index를 못 찾은 것이다.
        for (int i = 0; i < productCategory.Length; i++)
        {
            if (productCategory[i] == category)
            {
                index = i;
                break;
            }
        }
        return index;
    }

}
