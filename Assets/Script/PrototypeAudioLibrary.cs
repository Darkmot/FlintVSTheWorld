using UnityEngine;
using System.Collections;

[System.Serializable]
public class AudioTheme {
	public string name;
	public AudioClip intro;
	public AudioClip[] segments;
	public AudioClip question;
}


public class PrototypeAudioLibrary : MonoBehaviour {

	public AudioTheme[] audioTheme;
	public AudioClip answer;

}
