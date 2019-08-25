using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum VSState
{
    hero1,
    hero2
}
public class buttonend : MonoBehaviour
{
    public VSState vsState = VSState.hero1;
    private UIButton self;
    private UILabel label;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   

    void Awake()
    {
        self = this.GetComponent<UIButton>();
        label = transform.Find("Label").GetComponent<UILabel>();
    }


    public void BeClick()
    {
        self.SetState(UIButtonColor.State.Disabled, true);
        label.text = "对方回合";
    }
}
