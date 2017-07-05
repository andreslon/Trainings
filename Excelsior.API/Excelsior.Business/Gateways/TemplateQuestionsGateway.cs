using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Helpers;
using Excelsior.Domain;
using Excelsior.Domain.Helpers;
using Excelsior.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.Gateways
{
    public class TemplateQuestionsGateway : ITemplateQuestionsGateway
    {
        public ITemplateQuestionsRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public TemplateQuestionsGateway(ITemplateQuestionsRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        } 

        public ResultInfo<IList<TemplateQuestionBaseDto>> GetAll(TemplateQuestionsRequestDto request)
        {
            //Perform input validation
            //----

            //Get the result
            var result = new ResultInfo<IList<TemplateQuestionBaseDto>>();
            try
            {
                var response = new List<TemplateQuestionBaseDto>();
                var entities = Repository.GetAll(request.TemplateId, request.IsActive, request.Search);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return entities.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var entitiesPaged = GeneralHelper.GetPagedList(entities.OrderBy(x => x.CRFTemplateGroup.GroupSeq).ThenBy(x => x.QuestionSeq), result.Pager);
                if (entitiesPaged != null)
                {
                    foreach (var entity in entitiesPaged)
                    {
                        var dto = new TemplateQuestionBaseDto(entity);
                        response.Add(dto);
                    }
                }

                result.Result = response;
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

        public ResultInfo<TemplateQuestionFullDto> GetSingle(long id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<TemplateQuestionFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFTemplateQuestionID == id);
                if (entity != null)
                {
                    var dto = new TemplateQuestionFullDto(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Question not found");
                }
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

        public ResultInfo<TemplateQuestionFullDto> Add(TemplateQuestionFullDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<TemplateQuestionFullDto>();
            try
            {
                var entity = request.ToEntity();
                Repository.Add(entity);
                if (request.Answers != null)
                    AttachAnswers(entity, request.Answers);
                if (request.Tags != null)
                    AttachTags(entity, request.Tags);
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new TemplateQuestionFullDto(entity);
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

        public ResultInfo<TemplateQuestionFullDto> Update(TemplateQuestionFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<TemplateQuestionFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFTemplateQuestionID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity,fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new TemplateQuestionFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Question not found");
                }
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

        public ResultInfo<bool> Delete(long id)
        {
            //Perform input validation
            //---- 
            var result = new ResultInfo<bool>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFTemplateQuestionID == id);
                if (entity != null)
                {
                    Repository.Delete(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    result.Result = true;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Question not found");
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

        public ResultInfo<IList<TemplateAnswerFullDto>> GetAnswers(long id)
        {
            var result = new ResultInfo<IList<TemplateAnswerFullDto>>();
            try
            {
                var entities = Repository.GetAnswers(id);
                result.Result = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return entities.Select(x => new TemplateAnswerFullDto(x, new TemplateQuestionBaseDto())).ToList();
                });
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }

            return result;
        }

        public ResultInfo<TemplateQuestionFullDto> SetAnswers(long id, IList<TemplateAnswerFullDto> answers)
        {
            var result = new ResultInfo<TemplateQuestionFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFTemplateQuestionID == id);
                if (entity != null)
                {
                    AttachAnswers(entity, answers);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new TemplateQuestionFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Question not found");
                }
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }

            return result;
        }

        private void AttachAnswers(CRF_TemplateQuestion entity, IList<TemplateAnswerFullDto> answers)
        {
            var answerEntities = RefreshOrRemoveExistingAnswers(entity, answers);
            for (var i = 0; i < answers.Count; i++)
            {
                var answerEntity = Repository.AddAnswer(entity, answerEntities[i]);
            }
        }

        private IList<CRF_TemplateAnswer> RefreshOrRemoveExistingAnswers(CRF_TemplateQuestion entity, IList<TemplateAnswerFullDto> answers)
        {
            var entityAnswers = entity.CRF_TemplateAnswers.ToList();
            foreach (var answer in entityAnswers)
            {
                if (!answers.Any(x => x.Id == answer.CRFTemplateAnswerID))
                    Repository.Context.Delete(answer);
            }

            var answerEntities = new List<CRF_TemplateAnswer>();
            foreach (var answer in answers)
            {
                var entityAnswer = entityAnswers.FirstOrDefault(x => x.CRFTemplateAnswerID == answer.Id);
                if (entityAnswer != null)
                {
                    answerEntities.Add(answer.ToEntity(entityAnswer));
                }
                else
                {
                    answerEntities.Add(answer.ToEntity());
                }
            }

            return answerEntities;
        }

        public ResultInfo<IList<TemplateQuestionTagFullDto>> GetTags(long id)
        {
            var result = new ResultInfo<IList<TemplateQuestionTagFullDto>>();
            try
            {
                var entities = Repository.GetTags(id);
                result.Result = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return entities.Select(x => new TemplateQuestionTagFullDto(x, new TemplateQuestionBaseDto())).ToList();
                });
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }

            return result;
        }

        public ResultInfo<TemplateQuestionFullDto> SetTags(long id, IList<TemplateQuestionTagFullDto> tags)
        {
            var result = new ResultInfo<TemplateQuestionFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFTemplateQuestionID == id);
                if (entity != null)
                {
                    //check existing steps
                    AttachTags(entity, tags);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new TemplateQuestionFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Question not found");
                }
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }

            return result;
        }

        private void AttachTags(CRF_TemplateQuestion entity, IList<TemplateQuestionTagFullDto> tags)
        {
            var tagEntities = RefreshOrRemoveExistingTags(entity, tags);
            for (var i = 0; i < tags.Count; i++)
            {
                var tagEntity = Repository.AddTag(entity, tagEntities[i]);
            }
        }

        private IList<CRF_TemplateQuestionTag> RefreshOrRemoveExistingTags(CRF_TemplateQuestion entity, IList<TemplateQuestionTagFullDto> tags)
        {
            var entityTags = entity.CRF_TemplateQuestionTags.ToList();
            foreach (var tag in entityTags)
            {
                if (!tags.Any(x => x.Id == tag.CRFTemplateQuestionTagID))
                    entity.CRF_TemplateQuestionTags.Remove(tag);
            }

            var tagEntities = new List<CRF_TemplateQuestionTag>();
            foreach (var tag in tags)
            {
                var entityTag = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return Repository.Context.CRF_TemplateQuestionTags.FirstOrDefault(x => x.CRFTemplateQuestionTagID == tag.Id);
                });
                if (entityTag != null)
                {
                    tagEntities.Add(tag.ToEntity(entityTag));
                }
                else
                {
                    tagEntities.Add(tag.ToEntity());
                }
            }

            return tagEntities;
        }

        public ResultInfo<TemplateQuestionFullDto> SetValidation(long id, AnswerValidationFullDto validation)
        {
            var result = new ResultInfo<TemplateQuestionFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFTemplateQuestionID == id);
                if (entity != null)
                {
                    CRF_AnswerValidation vEntity = null;
                    if(validation != null)
                        vEntity = validation.ToEntity();
                    vEntity = Repository.SetValidation(entity, vEntity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new TemplateQuestionFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Question not found");
                }
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }

            return result;
        }

        public ResultInfo<TemplateQuestionFullDto> Clone(long id, CommonRequestDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<TemplateQuestionFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFTemplateQuestionID == id);
                if (entity != null)
                {
                    var cloneEntity = Repository.Clone(entity, request.Id);
                    Repository.Commit();
                    Repository.Refresh(cloneEntity);
                    var dto = new TemplateQuestionFullDto(cloneEntity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Template Question not found");
                }
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }
    }
}
