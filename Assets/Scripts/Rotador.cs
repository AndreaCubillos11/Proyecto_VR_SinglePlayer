using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Rotador : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }
 
    // Update is called once per frame
    void Update()
    {
       transform.Rotate (new Vector3 (45,45,45)*Time.deltaTime);
    }
}
 