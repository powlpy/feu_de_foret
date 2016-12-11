using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MyStatistics : MonoBehaviour {

	private int NbTrees, NbDamagedTrees, NbBurntTrees;
	private float TotalDamages;
    private GameObject TextIntactTrees, TextDamagedTrees, TextBurntTrees, TextNbTrees, TextTotalDamages;

    public void Awake() {
        NbTrees = 0;
        NbDamagedTrees = 0;
        NbBurntTrees = 0;
		TotalDamages = 0f;

        TextIntactTrees = GameObject.Find("TextStats1");
        TextDamagedTrees = GameObject.Find("TextStats2");
        TextBurntTrees = GameObject.Find("TextStats3");
        TextNbTrees = GameObject.Find("TextStats4");
		TextTotalDamages = GameObject.Find ("TextStats5");
    }

    public void Start() {
        Reset();
        UpdateStats();
    }

    public void Reset() {
        NbTrees = 0;
        NbDamagedTrees = 0;
        NbDamagedTrees = 0;
		TotalDamages = 0f;
    }

    public void IncrementNbTree() {
        NbTrees++;
        UpdateNbTree();
    }

    void UpdateNbTree() {
        TextNbTrees.GetComponent<Text>().text = "Nb trees :         \t" + NbTrees.ToString();
    }

    void UpdateStats() {

        TextIntactTrees.GetComponent<Text>().text = "Intact trees :   \t" + GetPercentageIntactTrees();
        TextDamagedTrees.GetComponent<Text>().text = "Damaged trees : \t" + GetPercentageDamagedTrees();
        TextBurntTrees.GetComponent<Text>().text = "Burnt trees :     \t" + GetPercentageBurntTrees();
		TextTotalDamages.GetComponent<Text>().text = "Damages :      \t" + GetPercentageTotalDamages();

    }

    public void AddDamaged() {
        NbDamagedTrees++;
    }

    public void AddBurnt() {
        NbDamagedTrees--;
        NbBurntTrees++;
    }

    public int GetNbIntactTrees() {
        return NbTrees - (NbDamagedTrees + NbBurntTrees);
    }

    string GetPercentageIntactTrees() {
        float percentage;
        if(NbTrees > 0)
            percentage = Mathf.Round((float)GetNbIntactTrees() / (float)NbTrees * 100f);
        else
            percentage = 100f;

        return percentage.ToString() + " %";
    }

    string GetPercentageDamagedTrees() {
        float percentage;
        if (NbTrees > 0)
            percentage = Mathf.Round(NbDamagedTrees / (float)NbTrees * 100f);
        else
            percentage = 0f;

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

	string GetPercentageTotalDamages() {
		float percentage;
		if (TotalDamages > 0) {
			percentage = Mathf.Round (TotalDamages / (float)NbTrees * 0.1f);
		} else {
			percentage = 0f;
		}
		return percentage.ToString () + " %";
	}

	public int GetNbBurnTrees(){
		return NbBurntTrees;
	}

	public int GetNbTrees(){
		return NbTrees;
	}

	public int GetNbDamagedTrees(){
		return NbDamagedTrees;
	}

	public float GetTotalDamage(){
		return TotalDamages;
	}

	public void AddTotalDamages(float damages){
		TotalDamages += damages;
		UpdateStats ();
	}
}
