using UnityEngine;
using System.Collections;

public class DragableCard : UIDragDropItem {

    protected override void OnDragDropRelease(GameObject surface) {
        base.OnDragDropRelease(surface);

        if ( surface!=null && surface.tag == "FightCard" && GameController.currenHeroName == "hero1") {
            //拖拽到了可发牌区域
            int need = this.GetComponent<Card>().needCraystal;
               Hero1Craystal hero1 = GameObject.Find("Hero1Craystal").GetComponent<Hero1Craystal>();
              bool flag = hero1.Get(need);
            //bool flag = true;
            if (flag)
            {
            transform.parent.GetComponent<MyCard>().RemoveCard(this.gameObject);
                // surface.GetComponent<fightcard>().AddCard(this.gameObject);
                manage._instance.SubHero2Hp(this.GetComponent<Card>().harm);
                //Destroy(this.gameObject);
                transform.parent.GetComponent<MyCard>().delete(this.gameObject);
            }
            else
            {
                transform.parent.GetComponent<MyCard>().UpdateShow();
            }

        } else {
            transform.parent.GetComponent<MyCard>().UpdateShow();
        }
    }
	
}
