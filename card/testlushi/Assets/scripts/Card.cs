using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {
    public int needCraystal;
    public int harm;
    public int hp;

    private UISprite sprite;
    private UILabel hpLabel;
    private UILabel harmLabel;

    private string CardName {

        get {
            return sprite.spriteName;
        }
    }
    void Awake() {
        sprite = this.GetComponent<UISprite>();
        hpLabel = transform.Find("hpLabel").GetComponent<UILabel>();
        harmLabel = transform.Find("harmLabel").GetComponent<UILabel>();

        InitProperty();
        ResetShow();
    }

    

    void OnPress(bool isPressed) {
        if (isPressed) {
            DesCard._instance.ShowCard( CardName ) ;
        }
      //  Debug.Log("bug");
    }

    public void SetDepth(int depth) {
        sprite.depth = depth;
        hpLabel.depth = depth + 1;
        harmLabel.depth = depth + 1;
    }

    public void ResetPos() {//更新血量和伤害的位置 
        harmLabel.GetComponent<UIAnchor>().enabled = true;
        hpLabel.GetComponent<UIAnchor>().enabled = true;
    }
    
    public void ResetShow() {
        //更新血量和伤害的显示
        harmLabel.text = harm + "";
        hpLabel.text = hp + "";
    }

    public void InitProperty() {//初始化属性 包括 水晶数量 伤害 血量
        string spriteName  = sprite.spriteName;

        needCraystal = spriteName[5] - '0';
        harm = spriteName[7] - '0';
        hp = spriteName[9] - '0';
        // Debug.Log(spriteName + "   " + spriteName[5]);

        harmLabel.text = harm + "";
        hpLabel.text = hp + "";
    }

}
