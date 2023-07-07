using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class SubtitleDataPlayable : PlayableBehaviour {
    public string text = "A Subtitle";
    public Color color = Color.black;
}

[Serializable]
public class SubtitleData : PlayableAsset, ITimelineClipAsset {

    public SubtitleDataPlayable subtitleData = new SubtitleDataPlayable();

    public override Playable CreatePlayable(PlayableGraph graph, GameObject go) {
        return ScriptPlayable<SubtitleDataPlayable>.Create(graph, subtitleData);
    }

    public ClipCaps clipCaps {
        get { return ClipCaps.Blending | ClipCaps.Extrapolation; }
    }
}
