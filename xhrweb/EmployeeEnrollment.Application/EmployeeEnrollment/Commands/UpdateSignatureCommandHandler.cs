using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Models;
using EmployeeEnrollment.Core.Interfaces;
using EmployeeEnrollment.Core.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeEnrollment.Commands
{
    public class UpdateSignatureCommandHandler : IRequestHandler<Commands.V1.UpdateSignature, UpdateEmployeeSignatureCommandVM>
    {
        #region ctor
        public UpdateSignatureCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeEnrollment, Guid> repository, IImageFileManager imageFileManager, IUriComposer uriComposer)
        {
            _repository = repository;
            _imageFileManager = imageFileManager;
            _uriComposer = uriComposer;
        }
        #endregion
        #region properties
        private readonly IAsyncRepository<EmployeeEntities.EmployeeEnrollment, Guid> _repository;
        private readonly IImageFileManager _imageFileManager;
        private readonly IUriComposer _uriComposer;
        #endregion
        public async Task<UpdateEmployeeSignatureCommandVM> Handle(Commands.V1.UpdateSignature request, CancellationToken cancellationToken)
        {
            var response = new UpdateEmployeeSignatureCommandVM
            {
                Status = false,
                Message = "error"
            };
            try {
                var signatureUri = string.Empty;
                signatureUri = _imageFileManager.UploadImageFile(request.EmployeeSignature, $"{request.Settings.ContentRoot.RootPath}{request.Settings.ContentRoot.EmployeeSignaturePath}",

                      new ImageFileSettings
                      {
                          ApplyResize = true,
                          MaxFileSize = request.Settings.PlanetHRImageFileSettings.MaxFileSize,
                          Width = request.Settings.PlanetHRImageFileSettings.MaxWidth,
                          Height = request.Settings.PlanetHRImageFileSettings.MaxHeight,
                          ValidExtensions = request.Settings.PlanetHRImageFileSettings.ValidExtensions.Split(',').ToList()
                      }
                    );
                var employeeFilter = new EmployeeEnrollmentByEmployeeIdSpecification(request.EmployeeId);
                var employees = await _repository.ListAsync(employeeFilter).ConfigureAwait(false);
                if (!employees.Any())
                { return response; }
                var employee = employees.FirstOrDefault();
                employee.UpdateEmployeeSignature(request.EmployeeId, signatureUri);
                await _repository.UpdateAsync(employee);
                response.Status = true;
                response.SignatureUri = signatureUri;
                response.Message = "entity updated successfully.";
                response.Id = employee.Id;
            }
            catch (Exception ex) {
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
