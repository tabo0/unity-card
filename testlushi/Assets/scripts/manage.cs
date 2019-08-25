using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//首先要记得引用命名空间
public class manage : MonoBehaviour
{
    public static manage _instance;
    public Hero1 hero1;
    public Hero2 hero2;
    public UILabel showmynum, showyournum;
    private bool isShow1=false,isShow2=false;
    private float timer1=0, timer2=0,showTime=2f;
    public GameObject gamecontroller;
    public GameObject over;
    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        over.active = false;
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isShow1)
        {
            timer1 += Time.deltaTime;
            if (timer1 > showTime)
            {
                showmynum.alpha = 0;
                timer1 = 0;
                isShow1 = false;
            }
            else
            {
                showmynum.alpha = (showTime - timer1) / showTime;
            }
        }
        if (isShow2)
        {
            timer2 += Time.deltaTime;
            if (timer2 > showTime)
            {
                showyournum.alpha = 0;
                timer2 = 0;
                isShow2 = false;
            }
            else
            {
                showyournum.alpha = (showTime - timer2) / showTime;
            }
        }
        if (hero1.gameover())
        {
            over.active = true;
            over.GetComponent<UILabel>().text = "LOST";
            gamecontroller.active = false;
        }
        if (hero2.gameover())
        {
            gamecontroller.active = false;
            over.active = true;
            over.GetComponent<UILabel>().text = "WIN";
        }
    }
    public void SubHero1Hp(int x)
    {
        hero1.TakeDamage(x);
        isShow1 = true;
        timer1 = 0;
        showSubHero1Hp(x);  
    }
    public void SubHero2Hp(int x)
    {
       

        hero2.TakeDamage(x);
        isShow2 = true;
        timer2 = 0;
        showSubHero2Hp(x);


        loginsocket.send("1," + loginmanage.yourname + "," + x);
    }
    public void showSubHero1Hp(int x)
    {
        showmynum.text = "-"+x;
     //   showmynum.GetComponent<TweenAlpha>().PlayForward();
      //  TweenAlpha.Begin(showmynum, 1f, 0f);
    }
    public void showSubHero2Hp(int x)
    {
        showyournum.text = "-" + x;
       // showyournum.GetComponent<TweenAlpha>().PlayForward();
    }
    public void transforlogin()
    {
        GameController.currenHeroName = "hero1";
        SceneManager.LoadScene("login");
    }
}
