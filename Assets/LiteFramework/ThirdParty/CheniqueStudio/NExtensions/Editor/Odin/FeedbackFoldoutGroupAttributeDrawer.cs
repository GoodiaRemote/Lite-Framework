using CheniqueStudio.NExtensions.NTools.Odin;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace CheniqueStudio.NExtensions.Editor.Odin
{
    public class FeedbackFoldoutGroupAttributeDrawer : OdinGroupDrawer<FeedbackGroupAttribute>
    {
        private LocalPersistentContext<bool> isExpanded;

        protected override void Initialize()
        {
            isExpanded = this.GetPersistentValue(
                "ColoredFoldoutGroupAttributeDrawer.isExpanded",
                GeneralDrawerConfig.Instance.ExpandFoldoutByDefault);
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            GUIHelper.PushColor(new Color(Attribute.R, Attribute.G, Attribute.B, Attribute.A));
            SirenixEditorGUI.BeginBox();
            SirenixEditorGUI.BeginBoxHeader();
            GUIHelper.PopColor();
            isExpanded.Value = SirenixEditorGUI.Foldout(isExpanded.Value, label);
            SirenixEditorGUI.EndBoxHeader();

            if (SirenixEditorGUI.BeginFadeGroup(this, isExpanded.Value))
                for (var i = 0; i < Property.Children.Count; i++)
                    Property.Children[i].Draw();

            SirenixEditorGUI.EndFadeGroup();
            SirenixEditorGUI.EndBox();
        }
    }
}