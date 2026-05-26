using MyApp.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace MyApp.Domain.Entities
{
    /// <summary>
    /// Eine dynamische Sammlung von Tracks, definiert durch Filter-Regeln.
    /// </summary>
    public sealed class SmartCollection
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = "Folder"; // Standard-Icon
        
        /// <summary>
        /// Liste der Regeln, die ein Track erfüllen muss, um Teil dieser Sammlung zu sein.
        /// </summary>
        public List<FilterRule> Rules { get; init; } = new();

        /// <summary>
        /// Gibt an, ob ALLLE Regeln (AND) oder EINE der Regeln (OR) erfüllt sein müssen.
        /// </summary>
        public bool MatchAllRules { get; set; } = true;
    }
}
