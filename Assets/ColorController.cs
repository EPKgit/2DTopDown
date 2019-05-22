using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    public Color color;

    private void UpdateColor() {
      foreach(SpriteRenderer r in GetComponentsInChildren<SpriteRenderer>()) {
        r.color = color;
      }
    }
    void OnValidate() {
      UpdateColor();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
