using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryGame.Anirudh.Bhandari
{
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager instance;

        public static SoundManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<SoundManager>();
                }

                return instance;
            }
        }

        public SoundClip correctTile;

        public SoundClip incorrectTile;

        public SoundClip start;

        public SoundClip win;

        public SoundClip lose;

        private void Awake()
        {
            correctTile.time = correctTile.audioClip.length;

            incorrectTile.time = incorrectTile.audioClip.length;

            start.time = start.audioClip.length;

            win.time = win.audioClip.length;

            lose.time = lose.audioClip.length;
        }

        public void PlaySound(Sounds e)
        {
            GameObject m = new GameObject("Audio", typeof(AudioSource));

            AudioSource audioSource = m.GetComponent<AudioSource>();

            AudioClip clip = null;

            switch(e)
            {
                case Sounds.CorrectTile: clip = correctTile.audioClip;
                    break;
                case Sounds.IncorrectTile: clip = incorrectTile.audioClip;
                    break;
                case Sounds.Start:clip = start.audioClip;
                    break;
                case Sounds.Win:clip = win.audioClip;
                    break;
                case Sounds.Lose:clip = lose.audioClip;
                    break;
            }
            audioSource.PlayOneShot(clip);

            StartCoroutine(SetDestroyObject(m, clip.length));
        }

        private IEnumerator SetDestroyObject(GameObject o, float t)
        {
            yield return new WaitForSeconds(t);

            Destroy(o);
        }
    }

    public enum Sounds
    {
        CorrectTile,
        IncorrectTile,
        Start,
        Win,
        Lose
    }
    [System.Serializable]
    public class SoundClip
    {
        public AudioClip audioClip;

        public float time;
    }
}
