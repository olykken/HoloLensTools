using System;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;

using UnityEngine.Video;

public class KeywordScript : MonoBehaviour
{
    [SerializeField]
    private string[] m_Keywords;

    // TEMP HARDWIRE TO VIDEO
    public GameObject videoParent;
    public VideoPlayer mVideo;

    private KeywordRecognizer m_Recognizer;

    void Start()
    {
        m_Keywords = new string[] { "Play", "Stop", "Back", "Forward", "Pause" };
        m_Recognizer = new KeywordRecognizer(m_Keywords);
        m_Recognizer.OnPhraseRecognized += OnPhraseRecognized;
        m_Recognizer.Start();

        mVideo = videoParent.GetComponent<VideoPlayer>();
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
        builder.AppendFormat("\tTimestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
        builder.AppendFormat("\tDuration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
        Debug.Log(builder.ToString());

        string word = args.text.ToLower();
        if (word == "play") { if (!mVideo.isPlaying && mVideo.isActiveAndEnabled) { mVideo.Play(); } }
        if (word == "stop") { if (mVideo.isPlaying && mVideo.isActiveAndEnabled) { mVideo.Stop(); } }
        if (word == "pause") { if (mVideo.isPlaying && mVideo.isActiveAndEnabled) { mVideo.Pause(); } }
        if (word == "back") { if (mVideo.isPlaying && mVideo.isActiveAndEnabled) { mVideo.frame = mVideo.frame - 450; } }
        if (word == "forward") { if (mVideo.isPlaying && mVideo.isActiveAndEnabled) { mVideo.frame = mVideo.frame + 450; } }
    }
}