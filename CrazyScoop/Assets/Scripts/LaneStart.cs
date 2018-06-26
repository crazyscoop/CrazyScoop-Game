using UnityEngine;
using System.Collections;

public class LaneStart : MonoBehaviour {


	private float count = 0;

    [SerializeField]
    private GameObject door1;

    [SerializeField]
    private GameObject door2;

	private float count2 = 0;

    public float speed = 4;

    private Animator animD1;

    private Animator animD2;

    void Start ()
	{
        Screen.orientation = ScreenOrientation.Landscape;
        animD1 = door1.GetComponent<Animator>();
        animD2 = door2.GetComponent<Animator>();
        animD1.enabled = false;
        animD2.enabled = false;
	}

    void FixedUpdate()
    {
        transform.Translate(-Vector3.forward * Time.deltaTime * speed);
    }


	

	private void Fire()
	{
		speed += 1f;
       // Debug.Log(speed);
        GameObject obj = LanePooling.current.Lanefunction ();
		if (obj == null)
			return;
		obj.transform.position = new Vector3(1.5f,0.25f,transform.position.z + 64.4f + 10);
        obj.GetComponent<LaneCloneStart>().speed2 = 5;
		obj.SetActive (true);
	}

	void OnTriggerExit(Collider col)
	{
		
		
		if (col.transform.name == "Plane1") 
		{
			if(count == 0)
			{
				count = 1;
                animD1.enabled = true;
                animD2.enabled = true;
                animD1.Play("doorOpen1");
                animD2.Play("doorOpen2");
                Fire ();


			}
		}
		
		if (col.transform.name == "Plane2") 
		{
			if(count2 == 0)
			{
				count2 = 1;
				Distroy ();
			}
			
		}
	}

	private void Distroy()
	{
		gameObject.SetActive (false);
	}
	
	void OnDisable()
	{
		CancelInvoke ("Distroy");
       
	}

}
