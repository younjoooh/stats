using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OrderNumbers : MonoBehaviour
{
    public int[] RandomNumbers = new int[8];
    public int[] OrderedNumbers;


    void Start()
    {
        for (int i = 0; i <= 7; i++)
        {
           RandomNumbers[i] = UnityEngine.Random.Range(1,100);
        }

        
        OrderedNumbers = (int[])RandomNumbers.Clone();
        System.Array.Sort(OrderedNumbers);

        for (int i = 0; i <= 7; i ++)
        {
            print(OrderedNumbers[i]);
        }
        
        
    }

}
