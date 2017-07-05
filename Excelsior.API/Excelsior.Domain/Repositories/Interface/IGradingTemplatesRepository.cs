using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface IGradingTemplatesRepository : IEntityBaseRepository<GRD_GradingTemplate>
    {
        IQueryable<GRD_GradingTemplate> GetAll(long? trialId, bool? isActive, bool? isLocked, string search);

        GRD_GradingTemplate GetGradingTemplateForProcedure(long procedureId, long timePointListId);

        IQueryable<GRD_TempQuestion> GetGradingTemplateQuestionsForTemplate(long templateId);

        IQueryable<GRD_GradingAnswer> GetGradingAnswersForTemplate(long templateId, long? trialId);

        IQueryable<GRD_Dependency> GetGradingDependenciesForTemplate(long templateId);
    }
}
