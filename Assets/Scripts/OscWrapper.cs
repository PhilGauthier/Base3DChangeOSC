using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;
using extOSC.Core.Events;
using UnityEngine.Events;

public class OscWrapper : MonoBehaviour
{
    public BaseChange _bs;
    public UnityEvent<Vector3> _suscribersVec3;
    public UnityEvent<Quaternion> _suscribersQuaternion;
    
    // For array Events
    /*
    public void SendArrayToVector3(List<OSCValue> array)
    {
        if (array.Count >= 3)
        {
            Vector3 newPos = new Vector3(array[0].FloatValue, array[1].FloatValue, array[2].FloatValue);            
            _suscribersVec3.Invoke(newPos);
        }
        else
            Debug.Log($"Pb: {array.Count}");
        Debug.Log("Test");
    }
    */
    public void ForwardRightToLeftBase(Vector3 p)
    {
        _suscribersVec3.Invoke(_bs.ConvertRightToLeft(p));
    }

    public void ForwardRightToLeftBase(Quaternion q)
    {
        
        _suscribersQuaternion.Invoke(_bs.ConvertRightToLeft(q, transform));
    }

}
