using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Dot : MonoBehaviour {

    [SerializeField]
    private int Bounces;

    [SerializeField]
    private int count;

    [SerializeField]
    private Transform finalPos;

    [SerializeField]
    private Sprite newDot;

    [SerializeField]
    private GameObject oblack;

    [SerializeField]
    private Transform oposition;

    private bool truth;

    private bool truth2;

    private Animator anima;

    [SerializeField]
    private float oforce;

    [SerializeField]
    private AudioSource bounceaud;

	void Start ()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        count = 0;
        truth = true;
        truth2 = true;
        anima = GetComponent<Animator>();
        anima.enabled = false;
        float time = anima.GetCurrentAnimatorStateInfo(0).length;
	}
	
	
	void Update ()
    {

        if (count == Bounces && truth == true)
        {
            transform.position = finalPos.position;
            GetComponent<Rigidbody2D>().constraints =  RigidbodyConstraints2D.FreezePositionY;
            anima.enabled = true;
            anima.Play("Swing");
            truth = false;
            Invoke("Oanimate", 0.4f);
        }
	}

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.transform.tag == "Dot")
        {
            count += 1;
            if (count < 8)
            {
                bounceaud.Play();
                bounceaud.volume -= 0.1f;
            }
        }

    }

    void Oanimate()
    {
        //Debug.Log("fsfs");
        gameObject.GetComponent<SpriteRenderer>().sprite = newDot;
        oblack.SetActive(true);
        oblack.GetComponent<Rigidbody2D>().AddForce(oblack.transform.right * oforce);
        Invoke("nextLevel", 3f);
    }

    void nextLevel()
    {
        if (truth2 == true)
        {
           // Debug.Log("L");
            SceneManager.LoadScene(1);
        }
    }

}
