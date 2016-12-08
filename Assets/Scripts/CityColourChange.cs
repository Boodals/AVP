using UnityEngine;
using System.Collections;

public class CityColourChange : MonoBehaviour {


    public Material inMat;
    private Material coreMat;
    float blendVal;
    private GameObject[] cityObjects;
    private Color baseColur;

    bool flip;
	// Use this for initialization
	void Start ()
    {
        baseColur = Color.gray;
        coreMat = Instantiate(inMat);
        cityObjects = GameObject.FindGameObjectsWithTag("CITYBUILDING");
        blendVal = 0.0f;
        flip = true;

	}
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetKey(KeyCode.J))
        {

            baseColur = new Color(blendVal, blendVal, blendVal);

        foreach(GameObject go in cityObjects)
            {
                go.GetComponent<Renderer>().material.SetColor("_Color", baseColur);
            }
            if(flip)
            {
                blendVal= blendVal + 0.01f;
                if(blendVal > 1)
                {
                    flip = false;
                }
            }
            else
            {
                blendVal = blendVal - 0.01f;
                if (blendVal < 0)
                {
                    flip = true;
                }
            }
        }




	}
}
