using UnityEngine;
using System.Collections;

public class Hero2Crystal : MonoBehaviour {

    public int usableNumber = 1;
    public int totalNumber = 1;
    public int maxNumber = 10;
    private UILabel label;

    void Awake() {
        label = this.GetComponent<UILabel>();
    }
    void Start()
    {
        GameController._instance.OnNewRound += this.OnNewRound;
    }
    void Update()
    {
        UpdateShow();
    }
    public void UpdateShow() {
        label.text = usableNumber + "/" + totalNumber;
    }
    public void Refresh()
    {
        if (totalNumber < maxNumber) totalNumber++;
        usableNumber = totalNumber;
        UpdateShow();
    }
    public bool Get(int x)  //消耗水晶
    {
        if (usableNumber >= x)
        {
            usableNumber -= x;
            UpdateShow();
            return true;
        }
        else
        {
            return false;
        }
    }
    public void OnNewRound(string heroName)
    {
        if (heroName == "hero2")
        {
            if(GameController._instance.roundindex>= GameController.second)
            Refresh();
        }
    }

}
