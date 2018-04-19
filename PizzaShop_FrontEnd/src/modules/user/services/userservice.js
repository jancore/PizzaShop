import { App } from '../../module';
import { BaseService } from '../../baseservice';

export class UserService extends BaseService {
    constructor(http, resolveUrl, localStorageService) {
        super(http, resolveUrl, 'api/Account/Register', 'Token');
        this._localStorage=localStorageService;
    }

    create(user) {
        return this.http.post(
            super.getRoute(),
            user
        );
    }

    logger(user) {
        var self = this;
        var data = "grant_type=password&username=" + user.userName + "&password=" + user.password;
        return this.http.post(
            super.getSecondRoute(),
            data,
            { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }
        ).then(function (response) {
            self._localStorage.set('authorizationData', { token: response.data.access_token, userName: user.userName });
            return response;
        },function error(response) {
            return response;
        });
    }

    clearLog() {
        var self = this;
        self._localStorage.remove('authorizationData');
    }
}

UserService.$inject = ['$http', 'resolveUrl', 'localStorageService'];
App.service('userService', UserService);