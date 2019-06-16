using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PM_BackgroundManager : MonoBehaviour
{
	public int InspectorIndex;
	
	float trackOffsetX = 0.0f;
	float trackOffsetY = 0.0f;
	float moveSpeed = 0.01f;
	Material material;
	
    // Start is called before the first frame update
    void Start()
    {
        material = gameObject.GetComponent<MeshRenderer>().material;	
    }

    // Update is called once per frame
    void Update()
    {
		trackOffsetX -= moveSpeed * Time.deltaTime * InspectorIndex;
		trackOffsetY -= moveSpeed * Time.deltaTime * InspectorIndex;
        material.SetTextureOffset("_MainTex", new Vector2(trackOffsetX, trackOffsetY));        
    }
}
