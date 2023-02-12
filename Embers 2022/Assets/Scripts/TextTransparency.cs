using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTransparency : MonoBehaviour
{
    // Start is called before the first frame update
    public Color alpha;
    void Start()
    {
        alpha = gameObject.GetComponent<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
