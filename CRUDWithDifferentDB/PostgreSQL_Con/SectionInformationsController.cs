using CRUDWithDifferentDB.PostgreSQL_Con.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDWithDifferentDB.PostgreSQL_Con
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionInformationsController : ControllerBase
    {
        protected readonly PostgreSQLDbContext _dbContext;
        public SectionInformationsController(PostgreSQLDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        [HttpPost]
        [Route("CreateSectionInformations")]
        
        public async Task<IActionResult> CreateSectionInformations(CreateSectionInformationsDTO objCreate)
        {
            try
            {
                var Info = new TblSectionInformation()
                {
                    StrBusinessUnit = objCreate.BusinessUnit,
                    StrSectionName = objCreate.SectionName,
                    StrReadingOf = objCreate.ReadingOf,
                    NumReadingValue = objCreate.ReadingValue,
                    DteActionTime = objCreate.DteActionTime
                };

                await _dbContext.TblSectionInformations.AddAsync(Info);
                await _dbContext.SaveChangesAsync();
                return Ok(Info);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetAllSectionInformations")]
        public async Task<IActionResult> GetAllSectionInformations()
        {
            try
            {
                var res = await _dbContext.TblSectionInformations.Select(x=> new CreateSectionInformationsDTO
                {
                    AutoId=x.IntAutoId,
                    BusinessUnit=x.StrBusinessUnit?? string.Empty,
                    SectionName=x.StrSectionName??string.Empty,
                    ReadingOf=x.StrReadingOf??string.Empty, 
                    DteActionTime=x.DteActionTime,
                    ReadingValue=x.NumReadingValue
                }).ToListAsync();
                           
                return Ok(res);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
