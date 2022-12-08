using CompanySetup.Application.Company.Commands.Models;
using MediatR;
using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CompanyEntities = CompanySetup.Core.Entities;
using CompanySetup.Core.Interfaces;
using CompanySetup.Core.Specifications;
using System.Linq;

namespace CompanySetup.Application.Company.Commands
{
    public class UpdateCompanyLogoCommandHandler : IRequestHandler<CompanyCommands.V1.UpdateCompanyImage, UpdateCompanyImageCommandVM>
    {
        public UpdateCompanyLogoCommandHandler(IAsyncRepository<CompanyEntities.CompanyAggregate.Company, Guid> repository, IImageFileManager imageFileManager, IUriComposer uriComposer, ICurrentUserContext userContext)
        {
            _repository = repository;
            _imageFileManager = imageFileManager;
            _uriComposer = uriComposer;
            _userContext = userContext;
        }
           private readonly IAsyncRepository<CompanyEntities.CompanyAggregate.Company, Guid> _repository;
           private readonly IImageFileManager _imageFileManager;
           private readonly IUriComposer _uriComposer;
        private readonly ICurrentUserContext _userContext;
        public async Task<UpdateCompanyImageCommandVM> Handle(CompanyCommands.V1.UpdateCompanyImage request, CancellationToken cancellationToken)
        {
            var response = new UpdateCompanyImageCommandVM
            {
                Status = false,
                Message = "error"
            };
            try {
                var pictureUri = string.Empty;
                pictureUri = _imageFileManager.UploadImageFile(request.CompanyLogo,
                       $"{request.Settings.ContentRoot.RootPath}{request.Settings.ContentRoot.CompanyLogoPath}",
                       new ImageFileSettings
                       {
                           ApplyResize = true,
                           MaxFileSize = request.Settings.PlanetHRImageFileSettings.MaxFileSize,
                           Width = request.Settings.PlanetHRImageFileSettings.MaxWidth,
                           Height = request.Settings.PlanetHRImageFileSettings.MaxHeight,
                           ValidExtensions = request.Settings.PlanetHRImageFileSettings.ValidExtensions.Split(',').ToList()
                       });
                var companyId = new Guid( _userContext.CurrentUserCompanyId);
                var compayFilter = new CompanyLogoFilterSpecification(companyId);
                var companies = await _repository.ListAsync(compayFilter).ConfigureAwait(false);
                if (!companies.Any())
                { return response; }
                var company = companies.FirstOrDefault();
                company.UpdateCompanyLogo(pictureUri);
                await _repository.UpdateAsync(company);
                response.Status = true;
                response.PictureUri = pictureUri;

                response.Message = "entity updated successfully.";
                response.Id = company.Id;
                return response;


            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
