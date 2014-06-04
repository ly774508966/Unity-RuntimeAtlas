using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class test : MonoBehaviour {
	public List<Texture2D> texturesToPack = new List<Texture2D>();

	protected List<Texture2D> eligable = null;
	protected List<Texture2D> building = null;
	protected List<bool> selected = null;
	protected List<AtlasCreator.Atlas> atlases = null;
	protected Vector2 scrollView = Vector2.zero;
	protected int selectedAtlas = 0;
	protected bool addToAtlas = false;

	void Awake() {
		atlases = new List<AtlasCreator.Atlas>();
		eligable = new List<Texture2D>(texturesToPack);
		selected = new List<bool>();
		foreach (Texture2D t in eligable)
			selected.Add(false);
		addToAtlas = true;
	}

	void OnGUI() {
		if (addToAtlas) {
			GUILayout.BeginVertical();

			int width = Screen.width / 100;

			GUILayout.Space(15.0f);
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.Label("<size=30>Select images to add to atlas</size>");
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();

			scrollView = GUILayout.BeginScrollView(scrollView);
			GUILayout.BeginHorizontal();
			for (int i = 0; i < eligable.Count; ++i) {
				if (i%width == 0) GUILayout.EndHorizontal();
				if (i%width == 0) GUILayout.BeginHorizontal();
				if (!selected[i]) {
					if (GUILayout.Button(eligable[i], GUILayout.Width(100), GUILayout.Height(100))) {
						selected[i] = true;
					}
				}
				else {
					GUILayout.Label(eligable[i], GUILayout.Width(100), GUILayout.Height(100));
				}
			}
			GUILayout.EndHorizontal();
			GUILayout.EndScrollView();

			GUILayout.Space(25.0f);
			bool madeSelection = false;
			foreach(bool b in selected) {
				if (b) {
					madeSelection = true;
					break;
				}
			}

			if (madeSelection) {
				if (GUILayout.Button("Add Selected Images To Atlas", GUILayout.Height(75.0f), GUILayout.Width(Screen.width))) {
					building = new List<Texture2D>();

					for (int i = eligable.Count - 1; i >= 0; --i) {
						if (selected[i]) {
							building.Add(eligable[i]);
							eligable.RemoveAt(i);
							selected.RemoveAt(i);
						}
					}

					AtlasCreator.Atlas[] newAtlases = AtlasCreator.CreateAtlas("test", building.ToArray(), atlases == null || atlases.Count == 0? null : atlases[atlases.Count - 1]);
					foreach(AtlasCreator.Atlas newAtlas in newAtlases) {
						if (!atlases.Contains(newAtlas)) {
							atlases.Add(newAtlas);
						}
					}
					addToAtlas = false;
				}
			}
			else {
				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				GUILayout.Label("<size=30>Select a few images to continue</size>");
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();
			}

			GUILayout.EndVertical();
		}
		else if (atlases != null && atlases.Count >= 1) {
			GUILayout.BeginVertical();

			GUILayout.Space(15.0f);
			GUILayout.BeginHorizontal();
			if (atlases.Count > 1) {
				if (GUILayout.Button("<<", GUILayout.Height(50.0f))) {
					selectedAtlas -= 1;
					if (selectedAtlas < 0) {
						selectedAtlas = atlases.Count - 1;
					}
				}
			}
			GUILayout.FlexibleSpace();
			GUILayout.Label("<size=30>Atlas" + (selectedAtlas + 1) + " of " + atlases.Count + "</size>");
			GUILayout.FlexibleSpace();
			if (atlases.Count > 1) {
				if (GUILayout.Button(">>", GUILayout.Height(50.0f))) {
					selectedAtlas += 1;
					if (selectedAtlas >= atlases.Count) {
						selectedAtlas = 0;
					}
				}
			}
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.Label(atlases[selectedAtlas].texture, GUILayout.Height(Screen.height - 150));
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();

			if (eligable.Count > 0) {
				if (GUILayout.Button("Add More Sprites To Atlas", GUILayout.Height(75.0f))) {
					addToAtlas = true;
				}
			}
			else {
				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				GUILayout.Label("<size=30>No More Images To Add</size>");
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();
			}

			GUILayout.EndVertical();
		}
	}
}
