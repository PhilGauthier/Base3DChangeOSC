using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    const int NB_Joints = 7;
    // Start is called before the first frame update
    protected Transform[] _j = new Transform[NB_Joints];
    [SerializeField, Range(-Mathf.PI, Mathf.PI)]
    public float[] _q = new float[NB_Joints];

    [SerializeField, Range(-Mathf.PI, Mathf.PI)]
    public float[] _qOffset = new float[NB_Joints];

    protected float[] _qOffseted = new float[NB_Joints];

    protected Vector3[] _localRadRot = new Vector3[NB_Joints];
    public float[] _qd = new float[NB_Joints];

    void Start()
    {
        initJoints();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateRotations();
    }

    public void SetQs(float[] q)
    {
        for (int i = 0; i < _q.Length; i++)
        {
            _q[i] = q[i];
        }
    }

    public void initJoints()
    {

        Transform[] renderers = this.GetComponentsInChildren<Transform>();
        int count = 0;
        foreach (Transform t in renderers)
        {
            if (t.name.Equals($"j{count}"))
                _j[count++] = t;            
        }
    }

    void applyOffsets()
    {
        for (int i = 0; i < _qOffset.Length; i++)
        {
            _qOffseted[i] = _q[i] + _qOffset[i];
        }
    }

    public void UpdateRotations()
    {
        applyOffsets();
        updateRotations(ref _qOffseted);
        for (int i = 0; i < _q.Length; i++)
        {
            _j[i].localEulerAngles = (_localRadRot[i]) * Mathf.Rad2Deg;
        }
    }

    void updateRotations(ref float[] q)
    {
        _localRadRot[0].y = -q[0];
        _localRadRot[1].z = -q[1];
        _localRadRot[2].z = -q[2];
        _localRadRot[3].z = -q[3];
        _localRadRot[4].x = -q[4];
        _localRadRot[5].z = -q[5];
        _localRadRot[6].y = -q[6];
    }
}
