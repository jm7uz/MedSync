﻿using MedSync.Data.IRepositories;
using MedSync.Data.Repositories;
using MedSync.Domain.Entities;
using MedSync.Service.DTOs;
using MedSync.Service.Exceptions;
using MedSync.Service.Interfaces;

namespace MedSync.Service.Services;

public class UserService : IUserService
{
    private long _id;
    private readonly IRepository<User> userRepository = new Repository<User>();
    public async Task GenerateIdAsync()
    {
        var users = await userRepository.SelectAllAsync();
        if (users.Count == 0)
        {
            this._id = 1;
        }
        else
        {
            var user = users[users.Count - 1];
            this._id = ++user.Id;
        }
    }
    public async Task<UserForResultDto> CreateAsync(UserForCreationDto dto)
    {
        var user = (await this.userRepository.SelectAllAsync()).FirstOrDefault(u => u.Email.ToLower() == dto.Email.ToLower());
        if (user is not null)
            throw new CustomException(400, "User is already exist");

        await GenerateIdAsync();

        var person = new User()
        {
            Id = this._id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Password = dto.Password,
            PhoneNumber = dto.PhoneNumber,
            CreatedAt = DateTime.UtcNow
        };

        var result = await userRepository.InsertAsync(person);

        var mappedUser = new UserForResultDto()
        {
            Id = result.Id,
            FirstName = result.FirstName,
            LastName = result.LastName,
            PhoneNumber = result.PhoneNumber,
        };

        return mappedUser;
    }

    public async Task<List<UserForResultDto>> GetAllAsync()
    {
        var users = await this.userRepository.SelectAllAsync();
        var result = new List<UserForResultDto>();

        foreach (var user in users)
        {
            var mappedUser = new UserForResultDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                role = user.role,
            };
            result.Add(mappedUser);
        }

        return result;
    }

    public async Task<UserForResultDto> GetByIdAsync(long id)
    {
        var user = await this.userRepository.SelectByIdAsync(id);
        if (user is null)
            throw new CustomException(404, "User is not found");

        var mappedUser = new UserForResultDto()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
        };

        return mappedUser;
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var user = await this.userRepository.SelectByIdAsync(id);
        if (user is null)
            throw new CustomException(404, "User is not found");

        return await this.userRepository.DeleteAsync(id);
    }

    public async Task<UserForResultDto> UpdateAsync(UserForUpdateDto dto)
    {
        var user = await this.userRepository.SelectByIdAsync(dto.Id);
        if (user is null)
            throw new CustomException(404, "User is not found");

        var mappedUser = new User()
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            UpdatedAt = DateTime.UtcNow
        };

        await this.userRepository.UpdateAsync(mappedUser);

        var result = new UserForResultDto()
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
        };

        return result;
    }
    public async Task<UserForLoginDto> UserValidator(string email, string password)
    {
        var user = (await this.userRepository.SelectAllAsync()).FirstOrDefault(u => (u.Email.ToLower() == email && u.Password == password));
        var result = new UserForLoginDto()
        {
            Id = user.Id,
            role = user.role,
        };

        return result;
    }
}
