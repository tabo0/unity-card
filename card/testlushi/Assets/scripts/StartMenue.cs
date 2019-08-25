using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System;
public class StartMenue : MonoBehaviour
{
    
    public VideoPlayer movTexture;
    public bool isDrawMov = true;

    public bool isShowMessage = false;

    public TweenScale logoTweenScale;
    public Camera camera1,camera2;
    public bool isCanShowSelectRolePanel = false;

    public TweenPosition selectRoleTween;

    public  UISprite heroSprite;
    void Awake()
    {
        heroSprite = GameObject.Find("hero0").GetComponent<UISprite>();
    }
    // Start is called before the first frame update
    void Start()
    {
        movTexture = GetComponent<VideoPlayer>();
        movTexture.Prepare();
        movTexture.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if (isDrawMov)
        {
            if (Input.GetMouseButtonDown(0) && isShowMessage == false)
            {
                isShowMessage = true;
            }
            else if (Input.GetMouseButtonDown(0) && isShowMessage == true)
            {
                StopMov();
            }
        }
        
        if (movTexture.frame>=0&&(ulong)movTexture.frame >= movTexture.frameCount)
        {
            StopMov();
        }
        //Debug.Log(movTexture.frame + "   "+ movTexture.frameCount);
        if (isCanShowSelectRolePanel)
        {
            if (Input.GetMouseButtonDown(0))
            {
                selectRoleTween.PlayForward();
                isCanShowSelectRolePanel = false;
            }
        }
    }
    void OnGUI()
    {
        if (isDrawMov)
        {
            //GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), movTexture);
            if (isShowMessage)
            {
                GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 40), "再点击一次屏幕退出动画的播放");
            }
        }
    }

    private void StopMov()
    {
        movTexture.Stop();
        isDrawMov = false;

        logoTweenScale.PlayForward();

        camera1.enabled = false;
        camera2.enabled = true;
    }
    public  void OnLogoTweenFinished()
    {
        isCanShowSelectRolePanel = true;
    }

    public void OnPlayButtonClick()
    {
        BlackMask._instance.Show();
        VSShow._instance.Show(heroSprite.spriteName, "hero" + UnityEngine.Random.Range(1, 10));
        StartCoroutine(LoadNextLevel());
    }


    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(2);
        Application.LoadLevel(1);
    }

}
