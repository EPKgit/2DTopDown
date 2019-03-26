using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSelf : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Die() {
      Destroy(transform.parent.gameObject);
    }
}
