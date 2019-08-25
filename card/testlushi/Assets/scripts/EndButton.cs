using UnityEngine;
using System.Collections;

public class EndButton : MonoBehaviour
{

    private UILabel label;
    public static EndButton _instance;
    void Awake()
    {
        _instance = this;
        label = transform.Find("Label").GetComponent<UILabel>();
        
    }
    void Update()
    {
        if (GameController.currenHeroName == "hero1")
        {
            label.text = "结束回合";
        }
        else
        {
            label.text = "对方回合";
        }

    }
    void Start()
    {
        GameController._instance.OnNewRound += this.OnNewRound;
    }
    public void OnEndButtonClick()
    {
        if (label.text== "结束回合")
        {
            label.text = "对方回合";
            GameController._instance.TransformPlayer();
        }

    }
    public void setlable(string str)
    {
        label.text = str;
    }
    public void OnNewRound(string heroName)
    {
        if (heroName == "hero1")
        {
            label.text = "结束回合";
        }
    }
}
