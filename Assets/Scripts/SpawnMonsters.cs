using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnMonsters : MonoBehaviour
{
    public GameObject MonsterGroup1;
    public GameObject MonsterGroup2;
    public bool MutantTrigger1 = false;
    public GameObject Mutant1;
    public GameObject Mutant2;
    public GameObject Mutant3;
    public GameObject Mutant4;
    public GameObject Mutant5;
    public GameObject Mutant6;
    public GameObject Giant1;
    public GameObject Giant2;

    public GameObject MedianGate1;
    public GameObject[] MergeMutant = new GameObject[6];
    public bool MergeKnocked2 = false;
    public bool MergeKnocked3 = false;

    public GameObject MutantText1;

    public GameObject SpawnText;
    void Start()
    {
        MutantText1.SetActive(false);   
    }

    void Update()
    {
        if (MedianGate1 == null)
        {
            MonsterGroup1.SetActive(true);
            SpawnText.GetComponent<Text>().text = "knock out all the enemies to get to the median Gate!\n press the [ q ] key to knock out the  parasiites!\n use your mouse button to shoot down the giant!\npress the [u] key to summon a turret!";
        }

        if (MutantTrigger1)
        {
            if (Mutant1 != null)
            {
                Mutant1.SetActive(true);

            }
        }

        if (Giant1.GetComponent<Giant>().KnockedOut)
        {
            SpawnText.GetComponent<Text>().text = "";
        }

        if (Mutant1 != null)
        {

            if (Mutant1.GetComponent<Parasite>().Marked)
            {
                MutantText1.GetComponent<Text>().text = "Now That The Parasite is Marked, Press the [ T ] key to Merge it into a pink Merged Mutant";
            }
        }

        if (MergeMutant[0] != null && !MergeMutant[0].GetComponent<Parasite>().KnockedOut)
        {
            MutantText1.GetComponent<Text>().text = "Now That you've Created a Merged Mutant, Move up close to it and press the [ y ] key to use your divide sword and destroy it!";
        }
        
        if (MergeMutant[0] != null && MergeMutant[0].GetComponent<Parasite>().KnockedOut)
        {
            if (Mutant2 != null)
            {
                Mutant2.SetActive(true);
            }
            if (Mutant3 != null)
            {
                Mutant3.SetActive(true);
                MutantText1.GetComponent<Text>().text = "Now Mark both of these 2 parasites before you merge them. Hold down and release your mouse button to shoot charged blasts ";
            }
        }


        if (Mutant2 != null && Mutant3 != null)
        {

            if (Mutant2.GetComponent<Parasite>().Marked && Mutant3.GetComponent<Parasite>().Marked)
            {
                MutantText1.GetComponent<Text>().text = "Now That both Parasites are Marked, Press the [ T ] key to Merge them into a pink Merged Mutant";
            }
        }

        if (MergeMutant[1] != null && !MergeMutant[1].GetComponent<Parasite>().KnockedOut)
        {
            MutantText1.GetComponent<Text>().text = "You merged those two parasites into one pink Merged Mutant! Move up close to it and press the [ y ] key to use your divide sword and destroy it!";
        }
            
        if (MergeMutant[1] != null && MergeMutant[1].GetComponent<Parasite>().KnockedOut)
        {
            if (Mutant4 != null)
            {
                Mutant4.SetActive(true);
            }
            if (Mutant5 != null)
            {
                Mutant5.SetActive(true);
            }
            if (Mutant6 != null)
            {
                Mutant6.SetActive(true);
                MutantText1.GetComponent<Text>().text = "Now Mark All 3 of these parasites. Hold down and release your mouse button to shoot charged blasts ";
            }
        }

        if (Mutant4 != null && Mutant5 != null && Mutant6 != null)
        {

            if (Mutant4.GetComponent<Parasite>().Marked && Mutant5.GetComponent<Parasite>().Marked & Mutant6.GetComponent<Parasite>().Marked)
            {
                MutantText1.GetComponent<Text>().text = "Now That all 3 Parasites are Marked, Press the [ T ] key to Merge them into a pink Merged Mutant";
            }
        }


        if (MergeMutant[2] != null && !MergeMutant[2].GetComponent<Parasite>().KnockedOut)
        {
            MutantText1.GetComponent<Text>().text = "You merged all 3 of those parasites into one pink Merge Mutant! Move up close to it and press the [ y ] key to use your divide sword and destroy it!";
        }

        if (MergeMutant[2] != null && MergeMutant[2].GetComponent<Parasite>().KnockedOut)
        {
            MutantText1.GetComponent<Text>().text = "You Destroyed All the merge mutants! now move ahead and un- lock the median Gate. Shoot all the Points around the gate to get started";
        }


    }


    private void OnTriggerEnter(Collider Coll)
    {
        if (Coll.gameObject.transform.name == "Mutant Trigger 1")
        {
            MutantTrigger1 = true;
            MutantText1.SetActive(true);
        }
    
    }
}
