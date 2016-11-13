using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inflammable : MonoBehaviour {

    public float fireValue = 0;
    private float maxFireValue = 120f;
    private ParticleSystem myFireEffect;
    private bool onFire = false;
    private bool isBurnt = false;
    private float maxDistance = 5f;
    private  MyStatistics myStatistics;
    private List<Inflammable> closeTrees = new List<Inflammable>();

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

    }

    void Start() {

        //Mettre à jour le nombre d'arbres
        myStatistics.IncrementNbTree();

        //Tableau contenant les colliders proches
        Collider[] closeColliders = Physics.OverlapSphere(transform.position, maxDistance);
        //Pour chacun d'entre eux
        foreach (Collider closeCollider in closeColliders) {
            //Recuperer le composant inflammable
            Inflammable closeInflammable = closeCollider.GetComponentInParent<Inflammable>();
            if (closeInflammable != null && closeInflammable != this) {     //si non nul et non this
                AddCloseTree(closeInflammable);         //ajouter le voisin à this
                closeInflammable.AddCloseTree(this);    //ajouter this au voisin
            }
        }
    }

    void Update() {
        if(GlobalVariables.State == 0) {
            return;
        }
        if(GlobalVariables.Speed == 0f) {
            return;
        }
        UpdateFire();
        RenderFire();

    }
    
    //Evolue et répand le feu de l'arbre
    void UpdateFire() {

        if(fireValue < 10f) {
            return;
        }

        //Le feu évolue
        fireValue += (0.002f * fireValue) * GlobalVariables.Speed;
        if(fireValue > maxFireValue) {
            fireValue = maxFireValue;
        }
        //Le feu se répand
        foreach(Inflammable closeTree in closeTrees) {
            closeTree.PassFire(this);
        }

    }

    void RenderFire() {

        if(fireValue < 10 || isBurnt) {
            return;
        }

        if (!onFire) {
            myFireEffect.Play();
            onFire = true;
            myStatistics.AddBurning();
        }

        float greenValue = -2f / 185f * fireValue + 49f / 45f;
        myFireEffect.startColor = new Color(1.0f, greenValue, 0.2f, 1.0f);

        UpdateMaterials();

        if (fireValue == maxFireValue) {
            isBurnt = true;
            myFireEffect.Stop();
            myStatistics.AddBurnt();
        }

    }

    void UpdateMaterials() {

        float myCutoff = 1f / 180f * fireValue + 4f / 9f;
        transform.Find("Visual").gameObject.GetComponent<Renderer>().materials[4].SetFloat("_Cutoff", myCutoff);
        float greyLevel = 1f - fireValue / 100f + Random.Range(0.1f,0.3f);
        transform.Find("Visual").gameObject.GetComponent<Renderer>().materials[0].color = new Color(greyLevel, greyLevel, greyLevel, 1f);
        transform.Find("Visual").gameObject.GetComponent<Renderer>().materials[1].color = new Color(greyLevel, greyLevel, greyLevel, 1f);
        transform.Find("Visual").gameObject.GetComponent<Renderer>().materials[2].color = new Color(greyLevel, greyLevel, greyLevel, 1f);
        transform.Find("Visual").gameObject.GetComponent<Renderer>().materials[3].color = new Color(greyLevel, greyLevel, greyLevel, 1f);


    }

    //Reçoit le feu de ses voisins
    public void PassFire(Inflammable foreignTree) {

        float foreignFire = foreignTree.GetComponent<Inflammable>().fireValue;
        float distance = Vector3.Distance(gameObject.transform.position, foreignTree.GetComponentInChildren<Collider>().ClosestPointOnBounds(gameObject.transform.position));

        if (foreignFire < 10 || fireValue > 15) {
            return;
        }

        if(foreignFire > fireValue) {
            fireValue += (foreignFire * 0.002f) * GlobalVariables.Speed * (maxDistance - distance) / maxDistance;
        }

    }

    public void AddCloseTree(Inflammable closeTree) {
        if (!closeTrees.Contains(closeTree)) {
            closeTrees.Add(closeTree);
        }
    }

}
