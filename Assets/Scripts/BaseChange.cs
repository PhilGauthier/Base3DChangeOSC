using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using extOSC.Components.Informers;
using System;

public class BaseChange : MonoBehaviour
{
    protected Transform[] _srcList;
    protected Transform[] _destList;
    public Transform _destParent;
    public Transform _srcParent;

    public float4x4 _tMatrix;

    // Start is called before the first frame update
    void Awake()
    {
        if (_srcParent == null || _destParent ==null)
            return;

        _srcList = new Transform[_srcParent.childCount];
        _destList = new Transform[_srcParent.childCount];
        
        for(int c = 0; c < _srcParent.childCount; c++)
        {
            var i = _srcParent.GetChild(c);            
            var o = Instantiate(i.gameObject, _destParent);
            var oscListO = o.GetComponents<OSCTransmitterInformer>();
            var oscListI = i.GetComponents<OSCTransmitterInformer>();
            for (int j=0;j< oscListI.Length; j++)
            {
                oscListI[j].TransmitterAddress += $"-{_srcParent.name}-";
                oscListO[j].TransmitterAddress += $"-{_destParent.name}-";
            }

            _srcList[c] = i;
            _destList[c] = o.transform;
        }
    }

    public Vector3 ConvertRightToLeft(Vector3 p)
    {
        float4 p4 = new float4(p.x, p.y, p.z, 0);
        float4 pOut = math.mul(math.transpose(_tMatrix), p4);        
        return new Vector3(pOut.x, pOut.y, pOut.z);
    }

    public Vector3 ConvertLeftToRight(Vector3 p)
    {
        float4 p4 = new float4(p.x, p.y, p.z, 0);
        float4 pOut = math.mul(_tMatrix, p4);
        return new Vector3(pOut.x, pOut.y, pOut.z);
    }

    public Quaternion ConvertRightToLeft(Quaternion q, Transform t)
    {
        float4x4 tMatrix2 = math.mul(_tMatrix, t.localToWorldMatrix);

        return Quaternion.LookRotation(tMatrix2.c2.xyz, tMatrix2.c1.xyz);       
    }

    public Quaternion ConvertLeftToRight(Quaternion q, Transform t)
    {
        float4x4 tMatrix2 = math.mul(_tMatrix, t.localToWorldMatrix);

        return Quaternion.LookRotation(tMatrix2.c2.xyz, tMatrix2.c1.xyz);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_srcParent == null || _destParent == null)
            return;
        for (int i = 0; i < _srcList.Length; i++)
        {
            // position  
            _destList[i].localPosition = ConvertLeftToRight(_srcList[i].localPosition);

            //orientation  
            _destList[i].localRotation = ConvertLeftToRight(_srcList[i].localRotation, _srcList[i].transform);
        }
    }
}
