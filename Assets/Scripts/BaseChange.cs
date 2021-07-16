using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using extOSC.Components.Informers;
using System;

public class BaseChange : MonoBehaviour
{
    public float4x4 _leftToRight;


    public Vector3 ConvertRightToLeft(Vector3 p)
    {
        float4 p4 = new float4(p.x, p.y, p.z, 0);
        float4 pOut = math.mul(math.transpose(_leftToRight), p4);        
        return new Vector3(pOut.x, pOut.y, pOut.z);
    }

    public Vector3 ConvertLeftToRight(Vector3 p)
    {
        float4 p4 = new float4(p.x, p.y, p.z, 0);
        float4 pOut = math.mul(_leftToRight, p4);
        return new Vector3(pOut.x, pOut.y, pOut.z);
    }

    public Quaternion ConvertRightToLeft(Quaternion qRight, Transform transform)
    {
        Matrix4x4 m44 = transform.localToWorldMatrix;
        m44.SetTRS(Vector3.zero, qRight, Vector3.one);
        float4x4 rightToLeft = math.transpose(_leftToRight);
        float4x4 tMatrix2 = math.mul(m44, _leftToRight);        
        tMatrix2 = math.mul(rightToLeft , tMatrix2);
        
        return Quaternion.LookRotation(tMatrix2.c2.xyz, tMatrix2.c1.xyz);
        
        //return new Quaternion(qRight.x, qRight.z, qRight.y, -qRight.w);
    }

    public Quaternion ConvertLeftToRight(Quaternion qLeft, Transform t)
    {
        /*
        float4x4 tMatrix2 = math.mul(_leftToRight, t.localToWorldMatrix);
        return Quaternion.LookRotation(tMatrix2.c2.xyz, tMatrix2.c1.xyz);
        */
        Matrix4x4 m44 = transform.localToWorldMatrix;
        m44.SetTRS(Vector3.zero, qLeft, Vector3.one);
        float4x4 rightToLeft = math.transpose(_leftToRight);

        float4x4 tMatrix2 = math.mul(m44, rightToLeft);
        tMatrix2 = math.mul(_leftToRight, tMatrix2);
        
        return Quaternion.LookRotation(tMatrix2.c2.xyz, tMatrix2.c1.xyz);
        
    }
}
