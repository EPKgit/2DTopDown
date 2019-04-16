using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToAimDirection : MonoBehaviour
{
    private PlayerInput playerInput;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponentInParent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 angles = transform.rotation.eulerAngles;
        Vector3 aimDirection = -playerInput.GetAimDirection();
        angles.z = Mathf.Atan2(aimDirection.y, aimDirection.x)*Mathf.Rad2Deg;
        Quaternion target = Quaternion.Euler(angles);
        // Quaternion target = Quaternion.LookRotation(aimDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, target, 0.5f);
    }
}
