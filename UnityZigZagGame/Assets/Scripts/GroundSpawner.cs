using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] GameObject sonZemin;

    private void Start()
    {
        for(int i = 1; i< 10; i++)
        {
            ZeminOluştur();
        }
    }

    public void ZeminOluştur()
    {
        Vector3 yon;

        if (Random.Range(0, 2) == 0)//0 gelirse x eksenine zemin koy
            yon = Vector3.left;
        else
            yon = Vector3.back;// 1 gelirse y eksenine zemin koy
        sonZemin = Instantiate(sonZemin, sonZemin.transform.position + yon, sonZemin.transform.rotation);
    }

}//class
