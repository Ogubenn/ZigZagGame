using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] GameObject sonZemin;
    [SerializeField] GameObject Coin;


    private void Start()
    {
        for(int i = 1; i< 20; i++)
        {
            ZeminOluştur();
        }
    }

    public void ZeminOluştur()
    {
        Vector3 yon;

        Vector3 yon2;

        if (Random.Range(0, 3) == 0)//0 gelirse x eksenine zemin koy
        {
            yon = Vector3.left;
            yon2 = Vector3.up;
            
            Instantiate(Coin,sonZemin.transform.position+yon2,Coin.transform.rotation);
        }
        else
            yon = Vector3.back;// 1 gelirse y eksenine zemin koy
        sonZemin = Instantiate(sonZemin, sonZemin.transform.position + yon, sonZemin.transform.rotation);
    }
}//class
