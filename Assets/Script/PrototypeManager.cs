using UnityEngine;
using System.Collections;

public enum PGameType {
	Start,
	Run,
	Stop
}

public class PrototypeManager : MonoBehaviour {

	public PrototypeLabel plabel;
	public Animator questionHalo;
	public Animator answerHalo;
	public PGameType gameType;

	public AudioSource musicSource;
	public AudioSource questionSource;
	public AudioSource answerSource;
	public PrototypeAudioLibrary audioLibrary;

	int selectedTheme;
	int selectedSong;

	// Use this for initialization
	void Start () {
		Restart ();
	}

	public void Restart() {
		answerSource.Stop ();
		musicSource.Stop ();
		questionSource.Stop ();

		gameType = PGameType.Start;
		plabel.ChangeLabel (PLabel.Start);
		answerSource.clip = audioLibrary.answer;
		selectedTheme = Random.Range (0,audioLibrary.audioTheme.Length);
		musicSource.clip = audioLibrary.audioTheme [selectedTheme].intro;
		questionSource.clip = audioLibrary.audioTheme [selectedTheme].question;
	}

	public void Begin() {
		gameType = PGameType.Run;
		plabel.ChangeLabel (PLabel.None);
		musicSource.Play ();
	}

	public void TapAction() {

		switch (gameType) {
		case PGameType.Start:
			Begin ();
			break;
		case PGameType.Run:
			answerHalo.SetTrigger ("Anim");
			plabel.ChangeLabel (PLabel.Perfect);
			answerSource.Play ();
			break;
		case PGameType.Stop:
			break;
		}

	}

	void RandomizeSong() {
		selectedSong = Random.Range (0,audioLibrary.audioTheme[selectedTheme].segments.Length);
		musicSource.clip = audioLibrary.audioTheme [selectedTheme].segments[selectedSong];
		musicSource.Play ();
	}

	void Update() {
		if ((gameType==PGameType.Run) && (!musicSource.isPlaying))
			RandomizeSong ();
	}
}
