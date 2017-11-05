﻿using WebApi.Models;

namespace WebApi.ViewModels
{
    public class AppointmentGetViewModel
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public AppointmentStatus Status { get; set; }

        public Bench Bench { get; set; }

        public int ClientId { get; set; }

        public string HealthworkerName { get; set; }
    }
}