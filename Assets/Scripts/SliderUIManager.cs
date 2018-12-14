using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class controls the triangles from the sliders, so that the correct value can be set.
public class SliderUIManager : MonoBehaviour {

    [SerializeField]
    private Slider[] _sliders;
    [SerializeField]
    private InputField[] _inputFields;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetValue(int sliderID, float value)
    {
        _sliders[sliderID].value = value;
    }
}
