using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using RiverStationWatch.Data;
using RiverStationWatch.Data.Model;
using RiverStationWatch.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RiverStationWatch.Services
{
    public class StationService : IStationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<StationService> _logger;

        public StationService(ApplicationDbContext context, IEmailSender emailSender, ILogger<StationService> logger)
        {
            _context = context;
            _emailSender = emailSender;
            _logger = logger;
        }

        // Check if a station sent a record in time tolerance
        public void CheckStationForTimeout()
        {
            if (_context.Records.Any())
            {
                var stations = _context.Stations.ToList();
                var currentTime = DateTime.Now;

                foreach (var station in stations)
                {
                    var lastRecord = _context.Records
                        .Where(r => r.StationId == station.Id)
                        .OrderByDescending(r => r.TimeStamp)
                        .FirstOrDefault();

                    if (lastRecord == null || (currentTime - lastRecord.TimeStamp).TotalMinutes > station.TimeOutInMinutes)
                    {
                        // Station is overdue, send email notification
                        SendTimeoutEmailNotification(station);
                    }
                }
            }
        }

        private void SendTimeoutEmailNotification(Station station)
        {
            try
            {
                var recipients = new List<string>();

                if (!string.IsNullOrWhiteSpace(station.ReportEmail))
                {
                    recipients.Add(station.ReportEmail);
                }
                else // If report email property for a station is empty, send the mail to all registered users
                {
                    foreach (var user in _context.Users)
                    {
                        recipients.Add(user.Email);
                    }
                }

                if (recipients.Any())
                {
                    var subject = "Water Level Record Timeout";
                    var body = $"The station '{station.StationName}' has not sent a water level record within the specified time tolerance limit.";

                    foreach (var recipient in recipients)
                    {
                        _emailSender.SendEmailAsync(recipient, subject, body).GetAwaiter().GetResult();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending timeout email notification for station '{station.StationName}'");
            }
        }

        //===================================================================================
        // Flood level exceedance check
        public void CheckFloodLevelExceedance()
        {
            if (_context.Records.Any())
            {
                var stations = _context.Stations.ToList();

                foreach (var station in stations)
                {
                    var records = _context.Records
                        .Where(r => r.StationId == station.Id && r.Value >= station.FloodLevel)
                        .ToList();

                    if (records.Count >= 3)
                    {
                        // Send email notification
                        SendFloodLevelExceedanceEmailNotification(station);
                    }
                }
            }
        }

        // Send report about flood on a river
        private void SendFloodLevelExceedanceEmailNotification(Station station)
        {
            try
            {
                var recipients = new List<string>();

                if (!string.IsNullOrWhiteSpace(station.ReportEmail))
                {
                    recipients.Add(station.ReportEmail);
                }
                else // If report email property for a station is empty, send the mail to all registered users
                {
                    foreach (var user in _context.Users)
                    {
                        recipients.Add(user.Email);
                    }
                }

                if (recipients.Any())
                {
                    var subject = "Flood Level Exceedance Alert";
                    var body = $"The station '{station.StationName}' has recorded 3 or more instances where the water level exceeds or equals the flood level.";

                    foreach (var recipient in recipients)
                    {
                        _emailSender.SendEmailAsync(recipient, subject, body).GetAwaiter().GetResult();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending flood level exceedance email notification for station '{station.StationName}'");
            }
        }
    }
}
