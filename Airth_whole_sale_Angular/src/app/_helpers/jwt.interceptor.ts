import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '@environments/environment';
import { AccountService } from '@app/_services';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    constructor(private accountService: AccountService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // add auth header with jwt if account is logged in and request is to the api url

        const account = this.accountService.accountValue;
        // const isLoggedIn = account && account.jwtToken;
        var isLoggedIn = "";

        var tokendata=localStorage.getItem('AuthToken');

        if(tokendata==null)
        {
          isLoggedIn=null;
        }
        else{
          isLoggedIn=tokendata;
        }

        //const headers = new HttpHeaders();
      //   headers.append('Content-Type', 'application/json');
      //  headers.append('Accept', 'application/json');
      //  headers.append('Authorization', 'Bearer ' + localStorage.getItem('AuthToken'));
      //  headers.append('Access-Control-Allow-Origin', 'https://localhost:7299');
      //  headers.append('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE');
      //  headers.append('Access-Control-Allow-Headers', 'X-Requested-With,content-type');

      //  const headers = new HttpHeaders({
      //   'Access-Control-Allow-Origin':'*',
      //   'Content-Type':'application/x-www-form-urlencoded',
      //  'Authorization':'Bearer ' + localStorage.getItem('AuthToken'),

      //  'Access-Control-Allow-Credentials':'true',
      //  'Access-Control-Allow-Methods':'*',
      //  'Access-Control-Allow-Headers':'Content-Type',
      // });


        const isApiUrl = request.url.startsWith(environment.apiUrl);
        if (isLoggedIn && isApiUrl) {
            request = request.clone({
              headers: request.headers.set('Authorization', 'Bearer ' + localStorage.getItem('AuthToken'))
            });
        }

        return next.handle(request);
    }
}
