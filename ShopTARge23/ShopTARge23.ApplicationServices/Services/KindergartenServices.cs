﻿using Microsoft.EntityFrameworkCore;
using ShopTARge23.Core.Domain;
using ShopTARge23.Core.Dto;
using ShopTARge23.Core.ServiceInterface;
using ShopTARge23.Data;

namespace ShopTARge23.ApplicationServices.Services
{
    public class KindergartenServices : IKindergartenServices
    {
        private readonly ShopTARge23Context _context;
        private readonly IFileServices _fileServices;

        public KindergartenServices
            (
                ShopTARge23Context context,
                IFileServices fileServices
            )
        {
            _context = context;
            _fileServices = fileServices;
        }

        public async Task<Kindergarten> Create(KindergartenDto dto)
        {
            Kindergarten kindergarten = new();

            kindergarten.Id = Guid.NewGuid();
            kindergarten.KindergartenName = dto.KindergartenName;
            kindergarten.GroupName = dto.GroupName;
            kindergarten.ChildrenCount = dto.ChildrenCount;
            kindergarten.Teacher = dto.Teacher;
            kindergarten.CreatedAt = DateTime.Now;
            kindergarten.ModifiedAt = DateTime.Now;

            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, kindergarten);
            }

            await _context.Kindergarten.AddAsync(kindergarten);
            await _context.SaveChangesAsync();

            return kindergarten;
        }

        public async Task<Kindergarten> GetAsync(Guid id)
        {
            var result = await _context.Kindergarten
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<Kindergarten> Update(KindergartenDto dto)
        {
            Kindergarten domain = new();

            domain.Id = dto.Id;
            domain.KindergartenName = dto.KindergartenName;
            domain.GroupName = dto.GroupName;
            domain.ChildrenCount = dto.ChildrenCount;
            domain.Teacher = dto.Teacher;
            domain.CreatedAt = dto.CreatedAt;
            domain.ModifiedAt = DateTime.Now;

            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, domain);
            }

            _context.Kindergarten.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }
        public async Task<Kindergarten> Delete(Guid id)
        {
            var result = await _context.Kindergarten
                .FirstOrDefaultAsync(x => x.Id == id);

            var images = await _context.FileToDatabases
                 .Where(x => x.KindergartenId == id)
                 .Select(y => new FileToDatabaseDto
                 {
                       Id = y.Id,
                       ImageTitle = y.ImageTitle,
                       KindergartenId = y.KindergartenId
                 }).ToArrayAsync();

            await _fileServices.RemoveImagesFromDatabase(images);
            _context.Kindergarten.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
