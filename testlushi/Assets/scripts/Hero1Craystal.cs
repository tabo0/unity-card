using UnityEngine;
using System.Collections;

public class Hero1Craystal : MonoBehaviour {

    public int maxNumber = 1;
    public int remainNumber = 1;

    public UISprite[] crystals;

    public int totalNumber;
    private UILabel label;

    void Awake() {
        totalNumber = crystals.Length;
        label = this.GetComponent<UILabel>();
    }
    void Start()
    {
        GameController._instance.OnNewRound += this.OnNewRound;
    }

    void Update() {
        UpdateShow();
    }
    public void Refresh()
    {
       if(maxNumber<totalNumber) maxNumber++;
        remainNumber = maxNumber;
        UpdateShow();
    }
    public bool Get(int x)  //消耗水晶
    {
        if (remainNumber >= x)
        {
            remainNumber -= x;
            UpdateShow();
            return true;
        }
        else
        {
            return false;
        }
    }
    public void UpdateShow() {
        for (int i = maxNumber; i < totalNumber; i++) {
            crystals[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < maxNumber; i++) {
            crystals[i].gameObject.SetActive(true);
        }
        for (int i = remainNumber; i < maxNumber; i++) {
            crystals[i].spriteName = "TextInlineImages_normal";
        }
        for (int i = 0; i < remainNumber; i++) {
            int numberName = i+1;
            string tempStr = "";
            if(numberName<=9){
                tempStr="0"+numberName;
            }else{
                tempStr=""+numberName;
            }

            crystals[i].spriteName = "TextInlineImages_" + tempStr;
        }

        label.text = remainNumber + "/" + maxNumber;
    }
    public void OnNewRound(string heroName)
    {
        if (heroName == "hero1"&& GameController._instance.roundindex >= GameController.first)
        {
            Refresh();
        }
    }

}
