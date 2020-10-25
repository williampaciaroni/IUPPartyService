using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace IUPPartyService.Models
{
    public class Participant : LazyEntity
    {

        public Participant()
        { }

        public Participant(ILazyLoader lazyLoader) : base(lazyLoader)
        { }

        public Participant(string kennitala, string name)
        {
            this.ParticipantKennitala = kennitala;
            this.ParticipantName = name;
        }

        [Key]
        public string ParticipantKennitala { get; set; }
        [Required]
        public string ParticipantName { get; set; }
        [Required]
        public string EventRef { get; set; }

        private Event _event;
        public virtual Event Event
        {
            get => lazyLoader.Load(this, ref _event);
            set => _event = value;
        }


    }
}
