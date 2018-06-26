using UnityEngine;
using System.Collections;

public class Spheremovement : MonoBehaviour {

    public static Spheremovement ultimate;

    [SerializeField]
    private GameObject player;

    public GameObject target;

    void Awake()
    {
        ultimate = this;
    }

	void Start ()
    {
        target = player;
	}
	
	
    
	void FixedUpdate ()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x, transform.position.y, transform.position.z), 0.01f);
        }
	}

}

