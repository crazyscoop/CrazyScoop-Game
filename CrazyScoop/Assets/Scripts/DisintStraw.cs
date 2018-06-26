using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisintStraw : MonoBehaviour {

    [SerializeField]
    private GameObject fake;

    [SerializeField]
    private Text straw;

    [SerializeField]
    private AudioSource coinssound;

    [SerializeField]
    private AudioSource distruction;

    private int strawcount;
    

    void Awake()
    {
       
      //  straw = GameObject.Find("Canvas").transform.GetChild(0).transform.GetChild(1).GetComponent<Text>();
        strawcount = 0;
        //straw.text = "x " + strawcount;
    }


    void Update()
    {

        

    }

    void OnCollisionEnter(Collision col)
    {
        
        if (col.collider.name == "pooledstrawberry(Clone)" && col.transform.tag == "d" )
        {
            if (LevelManager.clickbool)
            {
                coinssound.Play();

            }
            strawcount += 1;
            pausebutton.pbs.score += 1;
            straw.text = "x" + strawcount;
            col.transform.gameObject.SetActive(false);
            Rigidbody colbody = col.transform.GetComponent<Rigidbody>();
            colbody.velocity = Vector3.zero;
            col.transform.parent = null;

        }
        else if(col.collider.name != "pooledstrawberry(Clone)" && col.collider.name != "Lane3" && col.collider.name != "platform" && col.collider.name != "baricade")
        {
            if (LevelManager.clickbool)
            {
                distruction.Play();

            }
            Instantiate(fake, transform.position, transform.rotation);
            Destroy(gameObject);
            Player.laneindex = 1;
            //Invoke("pausebutton.pbs.GameOver", 2f);
            pausebutton.pbs.GameOver();
        }


    }
}
