using UnityEngine;
using System.Collections;

public class LaneCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.transform.tag == "d") 
		{
			col.transform.parent = gameObject.transform.GetChild(0).transform;
		}
	}
}
