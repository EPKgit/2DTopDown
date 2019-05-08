using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToAimDirection : MonoBehaviour
{
    public bool useYinstead = false;
    public float offset = 0;
    public bool flip = false;
    private PlayerInput playerInput;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponentInParent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 angles = transform.localEulerAngles;
        Vector3 aimDirection = -playerInput.GetAimDirection();
        float sign = 1;
        if(flip)sign=-1;
        float angle = sign*Mathf.Atan2(aimDirection.y, aimDirection.x)*Mathf.Rad2Deg+180+offset;
        if(useYinstead) angles.y = angle;
        else angles.z = angle;
        Quaternion target = Quaternion.Euler(angles);
        // Quaternion target = Quaternion.LookRotation(aimDirection, Vector3.up);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, target, 0.5f);
    }
}
