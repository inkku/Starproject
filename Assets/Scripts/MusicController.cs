using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	public AudioSource[] myAudioStems;

	public AudioClip[] myclips;


	public int beatsPerMeasure=4;
	public int BPM = 125;

	public float pulse;

	public float beatTimer;

	public int stemSelect;

	public float[] stemVol = new float[6]; 
	public bool[] onBeat = new bool[6];

	public float[] mix1 = new float[] {0f, 0f, 1f, 0f, 0f, 0f};
	public float[] mix2 = new float[] {0f, 0.6f, 1f, 0.2f, 0f, 0.3f};
	public float[] mix3 = new float[] {0f, 1f, 0.8f, 0.6f, 0.4f, 0.75f};
	public float[] mix4 = new float[] {0.6f, 1f, 0.6f, 0.8f, 0.8f, 1f};
	public float[] mix5 = new float[] {0.8f, 1f, 0.3f, 1f, 1f, 1f};
	public float[] mix6 = new float[] {1f, 1f, 0.2f, 1f, 1f, 1f};



	// Use this for initialization
	void Start () {

		pulse = beatsPerMeasure* 6000 / BPM;
/*		myclips = new string[] {"SETIjam00 - ampedUp00.mp3", 
			"SETIjam00 - bassbasic.mp3",
			"SETIjam00 - dronySynths00.mp3",
			"SETIjam00 - hiPerc00.mp3",
			"SETIjam00 - midIntense.mp3",
			"SETIjam00 - pulse00.mp3"};
*/
		myAudioStems = new AudioSource[6];
		for (int i=0; i<6; i++)
		{

			myAudioStems[i]=gameObject.AddComponent("AudioSource") as AudioSource;
			myAudioStems[i].clip = (AudioClip) myclips[i];

			if(myAudioStems[i].clip.isReadyToPlay)
			{
				myAudioStems[i].Play ();
			}
		}

		beatTimer = 0;

		foreach (AudioSource s in myAudioStems) {
			s.loop = true;
			s.panLevel = 0;
			s.dopplerLevel = 0;
				}

		//stemVol [2] = 1;
		//float setPulseVol = 0.3f;
		//stemVol [1] = 0.5f;

		mix1 = new float[] {0f, 0f, 1f, 0f, 0f, 0f};
		mix2 = new float[] {0f, 0.6f, 1f, 0.2f, 0f, 0.3f};
		mix3 = new float[] {0f, 1f, 0.8f, 0.6f, 0.4f, 0.75f};
		mix4 = new float[] {0.6f, 1f, 0.6f, 0.8f, 0.8f, 1f};
		mix5 = new float[] {0.8f, 1f, 0.3f, 1f, 1f, 1f};
		mix6 = new float[] {1f, 1f, 0.2f, 1f, 1f, 1f};
	}
	
	// Update is called once per frame
	void Update () {

		beatTimer += Time.deltaTime;
		int currentStar = GameHandler.Instance.currentStar.typeNum;

		switch (currentStar)
		{
		case 0:
			for (int i = 0; i < stemVol.Length; i++) {
				stemVol[i] = mix1[i];
				Debug.Log("mix1");
			}
			break;
		case 1:
			for (int i = 0; i < stemVol.Length; i++) {
				stemVol[i] = mix2[i];
				Debug.Log("mix1");
			}
			break;
		case 2:
			for (int i = 0; i < stemVol.Length; i++) {
				stemVol[i] = mix2[i];
			}
			break;
		case 3:
			for (int i = 0; i < stemVol.Length; i++) {
				stemVol[i] = mix3[i];
			}
			break;
		case 4:
			for (int i = 0; i < stemVol.Length; i++) {
				stemVol[i] = mix4[i];
			}
			break;
		case 5:
			for (int i = 0; i < stemVol.Length; i++) {
				stemVol[i] = mix5[i];
			}
			break;
		case 6:
			for (int i = 0; i < stemVol.Length; i++) {
				stemVol[i] = mix6[i];
			}
			break;

			

		}


		for (int i=0; i<6; i++) {
			myAudioStems [i].volume = stemVol [i];

		}
	/*
				// UPDATES ON BEAT
				/if (beatTimer >= pulse) 
				{
					beatTimer = (float)0;
					Debug.Log ("BEAT");

					for (int i=0; i<6; i++) {
						if (onBeat [i]) {	
							myAudioStems [i].volume = (float)stemVol [i];
					}
				}

				// REGULAR UPDATES
				for (int i=0; i<6; i++) 
				{
					if (!onBeat [i]) {
							myAudioStems [i].volume = (float) stemVol [i];
					}
				}
		} */

//		foreach( audioTrack in myTrackList) {

//		global styrning
//		audioTrack.volume = matematikHär * StarManager.Instance.nrStarsOfType[stjärntyp]

/*		
			StarManager.Instance.nrStarsOfTypeWithinRange
		
		public float EnergyMax;

			- Musik based on stjärnornas procentuella andel
			7 olika stjärnorter: intensitet (high to low): int array [6]: låg till hög, 
			   public int[] nrStarOfType
			
			- området du är nu
			public int[] nrStarOfTypeWithinRange
			
			public float EnergyCurrent
			public float EnergyMax
			
			Skicka in lowpassfilter
			
			-> Alla igång, volymkontroll
*/

	
	}
}