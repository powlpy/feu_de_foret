using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inflammable : MonoBehaviour {

    private float fireValue = 0;                // 0 : no fire          100 : max fire
    private float conditionValue = 1000;        // 1000 : intact tree      0 : burnt tree
    private ParticleSystem myFireEffect;
    private bool wasBurning = false;
    private float maxDistance = 5f;             // max distance to pass fire
    private  MyStatistics myStatistics;
    private List<Inflammable> closeTrees = new List<Inflammable>();     //trees to pass fire to
    private float inflammability;
    private bool isBurnt = false;
    private int watered = 0;
    private int status = 0; //0: intact, 1: damaged, 2: burnt

    void Awake() {

        myStatistics = GameObject.Find("Global").GetComponent<MyStatistics>();

        //désactiver les particules
        myFireEffect = gameObject.GetComponent<ParticleSystem>();
        myFireEffect.Stop();
        
        //random z-axis rotation pour plus de diversité
        transform.Find("Visual").Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));

        //random scale pour plus de diversité
        float mySize = Random.Range(-2f, 2f);
        transform.Find("Visual").localScale += new Vector3(mySize, mySize, mySize);

        inflammability = Random.Range(1.1f, 1.35f);
    }


    void Start() {
        //Mettre à jour le nombre d'arbres
        myStatistics.IncrementNbTree();
        Vector3 f1 = transform.position;
        Vector3 direction = new Vector3(Mathf.Cos(GlobalVariables.windDirection), Mathf.Sin(GlobalVariables.windDirection));
        float focalDist = GlobalVariables.windPower * GlobalVariables.minRadiusFire / 25;
        Vector3 f2 = f1 + (direction * focalDist);
        maxDistance = focalDist + GlobalVariables.minRadiusFire; // modification de la distance max
                                                                 //Tableau contenant les colliders proches
        Collider[] closeColliders = Physics.OverlapSphere(transform.position, maxDistance);
        //Pour chacun d'entre eux
        foreach (Collider closeCollider in closeColliders) {
            //Recuperer le composant inflammable
            Inflammable closeInflammable = closeCollider.GetComponentInParent<Inflammable>();
            if (closeInflammable != null && closeInflammable != this) {     //si non nul et non this

                if (((f1 - closeInflammable.transform.position).magnitude +
                    (f2 - closeInflammable.transform.position).magnitude) <= maxDistance) { // s'il se trouve dans l'ellipse
                    AddCloseTree(closeInflammable);         //ajouter le voisin à this
                }
                Vector3 of1 = closeInflammable.transform.position;
                Vector3 of2 = of1 + (direction * focalDist);
                if (((of1 - transform.position).magnitude +
                    (of2 - transform.position).magnitude) <= maxDistance) {
                    closeInflammable.AddCloseTree(this);    //ajouter this au voisin
                }
            }
        }
    }


    void Update() {
        if (GlobalVariables.State == 0) return;
        if (isBurnt) return;
        UpdateFire();
        RenderFire();
    }

    void UpdateFire() {

        //le feu evolue
        fireValue += (0.005f * fireValue) * (1-inflammability) * GlobalVariables.Speed * (1 - Mathf.Pow(conditionValue - 550, 2) / 200000f);
        fireValue -= watered * 0.12f * GlobalVariables.Speed;
        fireValue = Mathf.Clamp(fireValue, 0f, 100f);

        bool currentBurning = IsBurning();
        if (currentBurning && !wasBurning) StartFire();
        else if (!currentBurning && wasBurning) StopFire();
        wasBurning = currentBurning;

        //Le feu se répand aux voisins
        foreach (Inflammable closeTree in closeTrees)
            closeTree.PassFire(this);

        //L'arbre se dégrade
        if (IsBurning()) {
            conditionValue -= 0.01f * fireValue;
            conditionValue = Mathf.Clamp(conditionValue, 0f, 1000f);
            if(conditionValue == 0f) {
                isBurnt = true;
                StopFire();
            }
        }

        UpdateStats();

    }

    void RenderFire() {
        if (!IsBurning()) return;
        myFireEffect.startLifetime = (1f * fireValue / 100f) / GlobalVariables.Speed;

        UpdateMaterials();

    }


    void StartFire() {
        myFireEffect.Play();

    }

    void StopFire() {
        myFireEffect.Stop();

    }

    void UpdateMaterials() {
        float myCutoff = 1f - conditionValue / 2000f;
        transform.Find("Visual").gameObject.GetComponent<Renderer>().materials[4].SetFloat("_Cutoff", myCutoff);
        float greyLevel = conditionValue / 1000f;
        transform.Find("Visual").gameObject.GetComponent<Renderer>().materials[0].color = new Color(greyLevel, greyLevel, greyLevel, 1f);
        transform.Find("Visual").gameObject.GetComponent<Renderer>().materials[1].color = new Color(greyLevel, greyLevel, greyLevel, 1f);
        transform.Find("Visual").gameObject.GetComponent<Renderer>().materials[2].color = new Color(greyLevel, greyLevel, greyLevel, 1f);
        transform.Find("Visual").gameObject.GetComponent<Renderer>().materials[3].color = new Color(greyLevel, greyLevel, greyLevel, 1f);

    }

    //Reçoit le feu de ses voisins
    public void PassFire(Inflammable foreignTree) {

        float foreignFire = foreignTree.GetComponent<Inflammable>().fireValue;
        float distance = Vector3.Distance(gameObject.transform.position, foreignTree.GetComponentInChildren<Collider>().ClosestPointOnBounds(gameObject.transform.position));

        fireValue += (foreignFire * 0.0001f) * inflammability * GlobalVariables.Speed * (maxDistance - distance) / maxDistance;
        fireValue = Mathf.Clamp(fireValue, 0f, 100f);

    }

    public void AddCloseTree(Inflammable closeTree) {
        if (!closeTrees.Contains(closeTree))
            closeTrees.Add(closeTree);
    }

    public void Ignite() {
        fireValue = 30f;
    }

    bool IsBurning() {
        return fireValue > 15f;
    }

    public void Watered() {
        watered++;
    }

    void UpdateStats() {
        if(status == 0 && conditionValue < 850) {
            status = 1;
            myStatistics.AddDamaged();
        }else if(status == 1 && conditionValue < 150) {
            status = 2;
            myStatistics.AddBurnt();
        }
    }

}
