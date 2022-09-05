using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tween_Library.Scripts.Effects
{
    public class ScaleRectEffect : IUITweenEffect
    {
        private RectTransform RectTransform { get; }
        private Vector3 MaxSize { get; }
        private float ScaleSpeed { get; }
        private YieldInstruction Wait { get; }
        
        public ScaleRectEffect(RectTransform rectTransform, Vector3 maxSize, float scaleSpeed, YieldInstruction wait)
        {
            RectTransform = rectTransform;
            MaxSize = maxSize;
            ScaleSpeed = scaleSpeed;
            Wait = wait;

        }

        public IEnumerator Execute(int loopsCount)
        {
            while (loopsCount > 0)
            {
                var time = 0f;
                var currentScale = RectTransform.localScale;

                while (RectTransform.localScale != MaxSize)
                {
                    time += Time.deltaTime * ScaleSpeed;
                    var scale = Vector3.Lerp(currentScale, MaxSize, time);
                    RectTransform.localScale = scale;
                    yield return null;
                }

                yield return Wait;

                currentScale = RectTransform.localScale;
                time = 0f;
                while (RectTransform.localScale != Vector3.one)
                {
                    time += Time.deltaTime * ScaleSpeed;
                    var scale = Vector3.Lerp(currentScale, Vector3.one, time);
                    RectTransform.localScale = scale;
                    yield return null;
                }
                loopsCount--;
            }
        }
    }
}