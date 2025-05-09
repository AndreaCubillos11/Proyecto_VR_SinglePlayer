#if UNITY_EDITOR || ENABLE_RUNTIME_ADMIN_APIS
//-----------------------------------------------------------------------------
// <auto-generated>
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//-----------------------------------------------------------------------------
using System.Runtime.Serialization;
using UnityEngine.Scripting;

namespace Unity.Services.Multiplayer.Editor.Matchmaker.Authoring.AdminApi.Matchmaker.Model
{
    /// <summary>
    /// Restrictions
    /// </summary>
    [DataContract(Name = "Restrictions")]
    [Preserve]
    partial class Restrictions
    {
        /// <summary>
        /// The maximum number of Queues.
        /// </summary>
        /// <value>The maximum number of Queues.</value>
        [DataMember(Name = "maxQueues", EmitDefaultValue = false)]
        [Preserve]
        public int MaxQueues { get; set; }

        /// <summary>
        /// The maximum number of Pools per Queue.
        /// </summary>
        /// <value>The maximum number of Pools per Queue.</value>
        [DataMember(Name = "maxPoolsPerQueue", EmitDefaultValue = false)]
        [Preserve]
        public int MaxPoolsPerQueue { get; set; }

        /// <summary>
        /// The maximum number of Variants per Pool.
        /// </summary>
        /// <value>The maximum number of Variants per Pool.</value>
        [DataMember(Name = "maxPoolVariants", EmitDefaultValue = false)]
        [Preserve]
        public int MaxPoolVariants { get; set; }

        /// <summary>
        /// The maximum number of Teams per Match.
        /// </summary>
        /// <value>The maximum number of Teams per Match.</value>
        [DataMember(Name = "maxMatchTeams", EmitDefaultValue = false)]
        [Preserve]
        public int MaxMatchTeams { get; set; }

        /// <summary>
        /// The maximum number of Rules per Match.
        /// </summary>
        /// <value>The maximum number of Rules per Match.</value>
        [DataMember(Name = "maxMatchRules", EmitDefaultValue = false)]
        [Preserve]
        public int MaxMatchRules { get; set; }

        /// <summary>
        /// The maximum number of Team Rules per Team.
        /// </summary>
        /// <value>The maximum number of Team Rules per Team.</value>
        [DataMember(Name = "maxTeamRules", EmitDefaultValue = false)]
        [Preserve]
        public int MaxTeamRules { get; set; }

        /// <summary>
        /// The maximum number of Relaxations per Rule.
        /// </summary>
        /// <value>The maximum number of Relaxations per Rule.</value>
        [DataMember(Name = "maxRuleRelaxations", EmitDefaultValue = false)]
        [Preserve]
        public int MaxRuleRelaxations { get; set; }

        /// <summary>
        /// The maximum number of Players per Ticket.
        /// </summary>
        /// <value>The maximum number of Players per Ticket.</value>
        [DataMember(Name = "maxPlayersPerTicket", EmitDefaultValue = false)]
        [Preserve]
        public int MaxPlayersPerTicket { get; set; }

        /// <summary>
        /// The maximum timeout in seconds for a Pool.
        /// </summary>
        /// <value>The maximum timeout in seconds for a Pool.</value>
        [DataMember(Name = "maxPoolTimeout", EmitDefaultValue = false)]
        [Preserve]
        public int MaxPoolTimeout { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Restrictions" /> class.
        /// </summary>
        /// <param name="maxQueues">The maximum number of Queues..</param>
        /// <param name="maxPoolsPerQueue">The maximum number of Pools per Queue..</param>
        /// <param name="maxPoolVariants">The maximum number of Variants per Pool..</param>
        /// <param name="maxMatchTeams">The maximum number of Teams per Match..</param>
        /// <param name="maxMatchRules">The maximum number of Rules per Match..</param>
        /// <param name="maxTeamRules">The maximum number of Team Rules per Team..</param>
        /// <param name="maxRuleRelaxations">The maximum number of Relaxations per Rule..</param>
        /// <param name="maxPlayersPerTicket">The maximum number of Players per Ticket..</param>
        /// <param name="maxPoolTimeout">The maximum timeout in seconds for a Pool..</param>
        [Preserve]
        public Restrictions(int maxQueues = default(int), int maxPoolsPerQueue = default(int), int maxPoolVariants = default(int), int maxMatchTeams = default(int), int maxMatchRules = default(int), int maxTeamRules = default(int), int maxRuleRelaxations = default(int), int maxPlayersPerTicket = default(int), int maxPoolTimeout = default(int))
        {
            this.MaxQueues = maxQueues;
            this.MaxPoolsPerQueue = maxPoolsPerQueue;
            this.MaxPoolVariants = maxPoolVariants;
            this.MaxMatchTeams = maxMatchTeams;
            this.MaxMatchRules = maxMatchRules;
            this.MaxTeamRules = maxTeamRules;
            this.MaxRuleRelaxations = maxRuleRelaxations;
            this.MaxPlayersPerTicket = maxPlayersPerTicket;
            this.MaxPoolTimeout = maxPoolTimeout;
        }
    }

}
#endif
