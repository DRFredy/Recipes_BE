using AutoMapper;
using Microsoft.Extensions.Configuration;
using Recipes.API.Services.Interfaces;
using Recipes.DAL;
using System.IO;
using System.Threading.Tasks;

namespace Recipes.API.Services
{
  public class ImagesService : IImagesService
  {
    private readonly AppDbContext _context;
    private readonly UnitOfWork _unitOkWork;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;
    private readonly string _webRootPath;

    public ImagesService(AppDbContext context, IConfiguration config, IMapper mapper)
    {
      _mapper = mapper;
      _config = config;
      _context = context;
      _webRootPath = Directory.GetCurrentDirectory();
      _unitOkWork = new UnitOfWork(_context, _webRootPath, _config, _mapper);
    }

    public async Task<string> GetByFileNameAsync(string fileName)
    {
      if (string.IsNullOrWhiteSpace(fileName))
      {
        return null;
      }

      string img = string.Empty;

      try
      {
        img = await _unitOkWork.ImagesRepository.GetByFileNameAsync(fileName);

        _unitOkWork.Dispose();
      }
      catch
      { }

      return img;
    }

    public async Task<string> SaveAsync(string base64String)
    {
      string img = string.Empty;

      try
      {
        img = await _unitOkWork.ImagesRepository.SaveAsync(base64String);

        _unitOkWork.Dispose();
      }
      catch
      { }

      return img;
    }

    public async Task<bool> DeleteAsync(string imgFileName)
    {
      bool ret = false;

      try
      {
        ret = await _unitOkWork.ImagesRepository.DeleteAsync(imgFileName);

        _unitOkWork.Dispose();
      }
      catch 
      { }
      
      return ret;
    }
  }
}
