using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal : MonoBehaviour
{
  public int count;
  public GameObject endGameEffect;
  private bool debug = true;
  private bool won = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Win() {
      if(won)return;
      Instantiate(endGameEffect, transform);
      won=true;
    }

    void OnTriggerEnter2D(Collider2D col) {
      if(debug)Debug.Log("Trigger");
      count += 1;
      
      if(count == PlayerInput.all.Count) {
        Win();
      }
    }

    void OnTriggerExit2D(Collider2D col) {
      count -= 1;
    }
}
