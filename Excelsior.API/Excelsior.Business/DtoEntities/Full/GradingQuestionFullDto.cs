using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.DtoEntities.Full
{
    public class GradingQuestionFullDto : GradingQuestionBaseDto
    {
        public GradingQuestionFullDto()
            : this(null)
        {
            Answers = new List<GradingAnswerFullDto>();
        }
        public GradingQuestionFullDto(GRD_GradingQuestion entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                if (!(sender is GradingQuestionTagBaseDto) && entity.GRDQuestionTag != null)
                {
                    Tag = new GradingQuestionTagFullDto(entity.GRDQuestionTag);
                }
            }
        }
        public override GRD_GradingQuestion ToEntity(GRD_GradingQuestion entity = null)
        {
            entity = base.ToEntity(entity);

            entity.GRDQuestionTag = Tag.ToEntity(entity.GRDQuestionTag);

            if (Answers.Count > 0)
            {
                entity.GRD_GradingAnswers.Clear();
                foreach (var a in Answers)
                {
                    var lde = a.ToEntity();
                    entity.GRD_GradingAnswers.Add(lde);
                }
            }

            return entity;
        }

        public void fillAnswers(List<GRD_GradingAnswer> tAnswers, List<GRD_TempQuestion> tempQuestions)
        {
            if (tAnswers != null)
            {
                var qAnswers = tAnswers.Where(x => x.GQuestionID == this.Id).OrderBy(x => x.GAnswerSeq);
                if (qAnswers != null)
                {
                    this.Answers = new List<GradingAnswerFullDto>();
                    foreach (var answer in qAnswers)
                    {
                        var answerResponse = new GradingAnswerFullDto(answer);
                        if (answer.GRD_Dependencies != null)
                        {
                            answerResponse.Dependencies = new List<GradingDependencyFullDto>();
                            foreach (var dependency in answer.GRD_Dependencies1)
                            {
                                bool isAnswerfound = false;
                                if (dependency.GTargetAnswerID != null)
                                    isAnswerfound = qAnswers.Any(x => x.GAnswersID == dependency.GTargetAnswerID);
                                bool isQuestionfound = false;
                                if (dependency.GTargetQuestionID != null)
                                    isQuestionfound = tempQuestions.Any(x => x.GQuestionID == dependency.GTargetQuestionID);

                                if (isAnswerfound || isQuestionfound)
                                {
                                    answerResponse.Dependencies.Add(new GradingDependencyFullDto(dependency));
                                }
                            }
                        }
                        this.Answers.Add(answerResponse);
                    }
                }
            }
        }

        public GradingQuestionTagFullDto Tag { get; set; }
        public List<GradingAnswerFullDto> Answers { get; set; }
    }
}
