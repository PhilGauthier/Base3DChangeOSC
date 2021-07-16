using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;
using extOSC.Core.Events;
using UnityEngine.Events;

public class OscWrapper : MonoBehaviour
{
    public BaseChange _bs;
    public Transform _localRightTransform;

    public UnityEvent<Vector3> _suscribersVec3;
    public UnityEvent<Quaternion> _suscribersQuaternion;

    public void ForwardRightToLeftBase(Vector3 p)
    {
        _suscribersVec3.Invoke(_bs.ConvertRightToLeft(p));
    }

    public void ForwardLeftToRigthBase(Vector3 p)
    {
        _suscribersVec3.Invoke(_bs.ConvertRightToLeft(p));
    }

    public void ForwardRightToLeftBase(Quaternion qRight)
    {
        _suscribersQuaternion.Invoke(_bs.ConvertRightToLeft(qRight, transform));
    }

    public void ForwardLeftToRightBase(Quaternion qRight)
    {
        _suscribersQuaternion.Invoke(_bs.ConvertRightToLeft(qRight, transform));
    }

    private void FixedUpdate()
    {
        if (_localRightTransform != null)
        {
            _localRightTransform.localPosition = _bs.ConvertLeftToRight(transform.localPosition);
            _localRightTransform.localRotation = _bs.ConvertLeftToRight(transform.localRotation, transform);
        }
    }

}
