import { App } from '../../module';
import { BaseService } from '../../baseservice';

export class UserService extends BaseService {
    constructor(http, resolveUrl) {
        super(http, resolveUrl, 'api/Account/Register', 'Token');
    }

    create(user) {
        return this.http.post(
            super.getRouteAddUser(),
            user
        );
    }

    logger(user) {
        var data = "grant_type=password&username=" + user.userName + "&password=" + user.password;
        return this.http.post(
            super.getRouteLogin(),
            data,
            {headers: { 'Content-Type': 'application/x-www-form-urlencoded' }}
        );
        /*
        .success(function (response) {

            localStorageService.set('authorizationData', { token: response.access_token, userName: user.userName });
            _authentication.isAuth = true;
            _authentication.userName = user.userName;
        }).error(function (err, status) {
            //throw 
        });
        */
    }
    
}
UserService.$inject = ['$http', 'resolveUrl'];
App.service('userService', UserService);