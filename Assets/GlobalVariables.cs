using UnityEngine;
using System.Collections;

public class GlobalVariables : MonoBehaviour {
    
    public static float Speed = 1f;
    public static int State = 0;

    public static void NextState() {
        if(State == 0) {
            NextState0to1();
        }
    }

    static void NextState0to1() {
        State = 1;
        GameObject.Find("Instructions").SetActive(false);
        GameObject.Find("SliderSizeCircle").SetActive(false);
        GameObject.Find("SliderNbTreesCreated").SetActive(false);
    }

}
