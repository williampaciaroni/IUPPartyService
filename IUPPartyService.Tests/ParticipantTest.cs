using System;
using IUPPartyService.Models;
using Moq;
using Xunit;

namespace IUPPartyService.Tests
{
    public class ParticipantTest
    {
        private Mock<Participant> _Participant;

        public ParticipantTest()
        {
            _Participant = new Mock<Participant>("3005973949","William");
        }

        [Fact]
        public string GetKennitala()
        {
            return _Participant.Object.ParticipantKennitala;
        }

        [Fact]
        public string GetName()
        {
            return _Participant.Object.ParticipantName;
        }
    }
}
