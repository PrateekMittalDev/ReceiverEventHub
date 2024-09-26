using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiverEventHub.Infrastructure.Models
{
    //by default string is not accepting null so we make string nullable in latest version of .net
    public class ContentPlayLog
    {
        public Guid Id { get; set; }
        public long? ContentPlayLogId { get; set; }
        public Guid StudentId { get; set; }
        public Guid? ContentId { get; set; }
        public string? ContentTitle { get; set; }
        public int? ContentGroupId { get; set; }
        public DateTime Start { get; set; }
        public string? StartClientTime { get; set; }
        public int Duration { get; set; }
        public short Client { get; set; }
        public Guid RegionId { get; set; }
        public int? BrowserId { get; set; }
        public int? OperatingSystemId { get; set; }
        public string? BrowserVersion { get; set; }
        public string? OSVersion { get; set; }
        public short? Mode { get; set; }
        public int Unit { get; set; }
        public Guid? PlaylistId { get; set; }
        public bool IsAggregated { get; set; }
        public Guid? SchoolId { get; set; }
        public Guid? CampusId { get; set; }
        public Guid? SchoolClassId { get; set; }
        public int? ContentIndex { get; set; }
        public int? Round { get; set; }
        /// <summary>
        /// This field is used to process elk logs that were migrated from ContentPlayLog2020.
        /// This field is specifically to aggregate StudentPlayStatistic and StudentActivityStatistic for logs generated till 17th Jul 2022, after that, aggregation will take place using IsAggregated field.
        /// </summary>
        public bool? IsNotProcessed { get; set; }
        public string? AppVersion { get; set; }
    }
}
