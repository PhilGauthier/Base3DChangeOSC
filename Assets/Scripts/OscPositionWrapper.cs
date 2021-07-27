using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;
using extOSC.Core.Events;
using UnityEngine.Events;

public class OscPositionWrapper : MonoBehaviour
{
    public BaseChange _bs;
    public Transform _localRightTransform;

    public UnityEvent<string> _suscribersPosL2RString;
    public UnityEvent<string> _suscribersPosR2LString;
    public UnityEvent<Vector3> _suscribersVec3;
    public UnityEvent<Quaternion> _suscribersQuaternion;

    private string format2V(Vector3 p, Vector3 np)
    {
        return $"{p.x:+#.000;-#.000}, {p.y:+#.000;-#.000}, {p.z:+#.000;-#.000} => {np.x:+#.000;-#.000}, {np.y:+#.000;-#.000}, {np.z:+#.000;-#.000}";
    }

    public void ForwardRightToLeftBase(Vector3 p)
    {
        Vector3 np = _bs.ConvertRightToLeft(p);
        _suscribersVec3.Invoke(np);
        _suscribersPosR2LString.Invoke(format2V(p, np));
    }

    public void ForwardLeftToRigthBase(Vector3 p)
    {
        Vector3 np = _bs.ConvertRightToLeft(p);        
        _suscribersVec3.Invoke(np);
        _suscribersPosL2RString.Invoke(format2V(p, np));
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
            Vector3 oPos = transform.localPosition;
            Vector3 nPos = _bs.ConvertLeftToRight(transform.localPosition); 
            _localRightTransform.localPosition = nPos;
            _localRightTransform.localRotation = _bs.ConvertLeftToRight(transform.localRotation, transform);
        }
    }

}
