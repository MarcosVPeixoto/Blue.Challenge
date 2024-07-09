using AutoMapper;
using Blue.Challenge.Business.Responses.Queries;
using Blue.Challenge.Domain.Entities;

namespace Blue.Challenge.Business.Mapper
{
    public class ChallengeMapper : Profile
    {
        public ChallengeMapper()
        {
            CreateMap<Contact, GetContactQueryResponse>();
        }
    }
}
