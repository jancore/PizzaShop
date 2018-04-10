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
            
        console.log('User is logged');
        return deferred.promise;
        

        /*
        return this.http({
            method: 'POST',
            // set content-type to undefined so it is automatically selected
            headers: { 'Content-Type': undefined },
            url: super.getRoute(),
            data: {
                name: user.name,
                ingredients: user.ingredients,
                image: user.img,
            },
            transformRequest: function (data, headersGetter) {
                let formData = new FormData();
                angular.forEach(data, function (value, key) {
                    if (key === 'image' && value) {
                        formData.append(key, value, value.name);
                    }
                    else formData.append(key, value);
                });
                return formData;
            }
        });
        */
    }
}
LoginService.$inject = ['$http', 'resolveUrl'];
App.service('loginService', LoginService);