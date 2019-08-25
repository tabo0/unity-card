using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

    public int maxHp = 30;
    public int minHp = 0;

    protected UISprite sprite;
    private UILabel hpLabel;
    public int hpCount = 30;

    void Awake() {
        sprite = this.GetComponent<UISprite>();

        hpLabel = this.transform.Find("hp").GetComponent<UILabel>();
    }

    void Update() {
        //   if (Input.GetKey(KeyCode.E)) {
        if (Input.GetKeyDown(KeyCode.E)){ 
            TakeDamage(1);
        }
        //    if (Input.GetKey(KeyCode.R)) {
        if (Input.GetKeyDown(KeyCode.R)){
            PlusHp(1);
        }
    }


    //这个方法用来吸收伤害值
    public bool gameover()
    {
        if (hpCount <= minHp)
        {
            return true;

        }
        return false;
    }
    public void TakeDamage(int damage) {
        hpCount -= damage;
        hpLabel.text = hpCount + "";

        if (hpCount <= minHp) {
            //处理游戏结束的逻辑
            
        }
    }
    public void PlusHp(int hp) {
        hpCount += hp;
        if (hpCount >= maxHp) {
            hpCount = maxHp;
        }
        hpLabel.text = hpCount + "";
    }

}
