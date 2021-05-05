using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Interfaces.Repository;
using Recipes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Recipes.DAL.Repositories
{
  public class ImagesRepository : IImagesRepository
  {
    private readonly string _imgsPath;

    public ImagesRepository(string imgsPath)
    {
      _imgsPath = imgsPath;
    }

    public async Task<string> GetByFileNameAsync(string fileName)
    {
      return await Task.Run(() =>
      {
        string img = string.Empty;
        
        try
        {
          string pathAndFileNane = Path.Combine(_imgsPath, fileName);
          byte[] contentBytes = File.ReadAllBytes(pathAndFileNane);
          img = "data:image/png;base64," + Convert.ToBase64String(contentBytes);
        }
        catch
        { }

        return img;
      });
    }

    public async Task<string> SaveAsync(string base64String)
    {
      string imgFileName = string.Empty;

      if (base64String.StartsWith("data:image/"))
      {
        //"data:image/png;base64,"
        int posTo = base64String.IndexOf(";base64,") + ";base64,".Length;

        if (posTo > base64String.Length)
        {
          return string.Empty;
        }
        else
        {
          base64String = base64String.Substring(posTo).Trim();
        }
      }

      try
      {
        byte[] contentBytes = Convert.FromBase64String(base64String);

        if (contentBytes.Length > 0)
        {
          imgFileName = GenerateFileName();
          await File.WriteAllBytesAsync(Path.Combine(_imgsPath, imgFileName), contentBytes);
        }
      }
      catch
      {
        imgFileName = string.Empty;
      }

      return imgFileName;
    }

    public async Task<bool> DeleteAsync(string imgFileName)
    {
      try
      {
        await Task.Run(() => { 
          File.Delete(Path.Combine(_imgsPath, imgFileName));
        });
      }
      catch
      {
        return false;
      }

      return true;
    }

    #region private methods
    private string GenerateFileName()
    {
      bool alreadyExists = true;
      string fileName = string.Empty;

      while (alreadyExists)
      {
        Guid guid = Guid.NewGuid();
        fileName = $"{guid.ToString()}.jpg";

        if (!File.Exists(Path.Combine(_imgsPath, fileName)))
        {
          alreadyExists = false;
        }
      }

      return fileName;
    }
    #endregion
  }
}