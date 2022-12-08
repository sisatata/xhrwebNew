import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FileUploadService {

  private supportedImageFile: string[] = ['image/jpeg', 'image/png'];

  constructor() { }

  imageFileUpload(files: any): any {
    let fileValidationError = null;
    let fileName: any;
    if (files.length === 0)
      fileValidationError = "Empty file";

    if (files[0].size > (1024 * 1024)) {
      fileValidationError = "Maximum file size should be 1 MB";
    }

    let imageType = files[0].type;
    let validImage = this.supportedImageFile.filter(c => c == imageType)[0];
    if (validImage == null) {
      fileValidationError = "Only JPG/PNG files are supported";
    }

    const formData = new FormData();

    for (let file of files) {
      formData.append(file.name, file);
      fileName = file.name;
    }

    return { validationError: fileValidationError, fileName: fileName, formData: formData };
  }
}
