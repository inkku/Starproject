using UnityEngine;
using System.Collections;

public class AudioSourceFilters {
	
	public enum FilterType {lowpass, highpass, bandpass}

	public FilterType type=FilterType.lowpass;
	public int resonance=10;

	private float[] sigIn;
	private float[] sigOut;

	public int cutoffFreq;
	public int highCutoffFreq;
	public float upperClip, lowerClip;


	int samplerate = 48000;
	
	private float c;

//***************
//
//EFFECT FILTERS
//***************

	public AudioSourceFilters() {
		Start ();
		}

	public void Mute(float[] data) {
		for (int i=0; i < data.Length; i++) {
			data[i]=0;
		}
	}

	public void Distort(float[] data, float clip, float gain) {
		Distort (data, clip, -clip, gain);
	}

	public void Distort(float[] data, float upperClip, float lowerClip, float gain) {
		for (int i=0; i<data.Length; i++) {
			if (data [i]* gain > upperClip) {
				data [i] = upperClip;
			} else if (data[i] *gain < lowerClip) {
				data[i] = lowerClip;
			} else {
				data [i] *= gain;
			}  
		}
	}

	public void FilterRingmod(float[] data, int modFreq) {
		//		float step = 2* Mathf.PI / samplerate;
	}

//***************
//
//PASS FILTERS
//***************

	public void BandPassFilter(float[] data, int frequency, float Q) {
		type=FilterType.bandpass;
//		PassiveFilter (data, cutoffFreq, type);
		ActiveFilter (data, frequency, (1/Q), type);
	}

	public void LowPassFilter(float[] data, int frequency, float Q) {
		type=FilterType.lowpass;
//		PassiveFilter (data, cutoffFreq, type);
		ActiveFilter (data, frequency, (1/Q), type);
	}

	public void HighPassFilter(float[] data, int frequency, float Q) {
		type=FilterType.highpass;
//		PassiveFilter (data, cutoffFreq, type);
		ActiveFilter (data, frequency, (1/Q), type);

//		ActiveFilter (data, frequency, (1/Q), type);
	}











//***************
//
//HOUSEKEEPING and STUFF
//***************

	//THIS WITHOUT RESONANCE
	private void PassiveFilter(float[] data, int cutoffFreq, FilterType filtertype) {
	
		float[] ampIn = {0,0,0};
		float[] ampOut = {0,0,0};

/*		if (cutoffFreq > 0 && cutoffFreq < 20000) {
			switch (filtertype) {
				case (FilterType.lowpass):
					getLoPFilterCoefficients (ampIn, ampOut, cutoffFreq, 1);
					break;
				case (FilterType.highpass):
					getHiPFilterCoefficients (ampIn, ampOut, cutoffFreq, 1);
					break;
				case (FilterType.bandpass):
					getBPFilterCoefficients(ampIn, ampOut, cutoffFreq, 1);
					break;
			}

			filter(data, ampIn, ampOut);
		}*/
	}	

