using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MyStatistics : MonoBehaviour {

    private int NbTrees, NbBurningTrees, NbBurntTrees;
    private GameObject TextIntactTrees, TextBurningTrees, TextBurntTrees;

    // Use this for initialization
    void Start () {

        TextIntactTrees = GameObject.Find("TextStats1");
        TextBurningTrees = GameObject.Find("TextStats2");
        TextBurntTrees = GameObject.Find("TextStats3");

        NbTrees = GameObject.FindGameObjectsWithTag("Tree").Length;
        NbBurningTrees = 0;
        NbBurntTrees = 0;

        UpdateStats();
        
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
        float percentage = Mathf.Round((float)GetNbIntactTrees() / (float)NbTrees * 1000f) / 10f;
        return percentage.ToString() + " %";
    }

    string GetPercentageBurningTrees() {
        float percentage = Mathf.Round((float)NbBurningTrees / (float)NbTrees * 1000f) / 10f;
        return percentage.ToString() + " %";
    }

    string GetPercentageBurntTrees() {
        float percentage = Mathf.Round((float)NbBurntTrees / (float)NbTrees * 1000f) / 10f;
        return percentage.ToString() + " %";
    }

}
