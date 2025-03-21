﻿using ASP.NET_API_Template.Core.DTOs;

namespace ASP.NET_API_Template.EF.Repositories;

public class StudentRepository(AppDbContext _context) : BaseRepository<Student>(_context), IStudentRepository
{
    public async Task<GetStudentDto> GetStudentById(int id)
    {
        return await FindWithSelectionAsync(
            selector: StudentProfile.ToGetStudentDto(),
            criteria: x => x.Id == id);
    }

    public async Task<PagedResult<GetStudentDto>> GetAllStudents(
        int pageNumber = 1,
        int pageSize = 10,
        string? searchTerm = null)
    {
        Expression<Func<Student, bool>> filter = x => true;

        if (!string.IsNullOrEmpty(searchTerm))
        {
            filter = filter.AndAlso(c =>
                c.FullName.Contains(searchTerm) ||
                c.Email.Contains(searchTerm) ||
                c.PhoneNumber.Contains(searchTerm));
        }


        var pagedResult = await GetPagedDataWithSelectionAsync(
            orderBy: x => x.FullName,
            selector: StudentProfile.ToGetStudentDto(),
            criteria: filter,
            pageNumber: pageNumber,
            pageSize: pageSize);

        return pagedResult;
    }

    public async Task<BaseResponse<string>> AddStudentAsync(AddStudentDto studentDto)
    {
        var newStudent = studentDto.ToStudent();
        await AddAsync(newStudent);
        await _context.SaveChangesAsync();

        return new BaseResponse<string>
        {
            Success = true,
            Message = "تم إضافة الطالب بنجاح"
        };
    }

    public async Task<BaseResponse<string>> UpdateStudentAsync(UpdateStudentDto studentDto)
    {
        var student = await GetByIdAsync(studentDto.Id);
        if (student is null)
            return new BaseResponse<string> { Success = false, Message = "الطالب غير موجود" };


        student.UpdateStudent(studentDto);
        await _context.SaveChangesAsync();

        return new BaseResponse<string>
        {
            Success = true,
            Message = "تم تحديث الطالب بنجاح"
        };
    }


    public async Task<BaseResponse<string>> DeleteStudentAsync(int id)
    {
        var student = await GetByIdAsync(id);
        if (student is null)
        {
            return new BaseResponse<string>
            {
                Success = false,
                Message = "الطالب غير موجود"
            };
        }

        Delete(student);
        await _context.SaveChangesAsync();

        return new BaseResponse<string>
        {
            Success = true,
            Message = "تم حذف الطالب بنجاح"
        };
    }


}