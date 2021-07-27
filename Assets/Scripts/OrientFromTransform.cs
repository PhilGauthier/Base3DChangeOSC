using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientFromTransform : MonoBehaviour
{
    public Transform _tOrgin;
    public Transform _tTarger;
    

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 delta = _tTarger.localPosition - _tOrgin.localPosition;
        transform.rotation = Quaternion.LookRotation(delta);
        float s = delta.magnitude;
        transform.localPosition = _tOrgin.localPosition;
        transform.localScale = new Vector3(s, s, s);

    }
}
