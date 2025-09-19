using System;
using UnityEngine;
using Verse;

namespace OdysseyPatch.SubstructureOverlayOptions
{
    public class Command_Dropdown : Command_Action
    {
        private Func<Texture2D> tex;

        public Command_Dropdown(Func<Texture2D> tex)
        {
            this.tex = tex;
        }

        public override void DrawIcon(Rect rect, Material buttonMat, GizmoRenderParms parms)
        {
            base.DrawIcon(rect, buttonMat, parms);
            if (!parms.shrunk)
            {
                GUI.DrawTexture(rect.TopPartPixels(24f).RightPartPixels(24f), tex());
            }
        }
    }
}
