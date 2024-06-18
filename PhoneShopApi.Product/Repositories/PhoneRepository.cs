using Microsoft.EntityFrameworkCore;
using PhoneShopApi.Product.Data;
using PhoneShopApi.Product.Dto.Phone;
using PhoneShopApi.Product.Helper;
using PhoneShopApi.Product.Interfaces.IRepository;
using PhoneShopApi.Product.Mappers;
using PhoneShopApi.Product.Models;

namespace PhoneShopApi.Product.Repositories
{
    public class PhoneRepository(PhoneShopDbContext context) : IPhoneRepository
    {
        private readonly PhoneShopDbContext _context = context;

        public async Task<ICollection<Phone>> GetAllPhonesSellingAsync()
        {
            var phones = await _context.Phones
                    .Where(p => p.IsSelling)
                    .Include(p => p.Brand)
                    .Include(p => p.PhoneOptions)
                        .ThenInclude(po => po.BuiltInStorage)
                    .Include(p => p.PhoneOptions)
                        .ThenInclude(po => po.PhoneColor)
                    .Where(po => po.PhoneOptions.Count > 0)
                    .ToListAsync();

            return phones;
        }

        public async Task<ICollection<Phone>> GetAllAsync()
        {
            var phones = await _context.Phones
                    .Include(p => p.Brand)
                    .Include(p => p.PhoneOptions)
                        .ThenInclude(po => po.BuiltInStorage)
                    .Include(p => p.PhoneOptions)
                        .ThenInclude(po => po.PhoneColor)
                    .ToListAsync();

            return phones;
        }

        public async Task<Phone?> GetByIdAsync(int id)
        {
            var phone = await _context.Phones.FindAsync(id);

            return phone;
        }

        public async Task<Phone?> UpdateAsync(int id, UpdatePhoneRequestDto updatePhoneRequestDto)
        {
            var phoneToUpdate = await _context.Phones.FindAsync(id);

            if (phoneToUpdate is null)
            {
                return null;
            }

            phoneToUpdate.Name = updatePhoneRequestDto.Name;
            phoneToUpdate.BrandId = updatePhoneRequestDto.BrandId;
            phoneToUpdate.IsSelling = updatePhoneRequestDto.IsSelling;

            await _context.SaveChangesAsync();

            return phoneToUpdate;
        }

        public async Task<Phone> CreateAsync(Phone newPhone)
        {
            newPhone.PhoneDetail = new PhoneDetail();
            await _context.Phones.AddAsync(newPhone);
            await _context.SaveChangesAsync();

            return newPhone;
        }

        public async Task<Phone?> DeleteByIdAsync(int id)
        {
            var phoneToDelete = await _context.Phones.FindAsync(id);

            if (phoneToDelete is null)
            {
                return null;
            }

            _context.Remove(phoneToDelete);
            await _context.SaveChangesAsync();

            return phoneToDelete;
        }

        public async Task<ICollection<PhoneItemDto>> GetAllPhonesSellingFollowBrandAsync(QueryPhone query)
        {
            var listPhones = new List<PhoneItemDto>();

            var brands = await _context.Brands.ToListAsync();

            foreach (var brand in brands)
            {
                var newPhoneItem = new PhoneItemDto
                {
                    BrandId = brand.Id,
                    BrandName = brand.Name
                };

                var phonesQuery = _context.Phones
                    .Where(p => p.BrandId == brand.Id && p.IsSelling)
                    .Include(p => p.PhoneOptions)
                    .ThenInclude(po => po.PhoneColor)
                    .Include(po => po.PhoneOptions)
                    .ThenInclude(po => po.BuiltInStorage)
                    .Where(p => p.PhoneOptions.Count > 0)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(query.Name))
                {
                    phonesQuery = phonesQuery.Where(p => p.Name.Contains(query.Name));
                }

                var phones = await phonesQuery.ToListAsync();

                newPhoneItem.Phones = new List<Item>();

                foreach (var phone in phones)
                {
                    var phoneOption = phone.PhoneOptions.FirstOrDefault(po => po.Price > 0 && po.Quantity > 0);
                    if (phoneOption == null || phoneOption.Price <= 0 || phoneOption.Quantity <= 0) continue;

                    var item = new Item
                    {
                        IsSelling = phone.IsSelling,
                        PhoneId = phone.Id,
                        PhoneName = phone.Name,
                        BuiltInStorageCapacity = phoneOption.BuiltInStorage.Capacity,
                        BuiltInStorageUnit = phoneOption.BuiltInStorage.Unit,
                        PhoneColorName = phoneOption.PhoneColor.Name,
                        PhoneColorUrl = phoneOption.PhoneColor.ImageUrl,
                        Price = phoneOption.Price,
                        Quantity = phoneOption.Quantity
                    };

                    newPhoneItem.Phones.Add(item);

                }

                if (newPhoneItem.Phones.Count > 0)
                {
                    listPhones.Add(newPhoneItem);
                }
            }


            return listPhones;
        }

        public async Task<ICollection<PhoneItemDto>> AdminGetAllPhonesSellingFollowBrandAsync(QueryPhone query)
        {
            var listPhones = new List<PhoneItemDto>();

            var brands = await _context.Brands.ToListAsync();

            foreach (var brand in brands)
            {
                var newPhoneItem = new PhoneItemDto
                {
                    BrandId = brand.Id,
                    BrandName = brand.Name
                };

                var phonesQuery = _context.Phones
                    .Where(p => p.BrandId == brand.Id)
                    .Include(p => p.PhoneOptions)
                    .ThenInclude(po => po.PhoneColor)
                    .Include(po => po.PhoneOptions)
                    .ThenInclude(po => po.BuiltInStorage)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(query.Name))
                {
                    phonesQuery = phonesQuery.Where(p => p.Name.Contains(query.Name));
                }

                var phones = await phonesQuery.ToListAsync();

                newPhoneItem.Phones = new List<Item>();

                foreach (var phone in phones)
                {
                    var phoneOption = phone.PhoneOptions.FirstOrDefault();
                    var item = new Item
                    {
                        IsSelling = phone.IsSelling,
                        PhoneId = phone.Id,
                        PhoneName = phone.Name,
                        BuiltInStorageCapacity = phoneOption != null ? phoneOption.BuiltInStorage.Capacity : -1,
                        BuiltInStorageUnit = phoneOption != null ? phoneOption.BuiltInStorage.Unit : "N/A",
                        PhoneColorName = phoneOption != null ? phoneOption.PhoneColor.Name : "N/A",
                        PhoneColorUrl = phoneOption != null ? phoneOption.PhoneColor.ImageUrl : "http://14.225.207.131:19001/Uploads/NotFound.png",
                        Price = phoneOption != null ? phoneOption.Price : -1,
                        Quantity = phoneOption != null ? phoneOption.Quantity : -1
                    };

                    newPhoneItem.Phones.Add(item);

                }
                listPhones.Add(newPhoneItem);
            }
            return listPhones;
        }
    }
}