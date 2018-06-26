using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 

public class Quotes : MonoBehaviour {

    


    void Awake()
    {
       
        
    }

	void OnEnable ()
    {

        GetComponent<Renderer>().material = newQuote.nq.quotematerial[newQuote.nq.quoteindex];

        if(newQuote.nq.quoteindex < 6)
        {
            newQuote.nq.quoteindex += 1;
        }
        if(newQuote.nq.quoteindex == 6)
        {
            newQuote.nq.quoteindex = 0;
        }
  
               
    }
	
	
	void Update ()
    {
	
	}
}
