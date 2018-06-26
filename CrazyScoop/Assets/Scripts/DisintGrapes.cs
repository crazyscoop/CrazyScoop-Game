using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class DisintGrapes : MonoBehaviour {

    [SerializeField]
    private GameObject fake;

    [SerializeField]
    private Text grape;

    [SerializeField]
    private AudioSource coinssound;

    [SerializeField]
    private AudioSource distruction;

    private int grapecount ;



	void Awake ()
    {
        grapecount = 0;
        //grape.text = grapecount + " x";
    }




    void Update ()
    {

      
	
	}



    void OnCollisionEnter(Collision col)
    {
        
        if(col.collider.name == "pooledgrape(Clone)" && col.collider.tag == "d" )
        {
            grapecount += 1;
            if (LevelManager.clickbool)
            {
                coinssound.Play();

            }
            pausebutton.pbs.score += 1;
            grape.text =  grapecount + "x";
            col.transform.gameObject.SetActive(false);
            Rigidbody colbody = col.transform.gameObject.GetComponent<Rigidbody>();
            colbody.velocity = Vector3.zero;
            col.transform.parent = null;

        }
        else if (col.collider.name != "pooledgrape(Clone)" && col.collider.name != "Lane2" && col.collider.name != "platform" && col.collider.name != "baricade")
        {
            if (LevelManager.clickbool)
            {
                distruction.Play();

            }
            Instantiate(fake, transform.position, transform.rotation);
            Destroy(gameObject);

            Player.laneindex = 1;
       
            //Invoke("pausebutton.pbs.GameOver", 0.2f);

            pausebutton.pbs.GameOver();

        }


        
    }




}
