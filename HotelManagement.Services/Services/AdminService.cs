using AutoMapper;
using CloudinaryDotNet.Actions;
using HotelManagement.Core;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.Enums;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using HotelManagement.Infrastructure.Context;
using HotelManagement.Infrastructure.Repositories;
using HotelManagement.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services.Services
{
    public class AdminService : IAdminService

    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AdminService(IAdminRepository adminRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<Response<string>> AddUserRole(string userId, Roles role)
        {
            var response = new Response<string>
            {
                Succeeded = false,
                StatusCode = (int)HttpStatusCode.NotFound,
                Message = "User not found"
            };
            var result = await _adminRepository.AddUserRole(userId, role);
            if(!result) return response;
            
            response.Succeeded = true;
            response.StatusCode = (int)HttpStatusCode.OK;
            response.Message = "Role added successfully";
            return response;
        }

        public async Task<Response<string>> CreateRole(RoleDTO role)
        {
            var result = await _adminRepository.CreateRole(role);
            var response = new Response<string>();
            if (result)
            {
                response.Succeeded = true;
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "Role created successfully";
            }
            else
            {
                response.Succeeded = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Role cannot be created, please try again or change role";

            }

            return response;
        }

        public async Task<Response<string>> RemoveUserRole(string userId, Roles role)
        {
            var response = new Response<string>
            {
                Succeeded = false,
                StatusCode = (int)HttpStatusCode.NotFound,
                Message = "User not found"
            };
            var result = await _adminRepository.RemoveUserRole(userId, role);
            if (!result) return response;
            response.Succeeded = true;
            response.StatusCode = (int)HttpStatusCode.OK;
            response.Message = "Role removed successfully";
            return response;
        }

        public async Task<Response<List<Admin_managerDTO>>> GetAllManagers(Roles roles)
        {
            var managers = await _unitOfWork.adminRepository.GetAllAsync();
            var allManagers = _mapper.Map<List<Admin_managerDTO>>(roles);
           
            if(allManagers.Count == 0)
            {
                return new Response<List<Admin_managerDTO>>
                {
                    StatusCode = 404,
                    Succeeded = false,
                    Data = null,
                    Message = "Managers not found"
                };
            }
            return new Response<List<Admin_managerDTO>>
            {
                StatusCode =202,
                Succeeded = true,
                Data = allManagers,
                Message = "Successful!"
            };

        }

        Task<Response<string>>IAdminService.GetAllManagers(Roles role)
        {
            throw new NotImplementedException();
        }
    }
}
