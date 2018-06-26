using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LaneCloneStart : MonoBehaviour 
{	
	private float delay;

	private float extratime;

	private float deathtime;

	private float initialtime;

	private float count = 0;

	private float count2 = 0;

    public float speed2;

    [SerializeField]
    private GameObject door1;

    [SerializeField]
    private GameObject door2;

    private Animator animD1;

    private Animator animD2;

    private Transform transD1;

    private Transform transD2;

    private GameObject obs;

    void OnEnable()
    {
      //  Debug.Log(transform.name);
    }

    void Awake()
    {


    }


    void Start ()
	{
        
        //delay = (5 * 12.8f) / LaneMovement.speed;
        //initialtime = Time.time;
        /*float myTime1 = (5 * 12.8f) / mySpeed;
		float myTime2 = (5 * 25.6f) / (mySpeed + 3);
		InvokeRepeating("Fire",myTime1,17.14285714285714f);
       */
        //extratime = 0.1f;
        //deathtime = 0;
        //Debug.Log("start");


        animD1 = door1.GetComponent<Animator>();
        animD2 = door2.GetComponent<Animator>();
        animD1.enabled = false;
        animD2.enabled = false;
        transD1 = door1.transform;
        transD2 = door2.transform;


    }
	
	void FixedUpdate()
	{
        /*
		if (Time.time  >= delay + initialtime )
		{
			Fire();
			//extratime += 0.75f;
			deathtime += 0.75f;
		}
        */
        
        transform.Translate(-Vector3.forward * Time.deltaTime * speed2);

    }
	

	private void Fire()
	{
		speed2 += 1f;
		speed2 = Mathf.Clamp (speed2, 5, 10);
        //Debug.Log(speed);

        
		GameObject obj = LanePooling.current.Lanefunction ();
		//GameObject obs = LanePooling.current.obstaclefunction ();
		if (obj == null)
			return;
		obj.transform.position = new Vector3(1.5f,0.25f,transform.position.z + 64.2f + 10);
		obj.SetActive (true);
        
        if (obj.GetComponent<LaneCloneStart>().animD1 != null)
        {
            obj.GetComponent<LaneCloneStart>().animD1.enabled = false;
            obj.GetComponent<LaneCloneStart>().door1.transform.rotation= transD1.rotation;

        }
        if (obj.GetComponent<LaneCloneStart>().animD2 != null)
        {
            obj.GetComponent<LaneCloneStart>().animD2.enabled = false;
            obj.GetComponent<LaneCloneStart>().door2.transform.rotation = transD2.rotation;

        }
        obj.GetComponent<LaneCloneStart>().speed2 = speed2;
        
		//obs.transform.SetParent (obj.transform);
		//obs.transform.localPosition = new Vector3 (0, 0.5f, Random.Range (0, 50));
		//obs.SetActive (true);
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
                Fire();
                



            }
		}
		
		if (col.transform.name == "Plane2") 
		{
			if(count2 == 0)
			{
				count2 = 1;
				Distroy();
			}

		}
	}
	
	private void Distroy()
	{
		gameObject.SetActive (false);
		//obs.SetActive (false);
		count = 0;
	}
	
	void OnDisable()
	{
       // speed2 += 1;
       // speed2 = Mathf.Clamp(speed2, 5, 10);
       // Debug.Log(speed2);
		CancelInvoke ("Distroy");
		count2 = 0;
	}

}
