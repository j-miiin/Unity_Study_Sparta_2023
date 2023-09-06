using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INpc 
{
    public string Name { get; }
    public string Dialog { get; }

    void InteractWithPlayer();
}
