// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using osu.Game.Beatmaps;
using osu.Game.Database;
using Realms;

namespace osu.Game.Collections
{
    /// <summary>
    /// A collection of beatmaps grouped by a name.
    /// </summary>
    public class BeatmapCollection : RealmObject, IHasGuidPrimaryKey
    {
        [PrimaryKey]
        public Guid ID { get; set; }

        /// <summary>
        /// The collection's name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The <see cref="BeatmapInfo.MD5Hash"/>es of beatmaps contained by the collection.
        /// </summary>
        public IList<string> BeatmapMD5Hashes { get; } = null!;

        /// <summary>
        /// The date when this collection was last modified.
        /// </summary>
        public DateTimeOffset LastModified { get; set; }

        public BeatmapCollection(string? name = null, IList<string>? beatmapMD5Hashes = null)
        {
            ID = Guid.NewGuid();
            Name = name ?? string.Empty;
            BeatmapMD5Hashes = beatmapMD5Hashes ?? new List<string>();

            LastModified = DateTimeOffset.UtcNow;
        }

        [UsedImplicitly]
        private BeatmapCollection()
        {
        }
    }
}
