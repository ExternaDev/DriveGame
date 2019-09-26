using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchGlare : MonoBehaviour {
	public GlareEffect glare;
	public ChromaticAbrevEffect ChromaticAberr;

	public float flashLength = .25f;
	public float BloomIntensity = 2f;
	public float ChromaticIntensity = .01f;



	public void Flash(){
		if(glare==null){
	       	glare = this.gameObject.AddComponent<GlareEffect>();
	       	glare.Init(BloomIntensity);
	      }
	    if(ChromaticAberr==null){
	       	ChromaticAberr = this.gameObject.AddComponent<ChromaticAbrevEffect>();
	       	ChromaticAberr.Init(ChromaticIntensity);
	    }
	}


	public void StartFlash(){
		
		if(glare==null){
	       	glare = this.gameObject.AddComponent<GlareEffect>();
	       	glare.Init(BloomIntensity);
	     }
	    if(ChromaticAberr==null){
	       	ChromaticAberr = this.gameObject.AddComponent<ChromaticAbrevEffect>();
	       	ChromaticAberr.Init(ChromaticIntensity);
	    }
	    glare.TurnOn(flashLength);
	    ChromaticAberr.TurnOn(flashLength);

	}
	public void EndFlash(){
		if(glare!=null)
  			glare.TurnOff(flashLength*2f);
		if(ChromaticAberr!=null)
	    	ChromaticAberr.TurnOff(flashLength*2f);
glare=null;
ChromaticAberr=null;
	}
}
