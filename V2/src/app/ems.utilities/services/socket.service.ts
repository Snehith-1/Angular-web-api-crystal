import { HttpClient, HttpEvent, HttpHandler, HttpHeaders, HttpParams, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SocketService {
 
  constructor(private http: HttpClient) {
  }
  // async function getSheetData(): Promise<void> {
    //   const scriptUrl = 'https://script.googleusercontent.com/macros/echo?user_content_key=uWYkyfMPAwQFrpEl89gNtkUsrpngc0u6pgLyMVslG3r6i-C3KAKjoVm6gF7Adzi7_URqTFO-aa21qgXWRL6gfZWa6jDM-jZBm5_BxDlH2jW0nuo2oDemN9CCS2h10ox_1xSncGQajx_ryfhECjZEnJIQCV4uchpEa1LrcaPIUUWoGsal0diXNnZTmYoJ_MhFfI_qF3qqKgnfLxsMjcjqMTJBdG31Cw-uQRUuhtxpqQjPwhZSh2rmFdz9Jw9Md8uu&lib=MyY8D8eoe-tQuM2ppETkYKyjXqd7_VK5y'; // Replace with your Apps Script URL
    
    //   try {
    //     const response = await fetch(scriptUrl);
    
    //     if (response.ok) {
    //       const responseData = await response.json();
    //       // const responseData = await response.text();
    //       console.log('Data from Google Apps Script:');
    //       // console.log(responseData);
    //       const responseDatalist = responseData;
    //       console.log(responseDatalist)
         
    //      return  responseData
         
    //       // Handle your data here
    //     } else {
    //       console.error(`Failed to fetch data from the script. Status: ${response.status}`);
    //     }
        
    //   } catch (error) {
    //     console.error('Error fetching data:', error);
    //   }
    // }
    
    // // Call the function to fetch data
    // getSheetData();
  get(api: string) {
    return this.http.get(api);

  }
  getdtl(api: string){
    var user_gid = localStorage.getItem('user_gid'); 
    return this.http.get<any>(api + '?user_gid=' + user_gid); 
  }
  getparams(api: string, params: any) {
    return this.http.get(api, { params: params });
  }
  postparams(api:string, params:any){ 
    var user_gid = localStorage.getItem('user_gid'); 
    return this.http.post<any>(api + '?user_gid=' + user_gid ,params); }
    download(api: string, params: any) {
    return this.http.get(api, { params: params });
   }
 

getid(url:string,params :any) {
   return this.http.get<any>(url+ '?params_gid=' + params); }
       
  //////Snehith code Start//////////////////
  postfile(api: string, params: any) {


    return this.http.post(api, params);
  }

  //////Snehith code End//////////////////
  post(api: string, params: any) {
    return this.http.post(api, params);
  }
  generateexcel(api: string) {
    return this.http.get(api);

  }
  downloadimage(api: string, params: any) {

    return this.http.get(api, { params: params });
    }
    downloadFile(params: any){
      return this.http.post('azurestorage/DownloadDocument', params);
    }
    downloadfile(path: any, file_name: any) {
      var params = {
        file_path: path,
        file_name: file_name
      }
      return this.http.post('azurestorage/DownloadDocument', params);
    }
  
    
    filedownload(data: any) {
      const file_type = this.createBlobFromExtension(data.format);
      const blob = new Blob([data.file], { type: file_type });
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = data.name; // Specify the desired file name and extension.
      document.body.appendChild(a);
      a.click();
      window.URL.revokeObjectURL(url);
      
    }
    filedownload1(data: any) {
      const file_type = this.createBlobFromExtension(data.format);
      const base64Content = data.file;
      // Convert base64 to binary
      const binaryContent = atob(base64Content);
      // Create a Uint8Array from the binary content
      const uint8Array = new Uint8Array(binaryContent.length);
      for (let i = 0; i < binaryContent.length; i++) {
        uint8Array[i] = binaryContent.charCodeAt(i);
      }
      const blob = new Blob([uint8Array], { type: file_type });
      const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = data.name; // Specify the desired file name and extension.
        document.body.appendChild(a);
        a.click();
        window.URL.revokeObjectURL(url);
    }

    createBlobFromExtension(file_extension: any){
      const mimeTypes:{ [key: string]: string } = {
        txt: 'text/plain',
        html: 'text/html',
        css: 'text/css',
        js: 'application/javascript',
        json: 'application/json',
        xml: 'application/xml',
        pdf: 'application/pdf',
        doc: 'application/msword',
        docx: 'application/vnd.openxmlformats-officedocument.wordprocessingml.document',
        xls: 'application/vnd.ms-excel',
        xlsx: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
        ppt: 'application/vnd.ms-powerpoint',
        pptx: 'application/vnd.openxmlformats-officedocument.presentationml.presentation',
        jpeg: 'image/jpeg',
        jpg: 'image/jpeg',
        png: 'image/png',
        gif: 'image/gif',
        bmp: 'image/bmp',
        tiff: 'image/tiff',
        svg: 'image/svg+xml',
        mp3: 'audio/mpeg',
        wav: 'audio/wav',
        mp4: 'video/mp4',
        mov: 'video/quicktime',
        avi: 'video/x-msvideo',
        zip: 'application/zip',
        rar: 'application/x-rar-compressed',
        // Add more mappings as needed for other extensions
      };
      const minetype = mimeTypes[file_extension.toLocaleLowerCase()];
    return minetype;
  }
  
}



  