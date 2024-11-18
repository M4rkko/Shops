using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTARge23.Core.Dto;
using ShopTARge23.Core.ServiceInterface;
using ShopTARge23.Data;
using ShopTARge23.Models.Kindergarten;

namespace ShopTARge23.Controllers
{
    public class KindergartenController : Controller
    {
        private readonly ShopTARge23Context _context;
        private readonly IKindergartensServices _KindergartensServices;
        private readonly IFileServices _fileServices;

        public KindergartenController
            (
                ShopTARge23Context context,
                IKindergartensServices KindergartensServices,
                IFileServices fileServices
            )
        {
            _context = context;
            _KindergartensServices = KindergartensServices;
            _fileServices = fileServices;
        }

    public IActionResult Index()
    {
        var result = _context.Kindergartens
            .Select(x => new KindergartenIndexViewModel
            {
                Id = x.Id,
                KindergartenName = x.KindergartenName,
                GroupName = x.GroupName,
                ChildrenCount = x.ChildrenCount,
                Teacher = x.Teacher,
            });

        return View(result);
    }

    [HttpGet]
    public IActionResult Create()
    {
        KindergartenCreateUpdateViewModel kindergarten = new();

        return View("CreateUpdate", kindergarten);
    }

    [HttpPost]
    public async Task<IActionResult> Create(KindergartenCreateUpdateViewModel vm)
    {
        var dto = new KindergartenDto()
        {
            Id = vm.Id,
            KindergartenName = vm.KindergartenName,
            GroupName = vm.GroupName,
            ChildrenCount = vm.ChildrenCount,
            Teacher = vm.Teacher,
            CreatedAt = vm.CreatedAt,
            UpdatedAt = vm.UpdatedAt,
            Files = vm.Files,
            Image = vm.Image
                .Select(x => new FileToDatabaseDto
                {
                    Id = x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    KindergartenId = x.KindergartenId
                }).ToArray()
        };

        var result = await _KindergartensServices.Create(dto);

        if (result == null)
        {
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction(nameof(Index), vm);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        //tuua siia piltide  vaatamise funktsionaalsus

        var kindergarten = await _KindergartensServices.GetAsync(id);

        if (kindergarten == null)
        {
            return NotFound();
        }

        var photos = await _context.FileToDatabases
            .Where(x => x.KindergartenId == id)
            .Select(y => new KindergartenImageViewModel
            {
                KindergartenId = y.Id,
                ImageId = y.Id,
                ImageData = y.ImageData,
                ImageTitle = y.ImageTitle,
                Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
            }).ToArrayAsync();

        var vm = new KindergartenDetailsViewModel();

            vm.Id = kindergarten.Id;
            vm.KindergartenName = kindergarten.KindergartenName;
            vm.GroupName = kindergarten.GroupName;
            vm.ChildrenCount = kindergarten.ChildrenCount;
            vm.Teacher = kindergarten.Teacher;
            vm.CreatedAt = kindergarten.CreatedAt;
            vm.UpdatedAt = kindergarten.UpdatedAt;
        vm.Image.AddRange(photos);

        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var kindergarten = await _KindergartensServices.GetAsync(id);

        if (kindergarten == null)
        {
            return NotFound();
        }

        var photos = await _context.FileToDatabases
            .Where(x => x.KindergartenId == id)
            .Select(y => new KindergartenImageViewModel
            {
                KindergartenId = y.Id,
                ImageId = y.Id,
                ImageData = y.ImageData,
                ImageTitle = y.ImageTitle,
                Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
            }).ToArrayAsync();

        var vm = new KindergartenCreateUpdateViewModel();

            vm.Id = kindergarten.Id;
            vm.KindergartenName = kindergarten.KindergartenName;
            vm.GroupName = kindergarten.GroupName;
            vm.ChildrenCount = kindergarten.ChildrenCount;
            vm.Teacher = kindergarten.Teacher;
            vm.CreatedAt = kindergarten.CreatedAt;
            vm.UpdatedAt = kindergarten.UpdatedAt;
            vm.Image.AddRange(photos);

            return View("CreateUpdate", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Update(KindergartenCreateUpdateViewModel vm)
    {
        var dto = new KindergartenDto()
        {
            Id = vm.Id,
            KindergartenName = vm.KindergartenName,
            GroupName = vm.GroupName,
            ChildrenCount = vm.ChildrenCount,
            Teacher = vm.Teacher,
            CreatedAt = vm.CreatedAt,
            UpdatedAt = vm.UpdatedAt,
            Files = vm.Files,
            Image = vm.Image
                .Select(x => new FileToDatabaseDto
                {
                    Id = x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    KindergartenId = x.KindergartenId,
                }).ToArray()
        };

        var result = await _KindergartensServices.Update(dto);

        if (result == null)
        {
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction(nameof(Index), vm);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var kindergarten = await _KindergartensServices.GetAsync(id);

        if (kindergarten == null)
        {
            return NotFound();
        }

        var photos = await _context.FileToDatabases
            .Where(x => x.KindergartenId == id)
            .Select(y => new KindergartenImageViewModel
            {
                KindergartenId = y.Id,
                ImageId = y.Id,
                ImageData = y.ImageData,
                ImageTitle = y.ImageTitle,
                Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
            }).ToArrayAsync();

        var vm = new KindergartenDeleteViewModel();

            vm.Id = kindergarten.Id;
            vm.KindergartenName = kindergarten.KindergartenName;
            vm.GroupName = kindergarten.GroupName;
            vm.ChildrenCount = kindergarten.ChildrenCount;
            vm.Teacher = kindergarten.Teacher;
            vm.CreatedAt = kindergarten.CreatedAt;
            vm.UpdatedAt = kindergarten.UpdatedAt;
            vm.Image.AddRange(photos);

            return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmation(Guid id)
    {
        var realEstate = await _KindergartensServices.Delete(id);

        if (realEstate == null)
        {
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> RemoveImage(KindergartenImageViewModel vm)
    {
        var dto = new FileToDatabaseDto()
        {
            Id = vm.ImageId
        };

        var image = await _fileServices.RemoveImageFromDatabase(dto);

        if (image == null)
        {
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction(nameof(Index));
    }
}
}
