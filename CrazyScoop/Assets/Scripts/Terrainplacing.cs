using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Terrainplacing : MonoBehaviour {

    [SerializeField]
    private List<GameObject> house;


    private int index; 

	
	void Start()
    {
  
	}
	
	
	void Update ()
    {

	}

    void OnEnable()
    {

            index = Random.Range(0, 4);
            house[index].SetActive(true);
    }

    void OnDisable()
    {
            house[index].SetActive(false);

    }
}
