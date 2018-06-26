using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    public static Player current;

	//private Renderer myRenderer;

	//private Collider myCollider;

	private Rigidbody myRigidbody;

    public  bool inair ;

    [SerializeField]
    private GameObject sphere;

	[SerializeField]
	List<GameObject> models;

    private float pos;

    private float neg;

	[SerializeField]
	private int speed;

    [SerializeField]
    private float finalvol;

    [SerializeField]
    private AudioSource them;

    [SerializeField]
    private float rateofinc;

    private bool swipe = false;

    private Vector3 starttouch;

    private Vector3 endtouch;

    [SerializeField]
    private float swipemag;

    [SerializeField]
    private float jumpmag;

    [SerializeField]
    private float jumptime;

    private Vector3 startplayerpos;

    private Vector3 endplayerpos;

    private float sema;

    private bool ultimatetruth = false;

    [SerializeField]
    private List<Transform> playerposlist;

    [SerializeField]
    private AudioSource jumpsound;

    public static int laneindex = 1;
    
    void Awake()
    {
        Screen.orientation = ScreenOrientation.Landscape;
        current = this;
        Time.timeScale = 1;
        inair = false;
        sema = 0;
        ultimatetruth = false;
      //  StartCoroutine(StartTheme());
    }

    IEnumerator StartTheme()
    {
        while(them.volume < finalvol)
        {
            them.volume += rateofinc;
            yield return null;
        }
        them.volume = finalvol;
    }


    void Start () 
	{
        Screen.orientation = ScreenOrientation.Landscape;
		//myRenderer = GetComponent<Renderer> ();
		//myCollider = GetComponent<Collider> ();
		myRigidbody = GetComponent<Rigidbody> ();
        //colors = new List<Material> ();
        
        pos = 0;
        neg = 0;

	}

    void Update()
    {
        if (myRigidbody.velocity.y == 0 && inair == true)
        {
            inair = false;
        }


        if (Input.touchCount > 0)
        {

            Touch touch = Input.touches[0];
            if (touch.position.y < Screen.height * (1 - 0.20))
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        starttouch = touch.position;
                        ultimatetruth = true;
                        break;

                    case TouchPhase.Ended:
                        endtouch = touch.position;
                        if ((endtouch.x - starttouch.x) > swipemag && inair == false && Mathf.Sign(endtouch.x - starttouch.x) > 0 && Time.timeScale == 1)
                        {
                            if (ultimatetruth == true)
                            {
                                laneindex = LaneNo();
                                if (laneindex < 3)
                                {
                                    laneindex += 1;
                                    transform.position = Vector3.Lerp(transform.position, new Vector3(playerposlist[laneindex].position.x, transform.position.y, transform.position.z), 0.25f);
                                }
                                ultimatetruth = false;
                            }
                        }
                        if ((starttouch.x - endtouch.x) > swipemag && inair == false && Mathf.Sign(starttouch.x - endtouch.x) > 0 && Time.timeScale == 1)
                        {
                            if (ultimatetruth == true)
                            {
                                laneindex = LaneNo();
                                if (laneindex > 0)
                                {
                                    laneindex -= 1;
                                    transform.position = Vector3.Lerp(transform.position, new Vector3(playerposlist[laneindex].position.x, transform.position.y, transform.position.z), 0.25f);
                                }
                                ultimatetruth = false;
                            }
                        }
                        if (touch.deltaPosition.magnitude < jumpmag && inair == false && touch.deltaTime < jumptime && Time.timeScale == 1)
                        {
                            {
                                if (ultimatetruth == true)
                                {
                                    if (LevelManager.clickbool)
                                    {
                                        jumpsound.Play();
                                    }
                                    myRigidbody.AddForce(Vector3.up * 270);
                                    inair = true;
                                    ultimatetruth = false;
                                }
                            }
                        }
                        break;




                }
            }
            /*
            if (myRigidbody.velocity.y == 0 && inair == true)
            {
                inair = false;
            }

            if (Input.touchCount > 0)
            {

                Touch touch = Input.touches[0];

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        starttouch = touch.position;
                        ultimatetruth = true;
                        break;

                    case TouchPhase.Ended:
                        endtouch = touch.position;
                        if ((endtouch.x - starttouch.x) > swipemag && inair == false && Mathf.Sign(endtouch.x - starttouch.x) > 0)
                        {
                            if (ultimatetruth == true)
                            {
                                laneindex = LaneNo();
                                if (laneindex < 3)
                                {
                                    laneindex += 1;
                                    transform.position = Vector3.Lerp(transform.position, new Vector3(playerposlist[laneindex].position.x, transform.position.y, transform.position.z), 0.25f);
                                }
                                ultimatetruth = false;
                            }
                        }
                        if ((starttouch.x - endtouch.x) > swipemag && inair == false && Mathf.Sign(starttouch.x - endtouch.x) > 0)
                        {
                            if (ultimatetruth == true)
                            {
                                laneindex = LaneNo();
                                if (laneindex > 0)
                                {
                                    laneindex -= 1;
                                    transform.position = Vector3.Lerp(transform.position, new Vector3(playerposlist[laneindex].position.x, transform.position.y, transform.position.z), 0.25f);
                                }
                                ultimatetruth = false;
                            }
                        }
                        if (touch.deltaPosition.magnitude < jumpmag && inair == false && touch.deltaTime < jumptime)
                        {
                            {
                                if (ultimatetruth == true)
                                {
                                    myRigidbody.AddForce(Vector3.up * 300);
                                    inair = true;
                                    ultimatetruth = false;
                                }
                            }
                        }
                        break;




                }
                */

                /*
                if (touch.deltaPosition.magnitude == 0)
                {
                    if (touch.position.x > Screen.width / 2 && inair == false)
                    {
                        myRigidbody.velocity = Vector3.right * -0.8f * Time.deltaTime * speed;
                    }
                    if (touch.position.x < Screen.width / 2 && inair == false)
                    {
                        myRigidbody.velocity = Vector3.right * 0.8f * Time.deltaTime * speed;
                    }
                }
                */
            }
  




        /*
        bool jmp = CrossPlatformInputManager.GetButtonDown("Jump");

        //Debug.Log(jmp);

        if (jmp == true && inair == false )
        {
            myRigidbody.AddForce(Vector3.up * 300);
            inair = true;
        }
        

         if(myRigidbody.velocity.y == 0 && inair == true)
         {
             inair = false;
         }

        
         */

        //    if (Input.touchCount > 0)
        {

            //   Gyroscope g =  gameObject.GetComponent<Gyroscope>() ;

            //  Debug.Log(g.rotationRate);

            /*

                        Touch touch = Input.touches[0];

                        if (touch.position.y > (Screen.height) / 2 && touch.phase == TouchPhase.Began && inair == false)
                        {
                            myRigidbody.AddForce(Vector3.up * 300);
                            inair = true;

                        }


                     * /
                      /*
                         if (myRigidbody.velocity.y == 0 && inair == true)
                         {
                             inair = false;
                         }
                         */
            /*

            if ( touch.phase == TouchPhase.Stationary)
            {
           
                

                if(touch.position.x < (Screen.width)/2 && touch.position.y < (Screen.height)/2)
                {
                    transform.Translate(Vector3.right * -0.6f * Time.deltaTime * speed);
                }
            }

            if ( touch.phase == TouchPhase.Stationary)
            {
                if (touch.position.x > (Screen.width) / 2 && touch.position.y < (Screen.height) / 2)
                {
                    transform.Translate(Vector3.right * 0.6f * Time.deltaTime * speed);
                }
            }
            */

            /*
            if (touch.phase == TouchPhase.Stationary && Input.touchCount < 2)
            {
                if (touch.position.y > (Screen.height) / 2)
                {
                    myRigidbody.AddForce(Vector3.up * 30);
                }
            }
            */

            //}


            //Debug.Log(inair);




        }




    }

    void LateUpdate()
    {

    }

	void FixedUpdate ()
	{
        if (inair == false)
        {
            transform.position = new Vector3(playerposlist[laneindex].position.x,transform.position.y,transform.position.z);
        }

        /*
                if(Input.touchCount > 0)
                {
                    Touch touch = Input.touches[0];
                    switch (touch.phase)

                    {
                        case TouchPhase.Began:
                            starttouch = touch.position;
                            break;

                        case TouchPhase.Ended:
                            endtouch = touch.position;
                            if((endtouch - starttouch).magnitude > swipemag && inair == false && Mathf.Sign(endtouch.y - starttouch.y) > 0)
                            {
                                myRigidbody.AddForce(Vector3.up * 200);
                                inair = true;
                            }
                            break;
                    }
                }


                if(myRigidbody.velocity.y == 0 && inair == true)
                {
                    inair = false;
                }
        */










        //Debug.Log(myHorizontal);
        /*    
             if (Input.touchCount > 0)
             {
                 Touch touch = Input.touches[0];

                 if (touch.phase == TouchPhase.Stationary)
                 {

                     if (touch.position.x < (Screen.width) / 2 && inair == false)
                     {

                         transform.Translate(Vector3.right * -0.7f * Time.deltaTime * speed);
                     }
                 }

                 if (touch.phase == TouchPhase.Stationary)
                 {
                     if (touch.position.x > (Screen.width) / 2 && inair == false)
                     {

                         transform.Translate(Vector3.right * 0.7f * Time.deltaTime * speed);
                     }
                 }

             }
             */

    }

    /* public GameObject returnfruit ()
     {
         int index = Random.Range(0, 4);
         GameObject newplayer = (GameObject)Instantiate(models[index]);
         newplayer.transform.position = gameObject.transform.position;
         Destroy(gameObject);
         return newplayer;

     }
     */

    void OnTriggerExit(Collider other)
	{
		if(other.name == "CubeImp")
		{
            //returnfruit();
            //Debug.Log("yuup");
            int index = Random.Range(0, 4);
            gameObject.SetActive(false);
            
          //  Debug.Log(models[index].transform.name);
            
            models[index].SetActive(true);
            models[index].transform.position = gameObject.transform.position;
            Spheremovement.ultimate.target = models[index];
            //return newplayer;
            
        }

	}

    void OnCollisionEnter(Collision c)
    {
        if((c.transform.tag == "lanes" || c.transform.tag == "baricade") )
        {
            if (inair == true)
            {
                inair = false;
            }
        }


    }

    void OnCollisionExit(Collision c)
    {
        if(inair == true)
        {
            if(c.transform.tag == "baricade" && myRigidbody.velocity.y == 0)
            {
                inair = false;
            }
        }
    }

    void OnCollisionStay(Collision c)
    {
        if (inair == true)
        {
            if (c.transform.tag == "baricade" && myRigidbody.velocity.y == 0)
            {
                inair = false;
            }
            if (c.transform.tag == "lanes" && myRigidbody.velocity.y == 0)
            {
                inair = false;
            }

        }

    }

    private int LaneNo()
    {
        for (int i = 0;i < 4;i++)
        {
            if(transform.position.x == playerposlist[i].position.x)
            {
                return i;
            }
        }return 10;

    }

 
  











}
