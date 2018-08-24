using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SSupply.Web.Interfaces
{
    public interface IImageStorageService
    {
        Task<string> UploadFile(IFormFile file);
        Task DeleteFile(string url);
    }
}
