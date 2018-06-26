using UnityEngine;
using System.Collections;

public class animaa : MonoBehaviour {

   
    private Animator ad;

	void Start ()
    {
        Animator ad = gameObject.GetComponent<Animator>();
        ad.StartRecording(1000);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}




}
