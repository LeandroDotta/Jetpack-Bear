using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSpriteSwap : MonoBehaviour {

	
     public Sprite selectedSprite;

	 private Toggle toggle;
 
     // Use this for initialization
     void Start () {
		toggle = GetComponent<Toggle>();

         toggle.toggleTransition = Toggle.ToggleTransition.None; 
         toggle.onValueChanged.AddListener(OnTargetToggleValueChanged);

		 OnTargetToggleValueChanged(toggle.isOn);
     }
 
     void OnTargetToggleValueChanged(bool newValue) {
         Image targetImage = toggle.targetGraphic as Image;

         if (targetImage != null) {
             if (newValue) {
                 targetImage.overrideSprite = selectedSprite;
             } else {
                 targetImage.overrideSprite = null;
             }
         }
     }
}
