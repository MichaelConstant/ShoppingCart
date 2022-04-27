using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitInstantiation : MonoBehaviour
{
    public GameObject[] Commodity;
    [SerializeField] float Timer;

    // Start is called before the first frame update
    void Start()
    {
        Commodity[Random.Range(0, Commodity.Length)].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if(Timer>=4f)
        {
            Commodity[Random.Range(0, Commodity.Length)].SetActive(true);
        }
        if (Timer >= 8f)
        {
            Commodity[Random.Range(0, Commodity.Length)].SetActive(true);
        }
        if (Timer >= 12f)
        {
            Commodity[Random.Range(0, Commodity.Length)].SetActive(true);
        }
        if (Timer >= 16f)
        {
            Commodity[Random.Range(0, Commodity.Length)].SetActive(true);
        }
        if (Timer >= 20f)
        {
            Commodity[Random.Range(0, Commodity.Length)].SetActive(true);
        }
       
    }
}
