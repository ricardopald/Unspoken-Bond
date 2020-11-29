using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionController
{
    public enum KeyActions
    {
        ButtonA,
        ButtonB,
        ButtonX,
        ButtonY
    }

    public enum ButtonAction {
        Press = 0, 
        Down = 1, 
        Up = 2, 
        LongPress = 3, 
        DoubleTap = 4 , 
        Toggle = 5
    }
}
