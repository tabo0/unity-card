using UnityEngine;
using System.Collections;

public class DesCard : MonoBehaviour {

    public static DesCard _instance;
    public float showTime=3f;

    private UISprite sprite;
    private float timer = 0;
    private bool isShow = false;
    private UILabel hpLabel;
    private UILabel harmLabel;
    void Awake() {
        _instance = this;
        sprite = this.GetComponent<UISprite>();
        //this.gameObject.SetActive(false);
        sprite.alpha = 0;

        hpLabel = transform.Find("hpLabel").GetComponent<UILabel>();
        harmLabel = transform.Find("harmLabel").GetComponent<UILabel>();
    }

    void Update() {
        if (isShow) {
            timer += Time.deltaTime;
            if (timer > showTime) {
                sprite.alpha = 0;
                timer = 0;
                isShow = false;
            } else {
                sprite.alpha = (showTime - timer) / showTime;
            }
        }
    }

    public void ShowCard(string cardname) {
        this.gameObject.SetActive(true);
        sprite.spriteName = cardname;
        //iTween.FadeTo(this.gameObject, 0, 3f);
        sprite.alpha = 1;
        isShow = true;
        timer = 0;

        InitProperty();
    }

    public void InitProperty()
    {//初始化属性 包括 水晶数量 伤害 血量
        string spriteName = sprite.spriteName;

      int   harm = spriteName[7] - '0';
      int   hp = spriteName[9] - '0';
        // Debug.Log(spriteName + "   " + spriteName[5]);

        harmLabel.text = harm + "";
        hpLabel.text = hp + "";
    }

}
