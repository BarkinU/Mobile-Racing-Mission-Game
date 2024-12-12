using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct MapObject
{
    
    public string objectName;
    public Vector3 objectPosition;
    public Quaternion objectRotation;

    public MapObject(Transform mapObjRef)
    {
        objectName = mapObjRef.name.Split('.')[0];
        objectPosition=mapObjRef.position;
        objectRotation=mapObjRef.rotation;
    }
}
