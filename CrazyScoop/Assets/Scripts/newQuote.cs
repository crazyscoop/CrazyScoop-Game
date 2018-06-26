using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class newQuote : MonoBehaviour {

    public static newQuote nq;

    [SerializeField]
    List<Material> fakemat;

    public List<Material> quotematerial;

    public int quoteindex;

	void Start ()
    {
        nq = this;
	}
	
	
	void Update () {
	
	}

    void OnEnable()
    {

     for(int i = 0; i < fakemat.Count;i++)
     {
            Material tempmaterial = fakemat[i];
            int j = Random.Range(i, fakemat.Count);
            fakemat[i] = fakemat[j];
            fakemat[j] = tempmaterial;
     }
        quotematerial = fakemat;

    }

}
