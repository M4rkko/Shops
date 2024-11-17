using Microsoft.EntityFrameworkCore;
using ShopTARge23.Core.Domain;
using ShopTARge23.Core.Dto;
using ShopTARge23.Core.ServiceInterface;
using ShopTARge23.Data;

namespace ShopTARge23.ApplicationServices.Services
{
    public class KindergartenServices : IKindergartenServices
    {
        private readonly ShopTARge23Context _context;

        public KindergartenServices
            (
                ShopTARge23Context context
            )
        {
            _context = context;
        }

        public async Task<Kindergarten> Create(KindergartenDto dto)
        {
            Kindergarten kindergarten = new();

            kindergarten.Id = Guid.NewGuid();
            kindergarten.KindergartenName = dto.KindergartenName;
            kindergarten.CreatedAt = DateTime.Now;
            kindergarten.ModifiedAt = DateTime.Now;

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
            domain.CreatedAt = dto.CreatedAt;
            domain.ModifiedAt = DateTime.Now;

            _context.Kindergarten.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }
        public async Task<Kindergarten> Delete(Guid id)
        {
            var result = await _context.Kindergarten
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.Kindergarten.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
