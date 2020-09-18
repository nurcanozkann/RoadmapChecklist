﻿using Data.Repository;
using Data.UnitOfWork;
using Entity.Models.Tags;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.RoadmapTags
{
    public class RoadmapTagService : IRoadmapTagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<RoadmapTag> _repository;
        public RoadmapTagService(IUnitOfWork unitOfWork,IRepository<RoadmapTag> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public ReturnModel<RoadmapTag> Add(RoadmapTag roadmapTag)
        {
            var result = new ReturnModel<RoadmapTag>();
            try
            {
                _repository.Add(roadmapTag);
                result.Data=roadmapTag;

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex;
                result.Message = ex.Message;

            }
            return result;
        }

        public ReturnModel<bool> Delete(int roadmapTagId)
        {
            var result = new ReturnModel<bool>();
            try
            {
                _repository.Delete(roadmapTagId);
                result.Data = true;

            }
            catch (Exception ex)
            {

                result.IsSuccess = false;
                result.Exception = ex;
                result.Message = ex.Message;
            }
            return result;
        }

        public ReturnModel<RoadmapTag> Get(int roadmapTagId)
        {
            var result = new ReturnModel<RoadmapTag>();
            try
            {
              result.Data= _repository.Get(roadmapTag =>roadmapTag.Id == roadmapTagId);

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex;
                result.Message = ex.Message; 
            }
            return result;
        }

        public ReturnModel<IEnumerable<RoadmapTag>> GetAllByUser(int roadmapTagId)
        {
            var result = new ReturnModel<IEnumerable<RoadmapTag>>();
            try
            {
                result.Data = _repository.GetAll(roadmapTag => roadmapTag.Id == roadmapTagId);
            }
            catch (Exception ex)
            {

                result.IsSuccess = false;
                result.Exception = ex;
                result.Message = ex.Message;
            }
            return result;
        }

        public void Save()
        {
            _unitOfWork.Commmit();
        }

        public ReturnModel<RoadmapTag> Update(RoadmapTag roadmapTag)
        {
            var result = new ReturnModel<RoadmapTag>();
            try
            {
                _repository.Update(roadmapTag);
                result.Data = roadmapTag;

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
