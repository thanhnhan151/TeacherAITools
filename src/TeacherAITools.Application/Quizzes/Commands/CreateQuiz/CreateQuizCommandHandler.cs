using AutoMapper;
using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Quizzes.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Quizzes.Commands.CreateQuiz
{
    public class CreateQuizCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<CreateQuizCommand, Response<GetQuizResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<GetQuizResponse>> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
        {
            var quiz = new Quiz
            {
                QuizName = request.CreateQuizRquest.QuizName,
                LessonId = request.CreateQuizRquest.LessonId
            };

            await _unitOfWork.Quizzes.AddAsync(quiz);
            await _unitOfWork.CompleteAsync();

            var quizId = _unitOfWork.Quizzes.GetLastIdQuiz();
            QuizQuestion newQuestion = new();
            QuizAnswer newAnswer = new();

            foreach (var question in request.CreateQuizRquest.QuizQuestions)
            {
                var newId = _unitOfWork.QuizQuestions.GetLastIdQuestion();
                newQuestion.QuestionId = newId;
                newQuestion.QuestionName = question.QuestionName;
                newQuestion.QuizId = quizId;

                await _unitOfWork.QuizQuestions.AddAsync(newQuestion);
                await _unitOfWork.CompleteAsync();

                var questionId = _unitOfWork.QuizQuestions.GetLastIdQuestion();

                foreach (var answer in question.QuizAnswers)
                {
                    var answerId = _unitOfWork.QuizAnswers.GetLastIdAnswer() + 1;
                    newAnswer.Answer = answer.Answer;
                    newAnswer.IsCorrect = answer.IsCorrect;
                    newAnswer.QuestionId = questionId;
                    newAnswer.AnswerId = answerId;

                    await _unitOfWork.QuizAnswers.AddAsync(newAnswer);
                    await _unitOfWork.CompleteAsync();
                }
            }

            #region Test
            //List<QuizQuestion> quizQuestions = [];

            //foreach (var question in request.CreateQuizRquest.QuizQuestions)
            //{
            //    List<QuizAnswer> quizAnswers = [];

            //    foreach (var answer in question.QuizAnswers)
            //    {
            //        QuizAnswer newAnswer = new();
            //        var answerId = _unitOfWork.QuizAnswers.GetLastIdAnswer() + 1;
            //        if (quizAnswers.Count == 1) answerId++;
            //        newAnswer.Answer = answer.Answer;
            //        newAnswer.IsCorrect = answer.IsCorrect;
            //        newAnswer.AnswerId = answerId;
            //        quizAnswers.Add(newAnswer);
            //    }

            //    quizQuestions.Add(new QuizQuestion
            //    {
            //        QuestionName = question.QuestionName,
            //        QuizAnswers = quizAnswers
            //    });
            //}

            //await _unitOfWork.Quizzes.AddAsync(new Quiz
            //{
            //    QuizName = request.CreateQuizRquest.QuizName,
            //    LessonId = request.CreateQuizRquest.LessonId,
            //    QuizQuestions = quizQuestions
            //});

            //await _unitOfWork.CompleteAsync();
            #endregion

            return new Response<GetQuizResponse>(code: (int)ResponseCode.CREATED_SUCCESS, message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}
