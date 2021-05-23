using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstensioneMolla : MonoBehaviour
{
    public int state = 0;
    [SerializeField]private float val = 0.08f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(state == 1)
        {
            Vector3 scale = this.transform.localScale;
            scale.z += val;
            this.transform.localScale = scale;
        }
    }
}
