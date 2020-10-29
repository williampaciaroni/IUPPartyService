using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace IUPPartyService.Models
{
    public class Event : LazyEntity
    {
        [Key]
        public string EventID{ get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int MaxPeople { get; set; }
        [Required]
        public DateTime DateStart { get; set; }
        [Required]
        public DateTime DateEnd { get; set; }
        [Required]
        public string Host { get; set; }
        [Required]
        public string HostName { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public byte[] ImageData { get; set; }
        [Required]
        public bool Hidden { get; set; }
        [Required]
        public bool RequirePassword { get; set; }
        public string? Password { get; set; }

        public Event(ILazyLoader lazyLoader) : base(lazyLoader)
        { }

        private ICollection<Participant> _participant;

        public virtual ICollection<Participant> Participant
        {
            get => lazyLoader.Load(this, ref _participant);
            set => _participant = value;
        }

        public Event(string name, string description, int maxPeople, DateTime dateStart, DateTime dateEnd, string host, string hostName, double latitude, double longitude, byte[] image, bool hidden, bool requirePassword, string? password)
        {
            this.Name = name;
            this.Description = description;
            this.MaxPeople = maxPeople;
            this.DateStart = dateStart;
            this.DateEnd = dateEnd;
            this.Host = host;
            this.HostName = hostName;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.ImageData = image;
            this.Hidden = hidden;
            this.RequirePassword = requirePassword;
            this.Password = password;
        }
    }

    public class FilteredEvent
    {
        public string EventID { get; set; }
        public string Name { get; set; }
        public string HostName { get; set; }
        public double Distance { get; set; }
        public int Participants { get; set; }
        public int MaxPeople { get; set; }
        public byte[] Image { get; set; }
        public bool Hidden { get; set; }
    }

    public class FilteredEventAll
    {
        public string EventID { get; set; }
        public string Name { get; set; }
        public string HostName { get; set; }
        public byte[] Image { get; set; }
        public int MaxPeople { get; set; }
    }

    public class MyEvent
    {
        public string EventID { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public int Participants { get; set; }
        public int MaxPeople { get; set; }
    }
}
