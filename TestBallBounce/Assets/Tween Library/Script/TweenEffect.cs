using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tween_Library.Scripts
{
   public interface IUITweenEffect
    {
        IEnumerator Execute(int loopsCount);
    }

    public interface IColorTweenEffect
    {
        IEnumerator Execute();
    }
    public interface ITransformEffect
    {
        IEnumerator Execute(Vector3 scaleTarget);
    }
}
