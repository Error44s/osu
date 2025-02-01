// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Beatmaps;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Osu.Beatmaps;
using osu.Game.Rulesets.Osu.Objects;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class OsuModAutoTiming : ModAutoTiming, IApplicableToBeatmap
    {
        public void ApplyToBeatmap(IBeatmap beatmap)
        {
            if (beatmap is not OsuBeatmap osuBeatmap)
                return;

            var random = new System.Random();

            foreach (var hitObject in osuBeatmap.HitObjects)
            {
                if (hitObject is HitCircle hitCircle)
                {
                    double offset = random.NextDouble() * MaxTimeOffset.Value - MaxTimeOffset.Value / 2;

                    hitCircle.StartTime += offset;
                }
            }
        }
    }
}
