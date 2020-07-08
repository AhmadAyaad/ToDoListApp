import { Component, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import{baseUrl , loginUrl} from 'src/app/_config/api';
import { AuthService } from '../_services/auth.service';
import { from } from 'rxjs';
@Component({
  selector: 'app-dairy',
  templateUrl: './dairy.component.html',
  styleUrls: ['./dairy.component.css']
})
export class DairyComponent implements OnInit {

  uploader : FileUploader;
  hasBaseDropZoneOver: boolean;
  response: string;
  date;
  text;
  time;

  constructor(private authService : AuthService) { }

  ngOnInit() {

    this.initializeUploader();
     
      this.uploader.uploadAll();
  }



  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({

      url:
        baseUrl + 'user/' + this.authService.decodedToken.nameid + '/dairy',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024,
    });


    this.uploader.onBuildItemForm = (item, form) => {
      form.append("text", this.text);
      form.append("date", this.date);
      form.append('time', this.time);
    };

    

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    };
    
    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const res = JSON.parse(response);
        console.log(res);
        const photo = {
          id: res.id,
          url: res.url,
          dateAdded: res.dateAdded,
        };
        console.log(res.url);
      }
    };
  }

  
}
