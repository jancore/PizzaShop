import { App } from '../../module';
import { BaseService } from '../../baseservice';

export class LoginService extends BaseService {
    constructor(http, resolveUrl) {
        super(http, resolveUrl, 'token');
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
    logger(loginData) {

        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

        var deferred = $q.defer();

        $http.post(
            'http//:localhost:51889/token',
            data,
            {headers: { 'Content-Type': 'application/x-www-form-urlencoded' }}
        ).success(function (response) {

            localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName });    
            _authentication.isAuth = true;
            _authentication.userName = loginData.userName;

                deferred.resolve(response);

            }).error(function (err, status) {
                //_logOut();
                deferred.reject(err);
            });
        return deferred.promise;
    }
}
LoginService.$inject = ['$http', 'resolveUrl'];
App.service('loginService', LoginService);