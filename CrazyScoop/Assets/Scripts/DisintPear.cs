using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisintPear : MonoBehaviour {

    [SerializeField]
    private GameObject fake;

    [SerializeField]
    private Text pear;

    [SerializeField]
    private AudioSource coinssound;

    [SerializeField]
    private AudioSource distruction;

    private int pearcount ;

    void Awake()
    {
        pearcount = 0;
        //pear.text = pearcount + " x";
    }


    void Update()
    {



    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.name == "pooledpears(Clone)" && col.collider.tag == "d")
        {
            if (LevelManager.clickbool)
            {
                coinssound.Play();

            }
            pearcount += 1;
            pausebutton.pbs.score += 1;
            pear.text =  pearcount + "x";
            col.transform.gameObject.SetActive(false);
            Rigidbody colbody = col.transform.gameObject.GetComponent<Rigidbody>();
            colbody.velocity = Vector3.zero;
            col.transform.parent = null;

        }
        else if(col.collider.name != "pooledpears(Clone)"  && col.collider.name != "Lane1" && col.collider.name != "platform" && col.collider.name != "baricade")
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
