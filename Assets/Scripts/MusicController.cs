using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	public AudioSource[] myAudioStems;

	public AudioClip[] myclips;


	public int stemSelect;

	public float[] stemVol = new float[6]; 

	// Use this for initialization
	void Start () {

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

/*		AudioSource
		myAudioStems[0].clip = Resources.Load("Audio/Music Stems/SETIjam00 - ampedUp00.mp3") as AudioClip;
		myAudioStems[1].clip = Resources.Load("Audio/Music Stems/SETIjam00 - bassbasic.mp3") as AudioClip;
		myAudioStems[2].clip = Resources.Load("Audio/Music Stems/SETIjam00 - dronySynths00.mp3") as AudioClip;
		myAudioStems[3].clip = Resources.Load("Audio/Music Stems/SETIjam00 - hiPerc00.mp3") as AudioClip;
		myAudioStems[4].clip = Resources.Load("Audio/Music Stems/SETIjam00 - midIntense.mp3") as AudioClip;
		myAudioStems[5].clip = Resources.Load("Audio/Music Stems/SETIjam00 - pulse00.mp3") as AudioClip;
*/


	//	AudioSource[] myTrackList = new AudioSource[];
	
	}
	
	// Update is called once per frame
	void Update () {

		for (int i=0; i<6; i++) {
			myAudioStems [i].volume = stemVol [i];
		}

	
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