	//THIS USING RESONANCE
/*	private void ActiveFilter(float[] data, int cutoffFreq, float resonance, FilterType filtertype) {
		
		float[] ampIn = {0,0,0};
		float[] ampOut = {0,0,0};
		
		if (cutoffFreq > 0 && cutoffFreq < 20000) {
			switch (filtertype) {
			case FilterType.lowpass:
				getLoPFilterCoefficients (ampIn, ampOut, cutoffFreq, resonance);
				break;
			case FilterType.highpass:
				getHiPFilterCoefficients (ampIn, ampOut, cutoffFreq, resonance);
				break;
			case FilterType.bandpass:
				getBPFilterCoefficients(ampIn, ampOut, cutoffFreq, resonance);
				break;
			}
			filter(data, ampIn, ampOut);
		}
	}*/
	/*
	private void FreqFilter(float[] data, int cutoffreq, FilterType filtertype) {
		float sqr2 = Mathf.Sqrt (2);
		float pi = Mathf.PI;
		
		float[] ampIn = {0,0,0};
		float[] ampOut = {0,0,0};

		float c, c2, csqr2, d = 0;

		//LoP
		switch (type) {
		case (FilterType.lowpass):
				c = 1 / Mathf.Tan((Mathf.PI / samplerate) * cutoffFreq);
				c2 = c * c;
				csqr2 = sqr2 * c;
				d = (c2 + csqr2 + 1);
		
				ampIn[0] = 1 / d;		
				ampIn[1] = ampIn[0] + ampIn[0];
				ampIn[2] = ampIn[0];
				
				ampOut[1] = (2 * (1 - c2)) / d;
				ampOut[2] = (c2 - csqr2 + 1) / d;
			break;
		
		//HiP
		case (FilterType.highpass) :
			c = Mathf.Tan((pi / samplerate) * cutoffFreq);
			c2 = c * c;
			csqr2 = sqr2 * c;
			d = (c2 + csqr2 + 1);
			ampIn[0] = 1 / d + (c);
			ampIn[1] = -(ampIn[0] + ampIn[0]);
			ampIn[2] = ampIn[0];
			ampOut[1] = (2 * (c2 - 1)) / d;
			ampOut[2] = (1 - csqr2 + c2) / d;
			break;

		//BP
		case (FilterType.bandpass) :		
			c = 1 / Mathf.Tan((pi / samplerate) * cutoffFreq);
			d = 1 + c;
			ampIn[0] = 1 / d;
			ampIn[1] = 0;
			ampIn[2] = -ampIn[0];

			ampOut[1] = (-c*2* Mathf.Cos(2* pi *cutoffFreq / samplerate)) / d;
			ampOut[2] = (c-1) / d;
			break;
		}

		for (int i=0; i<data.Length; i++) {
			sigOut[0] = (ampIn[0] * data[i])
				+ (ampIn[1] * sigIn[1])	
					+ (ampIn[2] * sigIn[2])
					- (ampOut[1] * sigOut[1])
					- (ampOut[2] * sigOut[2]);
		
			sigOut[2] = sigOut[1];
			sigOut[1] = sigOut[0];
			sigIn[2] = sigIn[1];
			sigIn[1] = data[i];
		
			data[i]= sigOut[0];
		}
	}*/
		public void ActiveFilter(float[] data, float frequency, float resonance, FilterType filtertype) {

		float[] ampIn = {0,0,0};
		float[] ampOut = {0,0,0};
		
		float c, c2, csqr2, d = 0;

		switch (filtertype) {
		case FilterType.lowpass: //lowpass
			c = 1 / (float)Mathf.Tan (Mathf.PI * frequency / samplerate);
			ampIn[0] = 1 / (1+ resonance * c + c * c);
			ampIn[1] = 2f * ampIn[0];
			ampIn[2] = ampIn[0];
			ampOut[1] = 2.0f * (1.0f - c * c) * ampIn[0];
			ampOut[2] = (1.0f - resonance * c + c * c) * ampIn[0];
			break;
			
		case FilterType.highpass: //highpass
			c = (float)Mathf.Tan (Mathf.PI * frequency / samplerate);
			ampIn[0] = 1 / (1 + resonance * c + c*c);
			ampIn[1] = -2f * ampIn[0];
			ampIn[2] = ampIn[0];
			ampOut[1] = 2 * (c * c - 1.0f) * ampIn[0];
			ampOut[2] = (1.0f - resonance * c + c * c) * ampIn[0];
			break;
			
		case FilterType.bandpass: //bandpass

			c = 1 / Mathf.Tan((Mathf.PI / samplerate) * cutoffFreq);
			ampIn[0] = c/ resonance;
			ampIn[1] = 0;
			ampIn[2] = -ampIn[0];

//		ampOut[0] = (c ) + (c * c) + 1;
			ampOut[1] = -2*(c*c-1);
			ampOut[2] = -(c / resonance) + (c * c) + 1;
		


//			c = 1 / Mathf.Tan((Mathf.PI / samplerate) * cutoffFreq);
//			ampIn[0] = 1 / (1+ resonance*c + c);
//			ampIn[1] = 0;
//			ampIn[2]= -ampIn[0];
//			ampOut[1] = (-c * 2 * Mathf.Cos( 2 * (Mathf.PI * cutoffFreq / samplerate)) / (1+c));
//			ampOut[2] = (c-1) / (1+c) * ampIn[0];
			break;
		}
		
		for (int i=0; i<data.Length; i++) {
			float newOutput = ampIn[0] * data[i] + ampIn[1] * sigIn [0] + ampIn[2] * sigIn [1] - ampOut[1] * sigOut [0] - ampOut[2] * sigOut [1];
			
			sigIn [1] = sigIn [0];
			sigIn [0] = data [i];
			
			sigOut [2] = sigOut [1];
			sigOut [1] = sigOut [0];
			sigOut [0] = newOutput;

			data[i]= sigOut[0];

		}
		
	}


/*		
	void getLoPFilterCoefficients(float[] ampIn, float[] ampOut, int cutoffFreq, float resonance) {

		float cWarp = 1.0f / (float)Mathf.Tan(Mathf.PI * cutoffFreq / samplerate);

		ampIn[0] = 1 / (1 + resonance * cWarp + cWarp * cWarp);
		ampIn[1] = 2f * ampIn[0];
		ampIn[2] = ampIn[0];
		ampOut[0] = 2.0f * (1.0f - cWarp * cWarp) * ampIn[0];
		ampOut[1] = (1.0f - resonance * cWarp + cWarp * cWarp) * ampIn[0];
	}

	void getHiPFilterCoefficients(float[] ampIn, float[] ampOut, int cutoffFreq, float resonance) {

		float c = Mathf.PI * cutoffFreq / samplerate;
		float cWarp = Mathf.Tan (c);

		ampIn[0] = 1.0f / (1.0f + resonance * c + c * c);
		ampIn[1] = -2f * ampIn[0];
		ampIn[2] = ampIn[0];
		ampOut[1] = 2.0f * (cWarp * cWarp - 1.0f) * ampIn[0];
		ampOut[2] = (1.0f - resonance * c + c * c) * ampIn[0];

	}

	void getBPFilterCoefficients(float[] ampIn, float[] ampOut, int cutoffFreq, float resonance) {

		float c = (Mathf.PI / samplerate) * cutoffFreq;
		float cWarp = 1 / Mathf.Tan(c);

		ampIn[0] = cWarp/ resonance;
		ampIn[1] = 0;
		ampIn[2] = -ampIn[0];

		ampOut[0] = (cWarp / resonance) + (cWarp * cWarp) + 1;
		ampOut[1] = -2*(cWarp*cWarp-1);
		ampOut[2] = -(cWarp / resonance) + (cWarp * cWarp) + 1;
		
//		ampOut[1] = (-cWarp*2* Mathf.Cos(2* Mathf.PI *cutoffFreq / samplerate)) / (1+cWarp);
//		ampOut[2] = (cWarp-1) / (1+cWarp + resonance * c);

	}

	
	private void filter(float[]data, float[] ampIn, float[] ampOut) {
		
		// data in -> sigIn[0], sigIn[1], sigIn[2], sigOut[0], sigOut[1], sigOut[2]
		for (int i=0; i< data.Length; i++) {
			
			//shift incoming
			sigIn [2] = sigIn [1];
			sigIn [1] = sigIn [0];
			
			//read in new sample
			sigIn [0] = data [i];
			
			//shift outgoing
			sigOut [2] = sigOut [1];
			sigOut [1] = sigOut [0];
			
			sigOut [0] = (ampIn [0] * sigIn [0] + ampIn [1] * sigIn [1] + ampIn [2] * sigIn [2]
			              - ampOut [1] * sigOut [1]
			              - ampOut [2] * sigOut [2]);
			
			data [i] = (float)sigOut [0];
		}
	}

*/
	void Start () {
		samplerate= AudioSettings.outputSampleRate;
		cutoffFreq=200;
		highCutoffFreq = 1000;
		upperClip = 0.5f;
		lowerClip = -0.5f;
		
		sigIn= new float[] {0,0,0};
		sigOut= new float[] {0,0,0};
		
	}

