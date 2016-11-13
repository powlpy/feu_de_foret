using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MyStatistics : MonoBehaviour {

    private int NbTrees, NbBurningTrees, NbBurntTrees;
    private GameObject TextIntactTrees, TextBurningTrees, TextBurntTrees, TextNbTrees;

    public void Awake() {
        NbTrees = 0;
        NbBurningTrees = 0;
        NbBurningTrees = 0;

        TextIntactTrees = GameObject.Find("TextStats1");
        TextBurningTrees = GameObject.Find("TextStats2");
        TextBurntTrees = GameObject.Find("TextStats3");
        TextNbTrees = GameObject.Find("TextStats4");
    }

    public void Start() {
        UpdateStats();
    }

    //Met a jour la donnée NbTree. Appelée à chaque création d'arbre
    public void IncrementNbTree() {
        NbTrees++;
        UpdateNbTree();
    }

    void UpdateNbTree() {
        TextNbTrees.GetComponent<Text>().text = "Nb trees :         \t" + NbTrees.ToString();
    }

    void UpdateStats() {

        TextIntactTrees.GetComponent<Text>().text = "Intact trees :   \t" + GetPercentageIntactTrees();
        TextBurningTrees.GetComponent<Text>().text = "Burning trees : \t" + GetPercentageBurningTrees();
        TextBurntTrees.GetComponent<Text>().text = "Burnt trees :     \t" + GetPercentageBurntTrees().ToString();

    }

    public void AddBurning() {
        NbBurningTrees++;
        UpdateStats();
    }

    public void AddBurnt() {
        NbBurningTrees--;
        NbBurntTrees++;
        UpdateStats();
    }

    int GetNbIntactTrees() {
        return NbTrees - (NbBurningTrees + NbBurntTrees);
    }

    string GetPercentageIntactTrees() {
        float percentage;
        if(NbTrees > 0) {
            percentage = Mathf.Round((float)GetNbIntactTrees() / (float)NbTrees * 100f);
        }else {
            percentage = 100f;
        }
        return percentage.ToString() + " %";
    }

    string GetPercentageBurningTrees() {
        float percentage;
        if (NbTrees > 0) {
            percentage = Mathf.Round(NbBurningTrees / (float)NbTrees * 100f);
        } else {
            percentage = 0f;
        }
        return percentage.ToString() + " %";
    }

    string GetPercentageBurntTrees() {
        float percentage;
        if (NbTrees > 0) {
            percentage = Mathf.Round(NbBurntTrees / (float)NbTrees * 100f);
        } else {
            percentage = 0f;
        }
        return percentage.ToString() + " %";
    }

}
