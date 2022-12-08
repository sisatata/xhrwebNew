using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASL.Hrms.SharedKernel.Interfaces
{
    public interface IUriComposer
    {
        //string ComposeExamPicUri(string pictureName);
        string ComposeProfilePicUri(string pictureName);
        string ComposeCompanyLogoUri(string logoName);
        string ComposeEmployeeSignatureUri(string signature);
        string ComposeResetPasswordUri(string token, string email);

        string ComposeAttachedFileUri(string attachedFileName);
    }
}