	public void reset() {
				sigIn = new float[] {0,0,0};
				sigOut = new float[] {0,0,0};
		}


	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey ("up") && cutoffFreq <20000) {
			cutoffFreq+=10;
			Debug.Log (cutoffFreq);
				}
		if (Input.GetKey ("down") && cutoffFreq > 10) {
			cutoffFreq-=10;
			Debug.Log (cutoffFreq);

		}
		if (Input.GetKey ("left") && highCutoffFreq <20000) {
			highCutoffFreq-=10;
			Debug.Log (highCutoffFreq);
		}
		if (Input.GetKey ("right") && highCutoffFreq > 10) {
			highCutoffFreq+=10;
			Debug.Log (highCutoffFreq);
			
		}

		if (Input.GetKey ("l")) {
			type = FilterType.lowpass;
			Debug.Log ("Lowpass");
		}
		if (Input.GetKey ("h")) {
			type = FilterType.highpass;
			Debug.Log ("Highpass");
		}
		if (Input.GetKey ("b")) {
			type = FilterType.bandpass;
			Debug.Log ("Bandpass");
		}
		if (Input.GetKey ("a")) {
			resonance-=1;
			Debug.Log ("Resonance decreased to" + resonance);
		}
		if (Input.GetKey ("s")) {
			resonance+=1;
			Debug.Log ("Resonance increased to" + resonance);
		}

	}
	
}
