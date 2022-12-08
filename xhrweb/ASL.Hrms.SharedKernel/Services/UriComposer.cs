using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASL.Hrms.SharedKernel.Services
{
    public class UriComposer : IUriComposer
    {
        private readonly PlanetHRSettings _planetHRSettings;

        public UriComposer(PlanetHRSettings planetHRSettings) => _planetHRSettings = planetHRSettings;

        public string ComposeProfilePicUri(string pictureName)
        {
            return $"{_planetHRSettings.BaseUrl}/{_planetHRSettings.ContentRoot.EmployeeImagePath}{pictureName}";
        }
        public string ComposeCompanyLogoUri(string pictureName)
        {
            return $"{_planetHRSettings.BaseUrl}/{_planetHRSettings.ContentRoot.CompanyLogoPath}{pictureName}";
        }
        public string ComposeEmployeeSignatureUri(string pictureName)
        {
            return $"{_planetHRSettings.BaseUrl}/{_planetHRSettings.ContentRoot.EmployeeSignaturePath}{pictureName}";
        }

        public string ComposeAttachedFileUri(string attachedFileName)
        {
            return $"{_planetHRSettings.BaseUrl}/{_planetHRSettings.ContentRoot.AttachedFilePath}{attachedFileName}";
        }

        public string ComposeResetPasswordUri(string token, string email)
        {
            return $"{_planetHRSettings.ClientUrl}/auth/reset-password?token={token}&email={email}";
        }
    }
}
