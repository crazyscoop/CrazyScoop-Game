using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class fallingfruits : MonoBehaviour {

    //public static fallingfruits cur;
  
    List<GameObject> fruits;

    [SerializeField]
    List<GameObject> show;

    [SerializeField]
    private GameObject grapes;

    [SerializeField]
    private GameObject strawberry;

    [SerializeField]
    private GameObject pine;

    [SerializeField]
    private GameObject pears;

    public float fallrate = 1;

    private bool falltruth;

    void Awake()
    {
        //cur = this;
        falltruth = true;

    }

	void Start ()
    {


        fruits = new List<GameObject>();

      

        for(int i = 0;i<5;i++)
        {
            GameObject fruit_1 = (GameObject)Instantiate(grapes);
            fruit_1.SetActive(false);
            fruits.Add(fruit_1);

            GameObject fruit_2 = (GameObject)Instantiate(strawberry);
            fruit_2.SetActive(false);
            fruits.Add(fruit_2);

            GameObject fruit_3 = (GameObject)Instantiate(pine);
            fruit_3.SetActive(false);
            fruits.Add(fruit_3);

            GameObject fruit_4 = (GameObject)Instantiate(pears);
            fruit_4.SetActive(false);
            fruits.Add(fruit_4);
        }

        InvokeRepeating("falling", 1f, 1f);
    }
	
   public void falling()
    {
        if (falltruth == true)
        {
            int index = Random.Range(0, 4);
            //Debug.Log(show[index].name);

            for (int i = 0; i < 16; i++)
            {
                if (fruits[i].name == show[index].name + "(Clone)" && fruits[i].activeInHierarchy == false)
                {
                    fruits[i].transform.position = transform.position;
                    fruits[i].SetActive(true);
                    break;
                }

            }
        }


    }
	
	void FixedUpdate ()
    {


	
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.transform.name == "platformPlane1")
        {
            falltruth = false;
        }
       // Debug.Log("fsg");
    }
    void OnTriggerStay(Collider col)
    {
        if (col.transform.name == "platformPlane1")
        {
            falltruth = false;
        }
        //Debug.Log("fsg");

    }

    void OnTriggerExit(Collider col)
    {
        if (col.transform.name == "platformPlane2")
        {
            falltruth = true;
        }
       // Debug.Log("fsg");
    }


}
