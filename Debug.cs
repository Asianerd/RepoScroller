using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace RepoScroller
{
    public static class Debug
    {
        public static void Print(object obj)
        {
            System.Diagnostics.Debug.WriteLine(obj);
        }
    }
}
