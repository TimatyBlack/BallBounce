using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tween_Library.Scripts.Effects
{
    public class TransformEffect : ITransformEffect
    {
        private Vector3 startScale;
        private Transform Transform { get; }
        private Vector3 ScaleTarget { get; set; }
        private float ScaleSpeed { get; }
        private YieldInstruction Wait { get; }

        public TransformEffect(Transform transform, float scaleSpeed, YieldInstruction wait)
        {
            Transform = transform;
            ScaleSpeed = scaleSpeed;
            Wait = wait;
            startScale = Transform.localScale;
        }

        public IEnumerator Execute(Vector3 scaleTarget)
        {
            ScaleTarget = scaleTarget;
            var time = 0f;
            var currentScale = Transform.localScale;
            
            while (Transform.localScale != ScaleTarget)
            {
                time += Time.deltaTime * ScaleSpeed;
                var scale = Vector3.Lerp(currentScale, ScaleTarget, time);
                Transform.localScale = scale;
                yield return null;
            }

            yield return Wait;

            currentScale = Transform.localScale;
            time = 0f;

            while (Transform.localScale != startScale)
            {
                time += Time.deltaTime * ScaleSpeed;
                var scale = Vector3.Lerp(currentScale, startScale, time);
                Transform.localScale = scale;
                yield return null;
            }
        }
    }
}

