using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	public AudioSource[] myAudioStems;

	public AudioClip[] myclips;


	public int beatsPerTime=4;
	public int BPM = 125;

	public float beatTimer;


	public int stemSelect;

	public float[] stemVol = new float[6]; 
<<<<<<< Updated upstream
	public bool[] onBeat = new bool[6];

=======
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
	public bool[] isOnTicker = new bool[6];
=======
	public bool[] onBeat = new bool[6];

>>>>>>> FETCH_HEAD
=======
	public bool[] onBeat = new bool[6];

>>>>>>> FETCH_HEAD
=======
	public bool[] onBeat = new bool[6];

>>>>>>> FETCH_HEAD
>>>>>>> Stashed changes

	// Use this for initialization
	void Start () {


<<<<<<< Updated upstream
=======
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
=======
>>>>>>> FETCH_HEAD
=======
>>>>>>> FETCH_HEAD
>>>>>>> Stashed changes
/*		myclips = new string[] {"SETIjam00 - ampedUp00.mp3", 
			"SETIjam00 - bassbasic.mp3",
			"SETIjam00 - dronySynths00.mp3",
			"SETIjam00 - hiPerc00.mp3",
			"SETIjam00 - midIntense.mp3",
			"SETIjam00 - pulse00.mp3"};
*/
>>>>>>> FETCH_HEAD
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
<<<<<<< Updated upstream
		beatTimer = 0;

=======
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
		foreach (AudioSource s in myAudioStems) {
			s.loop = true;
			s.panLevel = 0;
			s.dopplerLevel = 0;
				}
		stemVol [2] = 1;
=======
		beatTimer = 0;

>>>>>>> FETCH_HEAD
=======
		beatTimer = 0;

>>>>>>> FETCH_HEAD
=======
		beatTimer = 0;

>>>>>>> FETCH_HEAD
>>>>>>> Stashed changes

	
	}
	
	// Update is called once per frame
	void Update () {

<<<<<<< Updated upstream
				beatTimer += Time.deltaTime;

=======
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD


		for (int i=0; i<6; i++) {
			myAudioStems [i].volume = stemVol [i];

		}
	
=======
				beatTimer += Time.deltaTime;

=======
				beatTimer += Time.deltaTime;

>>>>>>> FETCH_HEAD
=======
				beatTimer += Time.deltaTime;

>>>>>>> FETCH_HEAD
>>>>>>> Stashed changes
				for (int i=0; i<6; i++) {
						if (onBeat [i] && beatTimer > (beatsPerTime * 60 / BPM)) {	
								myAudioStems [i].volume = stemVol [i];
								Debug.Log ("BEAT");
								beatTimer = 0;
						} else if (!onBeat [i]) {
								myAudioStems [i].volume = stemVol [i];
						}

				}
		}
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> FETCH_HEAD
=======
>>>>>>> FETCH_HEAD
=======
>>>>>>> FETCH_HEAD
>>>>>>> Stashed changes
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
