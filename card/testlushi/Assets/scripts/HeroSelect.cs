using UnityEngine;
using System.Collections;

public class HeroSelect : MonoBehaviour {

    private UISprite selectHeroImage;
    private UILabel selectHeroName;



    private string[] heroNames ={
                                    "吉安娜·普罗德摩尔（法师）",
"雷克萨（猎人）",
"乌瑟尔·光明使者（圣骑士）",
"加尔鲁什·地狱咆哮（战士）",
"玛法里奥·怒风（德鲁伊）",
"古尔丹（术士）",
"萨尔（萨满祭司）",
"安杜因·乌瑞恩（牧师）",
"瓦莉拉·萨古纳尔（潜行者）"
                               };

    void Awake() {
        selectHeroImage = this.transform.parent.Find("hero0").GetComponent<UISprite>();
        selectHeroName = this.transform.parent.Find("hero_name").GetComponent<UILabel>();

    }

    void OnClick() {
        string heroname = this.gameObject.name;
        selectHeroImage.spriteName = heroname;
        char heroIndexChar = heroname[heroname.Length - 1];
        
        int heroIndex = heroIndexChar - '0';
        selectHeroName.text = heroNames[heroIndex - 1];
    }

}
