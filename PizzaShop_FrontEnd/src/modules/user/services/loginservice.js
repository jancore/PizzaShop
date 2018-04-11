import { App } from '../../module';
import { BaseService } from '../../baseservice';

export class LoginService extends BaseService {
    constructor(http, resolveUrl) {
        super(http, resolveUrl, 'Token');
    }

    get(id) {
        return this.http.get(
            super.getRouteById(id)
        );
    }
    getAll() {
        return this.http.get(
            super.getRoute()
        );
    }
    logger(user) {
        var data = "grant_type=password&username=" + user.userName + "&password=" + user.password;
        return this.http.post(
            super.getRoute(),
            data,
            {headers: { 'Content-Type': 'application/x-www-form-urlencoded' }}
        );
        /*.success(function (response) {

            localStorageService.set('authorizationData', { token: response.access_token, userName: user.userName });
            _authentication.isAuth = true;
            _authentication.userName = user.userName;
        }).error(function (err, status) {
            //throw 
        });*/
    }
}
LoginService.$inject = ['$http', 'resolveUrl'];
App.service('loginService', LoginService);