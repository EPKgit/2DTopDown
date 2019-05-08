using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal : MonoBehaviour
{
  public int count;
  private bool debug = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      if(count == PlayerInput.all.Count) {
        if(debug)Debug.Log("Win");
      }
    }

    void OnTriggerEnter2D(Collider2D col) {
      if(debug)Debug.Log("Trigger");
      count += 1;
    }

    void OnTriggerExit2D(Collider2D col) {
      count -= 1;
    }
}
