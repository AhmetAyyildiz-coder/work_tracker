using System;
using System.Collections.Generic;
using System.Linq;
using work_tracker.Data.Entities;

namespace work_tracker.Helpers
{
    /// <summary>
    /// Represents a continuous interval where the work item stayed in development.
    /// </summary>
    public class DevelopmentInterval
    {
        public DevelopmentInterval(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime Start { get; }
        public DateTime End { get; }
        public TimeSpan Duration => End - Start;
    }

    /// <summary>
    /// Provides helper methods to calculate development durations from status change activities.
    /// </summary>
    public static class DevelopmentTimeHelper
    {
        private static readonly string[] DevelopmentStatuses = { "Gelistirmede", "MudahaleEdiliyor" };

        public static List<DevelopmentInterval> CalculateIntervals(
            WorkItem workItem,
            IEnumerable<WorkItemActivity> statusActivities)
        {
            if (workItem == null) throw new ArgumentNullException(nameof(workItem));

            var intervals = new List<DevelopmentInterval>();
            var activities = statusActivities?
                .OrderBy(a => a.CreatedAt)
                .ToList() ?? new List<WorkItemActivity>();

            DateTime? devStartTime = null;
            DateTime? lastExitTime = null;

            if (IsInDevelopment(workItem.Status) && workItem.StartedAt.HasValue)
            {
                devStartTime = workItem.StartedAt.Value;
            }

            foreach (var activity in activities)
            {
                var enteredDevelopment = IsInDevelopment(activity.NewValue) && !IsInDevelopment(activity.OldValue);
                var exitedDevelopment = IsInDevelopment(activity.OldValue) && !IsInDevelopment(activity.NewValue);

                if (enteredDevelopment && devStartTime == null)
                {
                    devStartTime = activity.CreatedAt;
                }
                else if (exitedDevelopment && devStartTime != null)
                {
                    if (lastExitTime.HasValue && activity.CreatedAt <= lastExitTime.Value)
                    {
                        continue;
                    }

                    if (activity.CreatedAt > devStartTime.Value)
                    {
                        intervals.Add(new DevelopmentInterval(devStartTime.Value, activity.CreatedAt));
                        lastExitTime = activity.CreatedAt;
                    }

                    devStartTime = null;
                }
            }

            if (devStartTime.HasValue && IsInDevelopment(workItem.Status))
            {
                var currentTime = DateTime.Now;
                if (!lastExitTime.HasValue || currentTime > lastExitTime.Value)
                {
                    intervals.Add(new DevelopmentInterval(devStartTime.Value, currentTime));
                }
            }

            if (intervals.Count == 0 && workItem.StartedAt.HasValue)
            {
                var fallbackEnd = workItem.CompletedAt ?? DateTime.Now;
                if (fallbackEnd > workItem.StartedAt.Value)
                {
                    intervals.Add(new DevelopmentInterval(workItem.StartedAt.Value, fallbackEnd));
                }
            }

            return MergeOverlaps(intervals);
        }

        public static TimeSpan CalculateTotalDuration(
            WorkItem workItem,
            IEnumerable<WorkItemActivity> statusActivities)
        {
            var intervals = CalculateIntervals(workItem, statusActivities);
            var totalMinutes = intervals.Sum(interval => interval.Duration.TotalMinutes);
            return TimeSpan.FromMinutes(totalMinutes);
        }

        public static Dictionary<DateTime, TimeSpan> CalculateDailyBreakdown(
            WorkItem workItem,
            IEnumerable<WorkItemActivity> statusActivities)
        {
            var result = new Dictionary<DateTime, TimeSpan>();
            var intervals = CalculateIntervals(workItem, statusActivities);

            foreach (var interval in intervals)
            {
                var cursor = interval.Start.Date;
                while (cursor <= interval.End.Date)
                {
                    var dayStart = cursor;
                    var dayEnd = cursor.AddDays(1);

                    var effectiveStart = interval.Start > dayStart ? interval.Start : dayStart;
                    var effectiveEnd = interval.End < dayEnd ? interval.End : dayEnd;

                    if (effectiveStart < effectiveEnd)
                    {
                        if (!result.ContainsKey(cursor))
                        {
                            result[cursor] = TimeSpan.Zero;
                        }

                        result[cursor] += effectiveEnd - effectiveStart;
                    }

                    cursor = cursor.AddDays(1);
                }
            }

            return result;
        }

        private static bool IsInDevelopment(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return false;

            return DevelopmentStatuses.Any(s =>
                status.Equals(s, StringComparison.OrdinalIgnoreCase));
        }

        private static List<DevelopmentInterval> MergeOverlaps(List<DevelopmentInterval> intervals)
        {
            var ordered = intervals
                .Where(i => i.End > i.Start)
                .OrderBy(i => i.Start)
                .ToList();

            if (ordered.Count <= 1)
            {
                return ordered;
            }

            var merged = new List<DevelopmentInterval>();
            var current = ordered[0];

            for (int i = 1; i < ordered.Count; i++)
            {
                var next = ordered[i];

                if (next.Start <= current.End)
                {
                    var mergedEnd = next.End > current.End ? next.End : current.End;
                    current = new DevelopmentInterval(current.Start, mergedEnd);
                }
                else
                {
                    merged.Add(current);
                    current = next;
                }
            }

            merged.Add(current);
            return merged;
        }
    }
}

