using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tween_Library.Scripts;
using Tween_Library.Scripts.Effects;

public class CounterPanel : MonoBehaviour
{
    public IUITweenEffect _effect;
    private YieldInstruction _wait;


    [SerializeField] private float waitTime;
    [SerializeField] private Vector3 maxScaleSize;
    [SerializeField] private float scaleSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        _wait = new WaitForSeconds(waitTime);
        _effect = new ScaleRectEffect(GetComponent<RectTransform>(), maxScaleSize, scaleSpeed, _wait);
    }
    // Update is called once per frame
    void Update()
    {   
        
    }
}

