using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vehicle {
    public int node;
    public string name;
    public bool hasFinished;

    public vehicle(int node,string name,bool HasFinished){
        this.node = node;
        this.name = name;
        hasFinished =HasFinished;
    }
}
