using AutoMapper;
using Quiz.API.DTO;
using Quiz.Shared.Models;

namespace Quiz.API
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AnswerDTO, Answer>();
            CreateMap<QuestionDTO, Question>()
                .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers));
            CreateMap<QuizzDTO, Quizz>();
            CreateMap<ResultDTO, Result>();
            CreateMap<UserDTO, User>();

            CreateMap<Answer, AnswerDTO>();
            CreateMap<Question, QuestionDTO>()
                .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers));
            CreateMap<Quizz, QuizzDTO>();
            CreateMap<Result, ResultDTO>();
            CreateMap<User, UserDTO>();
        }
    }
}
