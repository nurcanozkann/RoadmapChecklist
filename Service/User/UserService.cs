﻿using Data.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Data.Infrastructure.UnitOfWork;
using Entity.Domain.User;

namespace Service.User
{
    public class UserService : IUserService
    {
        private readonly IRepository<Entity.Domain.User.User> repository;
        private readonly IUnitOfWork unitOfWork;

        public UserService(IRepository<Entity.Domain.User.User> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        public ReturnModel<Entity.Domain.User.User> Create(Entity.Domain.User.User userEntity)
        {
            var result = new ReturnModel<Entity.Domain.User.User>();

            try
            {
                repository.Add(userEntity);
                result.Data = userEntity;
            }
            catch (Exception exception)
            {
                result.IsSuccess = false;
                result.Exception = exception;
                result.Message = exception.Message;
            }

            return result;
        }

        public ReturnModel<Entity.Domain.User.User> Update(Entity.Domain.User.User userEntity)
        {
            var result = new ReturnModel<Entity.Domain.User.User>();

            try
            {
                repository.Update(userEntity);
                result.Data = userEntity;
            }
            catch (Exception exception)
            {
                result.IsSuccess = false;
                result.Exception = exception;
                result.Message = exception.Message;
            }

            return result;
        }

        public ReturnModel<IEnumerable<Entity.Domain.User.User>> GetAll()
        {
            var result = new ReturnModel<IEnumerable<Entity.Domain.User.User>>();

            try
            {
                result.Data = repository.GetList();
            }
            catch (Exception exception)
            {
                result.IsSuccess = false;
                result.Exception = exception;
                result.Message = exception.Message;
            }

            return result;
        }

        public ReturnModel<Entity.Domain.User.User> Get(int userId)
        {
            var result = new ReturnModel<Entity.Domain.User.User>();

            try
            {
                result.Data = repository.Get(user => user.Id == userId);
            }
            catch (Exception exception)
            {
                result.IsSuccess = false;
                result.Exception = exception;
                result.Message = exception.Message;
            }

            return result;
        }

        public ReturnModel<int> Delete(Entity.Domain.User.User userEntity)
        {
            var result = new ReturnModel<int>();

            try
            {
                repository.Delete(userEntity);
                result.Data = userEntity.Id;
            }
            catch (Exception exception)
            {
                result.IsSuccess = false;
                result.Exception = exception;
                result.Message = exception.Message;
            }

            return result;
        }
    }
}
