using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    [SerializeField]
    private Text straw;


    [SerializeField]
    private Text grape;


    [SerializeField]
    private Text pine;


    [SerializeField]
    private Text pear;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.transform.name);


    }




}
