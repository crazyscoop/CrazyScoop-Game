using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisintPine : MonoBehaviour
{
    [SerializeField]
    private GameObject fake;

    [SerializeField]
    private Text pine;

    [SerializeField]
    private AudioSource coinssound;

    [SerializeField]
    private AudioSource distruction;

    private int pinecount ;

    void Awake()
    {
        pinecount = 0;
        //pine.text = "x " + pinecount;


    }


    void Update()
    {

        
        
    }

    void OnCollisionEnter(Collision col)
    {
        
        if (col.collider.name == "pooledpineapple(Clone)" && col.collider.tag == "d")
        {
            //Debug.Log(col.transform.name);
            if (LevelManager.clickbool)
            {
                coinssound.Play();

            }
            pinecount += 1;
            pausebutton.pbs.score += 1;
            pine.text = "x" + pinecount ;
            col.transform.gameObject.SetActive(false);
            Rigidbody colbody = col.transform.gameObject.GetComponent<Rigidbody>();
            colbody.velocity = Vector3.zero;
            col.transform.parent = null;

        }
        else if (col.collider.name != "pooledpineapple(Clone)" && col.collider.name != "Lane4" && col.collider.name != "platform" && col.collider.name != "baricade")
        {
            if (LevelManager.clickbool)
            {
                distruction.Play();

            }
            Instantiate(fake, transform.position, transform.rotation);
            Destroy(gameObject);
            Player.laneindex = 1;
            //Invoke("pausebutton.pbs.GameOver",2f);
            pausebutton.pbs.GameOver();

        }


    }

}
