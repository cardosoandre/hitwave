using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Player
{
    int sand;
    public int Sand
    {
        get
        {
            return sand;
        }
        set
        {
            if(value != sand)
            {
                sand = value;
                if(OnSandChange != null)
                {
                    OnSandChange();
                }
            }
        }
    }
    public event Action OnSandChange;
}
