using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class fightcard : MonoBehaviour
{
    public Transform card01, card02;
    private float xoffset = 0;
    private List<GameObject> cardList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        xoffset = card02.position.x - card01.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddCard(GameObject go)
    {
        go.transform.parent = this.transform;
        cardList.Add(go);
        Vector3 pos = CalPosition();
        iTween.MoveTo(go, pos, 0.5f);
    }
    Vector3 CalPosition()
    {
        int index = cardList.Count;
        if (index % 2 == 0)
        {
            float m = (index / 2) * xoffset;
            Vector3 pos = new Vector3(card01.position.x - m, card01.position.y, card01.position.z);
            return pos;
        }
        else
        {
            float m = (index / 2) * xoffset;
            Vector3 pos = new Vector3(card01.position.x + m, card01.position.y, card01.position.z);
            return pos;
        }
    }
}
