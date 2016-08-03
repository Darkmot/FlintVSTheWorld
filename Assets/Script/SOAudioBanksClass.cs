using UnityEngine;
using System.Collections;

[System.Serializable]
public class AudioTheme {
	public string name;
	public AudioClip intro;
	public AudioClip[] segments;
	public AudioClip question;
}

[CreateAssetMenu(fileName = "SOAudioBanks",menuName = "Prototype/Audio Banks",order = 1)]
public class SOAudioBanksClass : ScriptableObject {

	public AudioTheme[] audioTheme;
	public AudioClip answer;

}

