using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Models;
using MediatR;
using PayrollManagement.Application.BenefitBillClaim.Commands.Models;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.BenefitBillClaim.Commands
{
    //public class UpdateBillAttachmentCommandHandler : IRequestHandler<Commands.V1.UpdateBillAttachment, UpdateBillAttachmentCommandVM>
    //{
    //    public UpdateBillAttachmentCommandHandler(IAsyncRepository<PayrollEntities.BenefitBillClaim, Guid> repository, IImageFileManager imageFileManager, IUriComposer uriComposer)
    //    {
    //        _repository = repository;
    //        _imageFileManager = imageFileManager;
    //        _uriComposer = uriComposer;
    //    }

    //    private readonly IAsyncRepository<PayrollEntities.BenefitBillClaim, Guid> _repository;
    //    private readonly IImageFileManager _imageFileManager;
    //    private readonly IUriComposer _uriComposer;

    //    public async Task<UpdateBillAttachmentCommandVM> Handle(Commands.V1.UpdateBillAttachment request, CancellationToken cancellationToken)
    //    {
    //        var response = new UpdateBillAttachmentCommandVM
    //        {
    //            Status = false,
    //            Message = "error"
    //        };


    //        try
    //        {
    //            var pictureUri = string.Empty;

    //            pictureUri = _imageFileManager.UploadImageFile(request.BillAttachment,
    //                    $"{request.Settings.ContentRoot.RootPath}{request.Settings.ContentRoot.EmployeeImagePath}",
    //                    new ImageFileSettings
    //                    {
    //                        ApplyResize = true,
    //                        MaxFileSize = request.Settings.PlanetHRImageFileSettings.MaxFileSize,
    //                        Width = request.Settings.PlanetHRImageFileSettings.MaxWidth,
    //                        Height = request.Settings.PlanetHRImageFileSettings.MaxHeight,
    //                        ValidExtensions = request.Settings.PlanetHRImageFileSettings.ValidExtensions.Split(',').ToList()
    //                    });

    //            //pictureUri = _uriComposer.ComposeProfilePicUri(pictureUri);
    //            //var employeeFilter = new EmployeeEnrollmentByEmployeeIdSpecification(request.EmployeeId);
    //            //var employees = await _repository.ListAsync(employeeFilter).ConfigureAwait(false);
    //            //if (!employees.Any())
    //            //{ return response; }
    //            //var employee = employees.FirstOrDefault();


    //            var entity = await _repository.GetByIdAsync(request.Id);

    //            entity.UpdateBillAttachment(
    //                     pictureUri);

    //            await _repository.UpdateAsync(entity);

    //            response.Status = true;
    //            response.PictureUri = pictureUri;

    //            response.Message = "entity updated successfully.";
    //            response.Id = entity.Id;
    //        }
    //        catch (Exception ex)
    //        {
    //            response.Message = ex.Message;
    //        }

    //        return response;
    //    }
    //}
}
