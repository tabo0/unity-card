using UnityEngine;
using System.Collections;

public class CardGenerator : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform fromCard;
    public Transform toCard;
    public string[] cardNames;

    public float transformTime = 0.5f;
    public int transformSpeed = 20;

    private bool isTransforming = false;
    private float timer = 0;
    private UISprite nowGenerateCard;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RandomGenerateCard();
        }

        if (isTransforming)
        {
            timer += Time.deltaTime;
            int index = (int)(timer / (1f / transformSpeed));
            index %= cardNames.Length;//17
            nowGenerateCard.spriteName = cardNames[index];

            if (timer > transformTime)
            {
                //变换结束
                //随机生成一个卡牌名字
                string cardName = cardNames[Random.Range(0, 5)];
                nowGenerateCard.spriteName = cardName;
                timer = 0;
                isTransforming = false;
            }
            nowGenerateCard.GetComponent<Card>().InitProperty();
            nowGenerateCard.GetComponent<Card>().ResetShow();
        }
    }

    public GameObject RandomGenerateCard()
    {

        GameObject go = NGUITools.AddChild(this.gameObject, cardPrefab);
        go.transform.position = fromCard.position;
        nowGenerateCard = go.GetComponent<UISprite>();
        iTween.MoveTo(go, toCard.position, 1f);
        isTransforming = true;
        return go;
    }
}
