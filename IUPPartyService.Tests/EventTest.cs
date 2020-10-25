using System;
using System.Collections.Generic;
using IUPPartyService.Models;
using Moq;
using Xunit;

namespace IUPPartyService.Tests
{
    public class EventTest
    {
        private Mock<Event> _Event;

        public EventTest()
        {
            _Event = new Mock<Event>("Ev","Fantastic Event",3, Convert.ToDateTime("01/12/2020"), Convert.ToDateTime("02/12/2020"), "3005973949", "William", 1.5154, 1.54332, Convert.FromBase64String(""));
        }

        [Fact]
        public ICollection<Participant> GetParticipants()
        {
            return _Event.Object.Participant;
        }

    }
}
