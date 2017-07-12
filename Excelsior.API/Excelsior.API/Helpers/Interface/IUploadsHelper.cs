using Excelsior.Business.DtoEntities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excelsior.API.Helpers
{
    public interface IUploadsHelper
    {
        void ConfigureStorageCORS();
        void SendMediaProcessingRequest(long mediaId);
        ResultInfo<string> SetMediaProperties(long mediaId, string path, string originalFileName);
        ResultInfo<string> GetMediaStorageUrl(HttpRequest request, bool isReadOnly = false);
        ResultInfo<BlockListResponseDto> PutBlockList(string path, BlockListRequestDto request);
        ResultInfo<IList<BlockResponseDto>> GetBlockList(string orginalFileName, string path);
    }    
}
