// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Input;

namespace osu.Game.Screens.Edit.Compose.Components
{
    public partial class SelectionBoxScaleHandle : SelectionBoxDragHandle
    {
        [Resolved]
        private SelectionBox selectionBox { get; set; } = null!;

        [Resolved]
        private SelectionScaleHandler? scaleHandler { get; set; }

        [BackgroundDependencyLoader]
        private void load()
        {
            Size = new Vector2(10);
        }

        protected override bool OnDragStart(DragStartEvent e)
        {
            if (e.Button != MouseButton.Left)
                return false;

            if (scaleHandler == null) return false;

            scaleHandler.Begin();
            return true;
        }

        private Vector2 getOriginPosition()
        {
            var quad = scaleHandler!.OriginalSurroundingQuad!.Value;
            Vector2 origin = quad.TopLeft;

            if ((Anchor & Anchor.x0) > 0)
                origin.X += quad.Width;

            if ((Anchor & Anchor.y0) > 0)
                origin.Y += quad.Height;

            return origin;
        }

        private Vector2 rawScale;

        protected override void OnDrag(DragEvent e)
        {
            base.OnDrag(e);

            if (scaleHandler == null) return;

            rawScale = convertDragEventToScaleMultiplier(e);

            applyScale(shouldKeepAspectRatio: e.ShiftPressed);
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (IsDragged && (e.Key == Key.ShiftLeft || e.Key == Key.ShiftRight))
            {
                applyScale(shouldKeepAspectRatio: true);
                return true;
            }

            return base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyUpEvent e)
        {
            base.OnKeyUp(e);

            if (IsDragged && (e.Key == Key.ShiftLeft || e.Key == Key.ShiftRight))
                applyScale(shouldKeepAspectRatio: false);
        }

        protected override void OnDragEnd(DragEndEvent e)
        {
            scaleHandler?.Commit();
        }

        private Vector2 convertDragEventToScaleMultiplier(DragEvent e)
        {
            Vector2 scale = e.MousePosition - e.MouseDownPosition;
            adjustScaleFromAnchor(ref scale);
            return Vector2.Divide(scale, scaleHandler!.OriginalSurroundingQuad!.Value.Size) + Vector2.One;
        }

        private void adjustScaleFromAnchor(ref Vector2 scale)
        {
            // cancel out scale in axes we don't care about (based on which drag handle was used).
            if ((Anchor & Anchor.x1) > 0) scale.X = 1;
            if ((Anchor & Anchor.y1) > 0) scale.Y = 1;

            // reverse the scale direction if dragging from top or left.
            if ((Anchor & Anchor.x0) > 0) scale.X = -scale.X;
            if ((Anchor & Anchor.y0) > 0) scale.Y = -scale.Y;
        }

        private void applyScale(bool shouldKeepAspectRatio)
        {
            var newScale = shouldKeepAspectRatio
                ? new Vector2(MathF.Max(rawScale.X, rawScale.Y))
                : rawScale;

            scaleHandler!.Update(newScale, getOriginPosition());
        }
    }
}
