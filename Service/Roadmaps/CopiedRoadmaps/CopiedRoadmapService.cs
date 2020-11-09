﻿using Data.Repository;
using Data.UnitOfWork;
using Entity.Models.Roadmaps;
using Service.Roadmaps.CopiedRoadmaps.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace Service.Roadmaps.CopiedRoadmaps
{
    public class CopiedRoadmapService : ICopiedRoadmapService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<CopiedRoadmap> _repository;
        private readonly IRepository<Roadmap> _roadmapRepository;
        private readonly IRepository<RoadmapItem> _roadmapItemRepository;
        public CopiedRoadmapService(IUnitOfWork unitOfWork, IRepository<CopiedRoadmap> repository,  IRepository<Roadmap> roadmapRepository, IRepository<RoadmapItem> roadmapItemRepository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _roadmapRepository = roadmapRepository;
            _roadmapItemRepository = roadmapItemRepository;
        }

        public void Save()
        {
            _unitOfWork.Commmit();
        }

        public ReturnModel<CopiedRoadmap> Add(CopiedRoadmap copiedRoadmap)
        {
            var result = new ReturnModel<CopiedRoadmap>();
            try
            {
                _repository.Add(copiedRoadmap);
                result.Data = copiedRoadmap;
            }
            catch (Exception ex)
            {

                result.IsSuccess = false;
                result.Exception = ex;
                result.Message = ex.Message;
            }
            return result;
        }

        public ReturnModel<bool> Delete(int copiedRoadmapId)
        {
            var result = new ReturnModel<bool>();
            try
            {
                _repository.Delete(copiedRoadmapId);
                result.Data = true;
            }
            catch (Exception ex)
            {
                result.Data = false;
                result.IsSuccess = false;
                result.Exception = ex;
                result.Message = ex.Message;
            }
            return result;
        }

        public ReturnModel<CopiedRoadmap> Get(int copiedRoadmapId)
        {
            var result = new ReturnModel<CopiedRoadmap>();
            try
            {
                result.Data = _repository.Get(copiedRoadmap => copiedRoadmap.Id == copiedRoadmapId);

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex;
                result.Message = ex.Message;
            }
            return result;
        }

        public ReturnModel<IEnumerable<CopiedRoadmap>> GetAllByUser(int copiedRoadmapId)
        {
            var result = new ReturnModel<IEnumerable<CopiedRoadmap>>();
            try
            {
                result.Data = _repository.GetAll(copiedRoadmap => copiedRoadmap.Id == copiedRoadmapId);


            }
            catch (Exception ex)
            {

                result.IsSuccess = false;
                result.Exception = ex;
                result.Message = ex.Message;
            }
            return result;
        }

        public ReturnModel<CopiedRoadmap> Update(CopiedRoadmap copiedRoadmap)
        {
            var result = new ReturnModel<CopiedRoadmap>();
            try
            {
                _repository.Update(copiedRoadmap);
                result.Data = copiedRoadmap;

            }
            catch (Exception ex)
            {

                result.IsSuccess = false;
                result.Exception = ex;
                result.Message = ex.Message;
            }
            return result;
        }

        public ReturnModel<CopiedRoadmap> AddCopy(CopiedRoadmapViewModel copiedRoadmapViewModel)
        {
            var result = new ReturnModel<CopiedRoadmap>();
            try
            {

                // Todo : Use AutoMapper
                var copiedRoadmap = new CopiedRoadmap()
                {
                    SourceId = copiedRoadmapViewModel.SourceRoadmapId,
                    TargetId = copiedRoadmapViewModel.TargetRoadmapId,
                };

                Add(copiedRoadmap);
                Save();
                result.Data = copiedRoadmap;
            }
            catch (Exception ex)
            {

                result.IsSuccess = false;
                result.Exception = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public ReturnModel<CopiedRoadmap> UpdateCopy(CopiedRoadmapViewModel copiedRoadmapViewModel)
        {
            var result = new ReturnModel<CopiedRoadmap>();
            try
            {
                // Todo : Use AutoMapper
                var copiedRoadmap = new CopiedRoadmap()
                {
                    SourceId = copiedRoadmapViewModel.SourceRoadmapId,
                    TargetId = copiedRoadmapViewModel.TargetRoadmapId,
                };

                Add(copiedRoadmap);
                Save();
                result.Data = copiedRoadmap;
            }
            catch (Exception ex)
            {

                result.IsSuccess = false;
                result.Exception = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public ReturnModel<Roadmap> Create(int userId, int roadmapId)
        {
            var result = new ReturnModel<Roadmap>();
            try
            {
                if (userId > 0 && roadmapId > 0)
                {
                    var sourceRoadmap = _roadmapRepository.Get(roadmap => roadmap.Id == roadmapId);
                    var targetRoadmap = new Roadmap
                    {
                        Name = sourceRoadmap.Name,
                        Visibility = sourceRoadmap.Visibility,
                        Status = sourceRoadmap.Status,
                        StartDate = sourceRoadmap.StartDate,
                        EndDate = sourceRoadmap.EndDate,
                        UserId = userId
                    };
                    var sourceRoadmapItems = _roadmapItemRepository.GetAll(roadmapItem => roadmapItem.RoadmapId == sourceRoadmap.Id); //GetAll kontrol?
                    foreach (var sourceRoadmapItem in sourceRoadmapItems)
                    {
                        var item = new RoadmapItem()
                        {
                            Title = sourceRoadmapItem.Title,
                            Description = sourceRoadmapItem.Description,
                            Status = sourceRoadmapItem.Status,
                            TargetEndDate = sourceRoadmapItem.TargetEndDate,
                            EndDate = sourceRoadmapItem.EndDate,
                            ParentId = sourceRoadmapItem.ParentId
                        };
                        targetRoadmap.RoadmapItems.Add(item);
                    }
                    var sourceCopiedRoadmapEntity = new CopiedRoadmap
                    {
                        SourceId = sourceRoadmap.Id,
                        TargetRoadmap = targetRoadmap
                    };
                    targetRoadmap.Targets.Add(sourceCopiedRoadmapEntity);
                    _roadmapRepository.Add(targetRoadmap);
                    result.Data = targetRoadmap;
                }
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
