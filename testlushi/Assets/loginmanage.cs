using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;//首先要记得引用命名空间
public class loginmanage : MonoBehaviour
{

    public InputField inputField;
    public static string myname, yourname;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void loginClik()
    {
        string name = inputField.text;

        Debug.Log(name);

        loginsocket.login(name);
        myname = name;
    }
    public void entergame()
    {
        string str = "0,"+myname+","+inputField.text;

        Debug.Log(inputField.text);
        loginsocket.send(str);

        yourname = inputField.text;
        SceneManager.LoadScene("playing");
    }
}
