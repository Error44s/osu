// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace osu.Game.Rulesets.Objects.Types
{
    /// <summary>
    /// A type of <see cref="HitObject"/> which may or may not generate ticks.
    /// </summary>
    public interface IHasGenerateTicks
    {
        /// <summary>
        /// Whether or not slider ticks should be generated at this control point.
        /// This exists for backwards compatibility with maps that abuse NaN slider velocity behavior on osu!stable (e.g. /b/2628991).
        /// </summary>
        public bool GenerateTicks { get; set; }
    }
}
