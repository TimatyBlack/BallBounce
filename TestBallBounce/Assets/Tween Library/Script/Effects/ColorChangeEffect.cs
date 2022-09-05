using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tween_Library.Scripts.Effects
{
    public class ColorChangeEffect : IColorTweenEffect
    {
        private Color startColor;
        private Material DefaultColor { get; }
        private Color NewColor { get; }
        private YieldInstruction Wait { get; }
        private GameObject Ball { get; }

        private MeshRenderer ballRenderer;

        public ColorChangeEffect(Material defaultColor, Color newColor, GameObject ball, YieldInstruction wait)
        {
            DefaultColor = defaultColor;
            NewColor = newColor;
            Ball = ball;
            Wait = wait;
            ballRenderer = Ball.GetComponent<MeshRenderer>();
            startColor = DefaultColor.color;
        }

        public IEnumerator Execute()
        {
            var time = 0f;
            while(ballRenderer.material.color != NewColor)
            {
                time += Time.deltaTime * 2;
                var color = Color.Lerp(DefaultColor.color, NewColor, time);
                ballRenderer.material.color = color;
                yield return null;
            }

            yield return Wait;

            time = 0f;
            while(ballRenderer.material.color != startColor)
            {
                time += Time.deltaTime * 10;
                var color = Color.Lerp(NewColor, startColor, time);
                ballRenderer.material.color = color;
                yield return null;
            }
        }
    }
}