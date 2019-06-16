using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PDS_OffsetHandler : MonoBehaviour
{
	float trackOffset = 0.0f;
	float moveSpeed = 0.1f;
	Material material;
	
    // Start is called before the first frame update
    void Start()
    {
        material = gameObject.GetComponent<LineRenderer>().material;	
    }

    // Update is called once per frame
    void Update()
    {
		trackOffset -= moveSpeed * Time.deltaTime;
        material.SetTextureOffset("_MainTex", new Vector2(trackOffset, 0));        
    }
	
}
