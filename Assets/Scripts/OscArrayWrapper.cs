using extOSC;
using extOSC.Core.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class OscArrayWrapper : MonoBehaviour
{
    public UnityEvent<float[]> _receivers;
    public void onReceiveArray(List<OSCValue> input)
    {
        float[] data = new float[input.Count];
        for(int i=0;i< input.Count; i++)
        {
            data[i] = input[i].FloatValue;
        }
        _receivers.Invoke(data);
    }    
}
