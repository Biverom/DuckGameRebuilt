﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace DuckGame;

[AttributeUsage(AttributeTargets.Field)]
public sealed class AutoConfigFieldAttribute : Attribute
{
    public static IReadOnlyList<MemberAttributePair<FieldInfo, AutoConfigFieldAttribute>> All;

    static AutoConfigFieldAttribute()
    {
        MemberAttributePair<FieldInfo, AutoConfigFieldAttribute>.RequestSearch(all => All = all);
    }

    public string? ShortName { get; set; } = null;
    /// <summary>
    /// Whether or not this field will be saved in the event of a crash.
    /// This can be potentially dangerous if the reason for the crash is this
    /// specific config field. In which case the user will be stuck in an
    /// eternal crash loop.
    /// </summary>
    public bool PotentiallyDangerous { get; set; } = true;

    public string? Id { get; set; } = null;
}