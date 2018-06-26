using UnityEngine;
using System.Collections;

public class Distroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
        

		if(col.tag == "d")
		{
            col.transform.gameObject.SetActive(false);
			Rigidbody colbody =  col.transform.gameObject.GetComponent<Rigidbody>();
            colbody.velocity = Vector3.zero;
			col.transform.parent = null;
		}

	}

}
