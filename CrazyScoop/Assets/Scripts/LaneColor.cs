using UnityEngine;
using System.Collections;

public class LaneColor : MonoBehaviour {

	[SerializeField]
	private Material arrowMaterial1;

	[SerializeField]
	private Material arrowMaterial2;

	[SerializeField]
	private Material arrowMaterial3;

	[SerializeField]
	private Material arrowMaterial4;

	

	private Rigidbody myRigidbody;

	void Start () 
	{
	
		 myRigidbody = GetComponent<Rigidbody> ();
	}
	

	void Update () 
	{
		if (myRigidbody.IsSleeping () == true) 
		{
			myRigidbody.WakeUp();
		}
	}

	void OnCollisionStay(Collision col)
	{
		if (col.transform.name == "Lane1")
		{
			arrowMaterial1.color = Color.Lerp(Color.black,new Color(0,1,0.2901960784313725f,1),Mathf.PingPong(Time.time,1));
		}

		if (col.transform.name == "Lane2")
		{
			arrowMaterial2.color = Color.Lerp(Color.black,Color.red,Mathf.PingPong(Time.time,1));
		}

		if (col.transform.name == "Lane3")
		{
			arrowMaterial3.color = Color.Lerp(Color.black,Color.red,Mathf.PingPong(Time.time,1));
		}

		if (col.transform.name == "Lane4")
		{
			arrowMaterial4.color = Color.Lerp(Color.black,Color.yellow,Mathf.PingPong(Time.time,1));
		}
	}

	void OnCollisionExit(Collision col)
	{
		if (col.transform.name == "Lane1")
		{
			arrowMaterial1.color = Color.black;
		}
		
		if (col.transform.name == "Lane2")
		{
			arrowMaterial2.color = Color.black;
		}
		
		if (col.transform.name == "Lane3")
		{
			arrowMaterial3.color = Color.black;
		}
		
		if (col.transform.name == "Lane4")
		{
			arrowMaterial4.color = Color.black;
		}

	}


}
