using UnityEngine;
using System.Collections;

public enum PGameType {
	Start,
	Intro,
	Run
}

public class PrototypeManager : MonoBehaviour {

	public PrototypeLabel plabel;
	public Animator questionHalo;
	public Animator answerHalo;
	public PGameType gameType;

	public AudioSource musicSource;
	public AudioSource questionSource;
	public AudioSource answerSource;
	public SOAudioBanksClass audioLibrary;
	public SOQuestionClass[] question;

	int selectedTheme;
	int selectedSong;
	int selectedQuestion;
	float startQuestion;
	int curQuestion;

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
		gameType = PGameType.Intro;
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
		case PGameType.Intro:
			break;
		}

	}

	void RandomizeSong() {
		gameType = PGameType.Run;
		selectedSong = Random.Range (0,audioLibrary.audioTheme[selectedTheme].segments.Length);
		musicSource.clip = audioLibrary.audioTheme [selectedTheme].segments[selectedSong];
		RandomizeQuestion ();
		musicSource.Play ();
	}

	void Update() {
		if (gameType == PGameType.Run) {
			if (!musicSource.isPlaying)
				RandomizeSong ();

			float curDelta = Time.timeSinceLevelLoad - startQuestion;
			if (curDelta >= question[selectedTheme].qData [selectedQuestion].length)
				RandomizeQuestion ();
			else if ((curQuestion < question[selectedTheme].qData [selectedQuestion].actionTime.Length) && (curDelta >= question[selectedTheme].qData [selectedQuestion].actionTime [curQuestion])) {
				print ("Tap: "+curDelta+" ["+curQuestion+"/"+question[selectedTheme].qData [selectedQuestion].actionTime.Length+"]");
				PlayQuestion ();
			}
		} else if (gameType == PGameType.Intro) {
			if (!musicSource.isPlaying)
				RandomizeSong ();
		}
	}

	void RandomizeQuestion() {		
		startQuestion = Time.timeSinceLevelLoad;
		curQuestion = 0;
		selectedQuestion = Random.Range (0,question[selectedTheme].qData.Length);
		print ("Question Randomized");
	}

	void PlayQuestion() {
		curQuestion++;
		questionSource.Play ();
		questionHalo.SetTrigger ("Anim");

	}
}
