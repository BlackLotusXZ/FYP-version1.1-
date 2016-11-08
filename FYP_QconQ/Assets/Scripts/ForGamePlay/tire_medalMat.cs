using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/********************************************/
/**
*  \brief
*   - This script just changes the material of the end-game medal you get for completing the level
***********************************************/
public class tire_medalMat : MonoBehaviour {

    public Material []mats;

    public void changeMaterials(int i)
    {
        this.GetComponent<RawImage>().material = mats[i];
    }
}
