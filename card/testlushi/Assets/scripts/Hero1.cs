using UnityEngine;
using System.Collections;

public class Hero1 : Hero {
    void Start() {
       // hpCount = 0;
        string heroName = PlayerPrefs.GetString("hero1");
      //  sprite.spriteName = heroName;
    }
}
