using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Models;
using EmployeeEnrollment.Core.Interfaces;
using EmployeeEnrollment.Core.Specifications;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeEnrollment.Commands
{
    public class ImportEmployeeFromExcelCommandHandler : IRequestHandler<Commands.V1.UpdateProfilePicture, UpdateEmployeeImageCommandVM>
    {
        public ImportEmployeeFromExcelCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeEnrollment, Guid> repository, IImageFileManager imageFileManager, IUriComposer uriComposer)
        {
            _repository = repository;
            _imageFileManager = imageFileManager;
            _uriComposer = uriComposer;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeEnrollment, Guid> _repository;
        private readonly IImageFileManager _imageFileManager;
        private readonly IUriComposer _uriComposer;

        public async Task<UpdateEmployeeImageCommandVM> Handle(Commands.V1.UpdateProfilePicture request, CancellationToken cancellationToken)
        {
            var response = new UpdateEmployeeImageCommandVM
            {
                Status = false,
                Message = "error"
            };


            try
            {
                var pictureUri = string.Empty;
                pictureUri = _imageFileManager.UploadImageFile(request.EmployeeImage,
                        $"{request.Settings.ContentRoot.RootPath}{request.Settings.ContentRoot.EmployeeImagePath}",
                        new ImageFileSettings
                        {
                            ApplyResize = true,
                            MaxFileSize = request.Settings.PlanetHRImageFileSettings.MaxFileSize,
                            Width = request.Settings.PlanetHRImageFileSettings.MaxWidth,
                            Height = request.Settings.PlanetHRImageFileSettings.MaxHeight,
                            ValidExtensions = request.Settings.PlanetHRImageFileSettings.ValidExtensions.Split(',').ToList()
                        });

                //pictureUri = _uriComposer.ComposeProfilePicUri(pictureUri);
                var employeeFilter = new EmployeeEnrollmentByEmployeeIdSpecification(request.EmployeeId);
                var employees = await _repository.ListAsync(employeeFilter).ConfigureAwait(false);
                if (!employees.Any())
                { return response; }
                var employee = employees.FirstOrDefault();


                //var entity = await _repository.GetByIdAsync(request.Id);

                employee.UpdateEmployeeImage(
                         request.EmployeeId,
                         pictureUri);

                await _repository.UpdateAsync(employee);

                response.Status = true;
                response.PictureUri = pictureUri;

                response.Message = "entity updated successfully.";
                response.Id = employee.Id;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}

