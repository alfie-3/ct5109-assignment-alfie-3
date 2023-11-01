using System.Collections;
using UnityEngine;

namespace Dialogue {
    public class DialogueBaseNode : BaseNode {

        [Input()][HideInInspector] public int entry;

        public bool ShowTextScrolling = true;

        [HideInInspector] public float textSpeed = 1.0f;

        [HideInInspector] public int characterNameIndex;
        [HideInInspector] public string speech;
        [HideInInspector] public Color nameColour;
        [HideInInspector] public Color textColour;

        public float FontSize;

        protected override void Create() {
            if(FontSize <= 0)
                FontSize = 45.0f;
        }

        public override IEnumerator Run() {
            DisplayUI();
            yield return null;
            CallNextNode();
        }

        /// <summary>
        /// Display this DialogueNode UI
        /// </summary>
        protected virtual void DisplayUI() {
            DialogueUIManager.Instance.SetFontSize(FontSize);
            DialogueUIManager.Instance.ClearButton();
            DialogueUIManager.Instance.DisplayText(this);
            DialogueUIManager.Instance.SetColour(nameColour, textColour);
        }

        protected virtual void CallNextNode()
        {
        }

        protected void CheckText()
        {
            string[] words = speech.Split(' ');

            Mesh mesh = DialogueUIManager.Instance.CharacterText.mesh;

            foreach (var word in words) {
                if (word == "Monday")
                {
                    foreach (var item in DialogueUIManager.Instance.Text.characterInfo)
                    {
                        item.material.color = Color.blue;

                        mesh.colors[0] = item.material.color;
                    }
                }

                DialogueUIManager.Instance.CharacterText.ForceMeshUpdate();

                //Debug.Log($"<{word}>" + " " + DialogueUIManager.Instance.CharacterText.mesh.colors[0].b);
            }
        }
    }
}